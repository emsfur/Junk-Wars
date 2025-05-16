using UnityEngine;
using Unity.Netcode;
using Cinemachine;

[RequireComponent(typeof(CharacterController))]
public class PlayerInputClassM1 : NetworkBehaviour
{
    [SerializeField] private Cinemachine.CinemachineVirtualCamera vc;
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip walkClip;
    [SerializeField] private AudioClip pickupClip;
    [SerializeField] private AudioSource audioSource;
    private CharacterController controller;
    private InputManager inputManager;
    private Transform cameraTransform;
    public bool groundPlayer;
    public float moveSpeed =2f;
    public PlayerControls playerControls;
    private Vector3 playerVelocity;
    public float jumpHeight =100f;
    private float gravityValue = -9.81f;
    public int thisScore =0;
    private Ray ray;
    private GameObject inHand;
    private Transform anchor; // client side
    [SerializeField] private Transform anchorProxyObject; // server side sync
    private Rigidbody anchorProxyRb;

    private bool wasGroundedLastFrame = true;
    private float walkSoundCooldown = 0f;

    private void Start()
    {
        controller= GetComponent<CharacterController>();
        inputManager = InputManager.Instance;
        cameraTransform = Camera.main.transform;
    }
    public override void OnNetworkSpawn()
    {
        anchorProxyRb = anchorProxyObject.GetComponent<Rigidbody>();
        if(IsOwner) {
            // begin first person cam
            vc.Priority =1;

            // disable cursor when game begins
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;

            // grabs item anchor (located as a child of main camera)
            anchor = Camera.main.transform.Find("ItemAnchor");
        }
        else {
            vc.Priority =0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsOwner) {
            return;
        }
        //Start camera and character speed after grounding them
        else if(IsOwner) {
            cameraTransform = Camera.main.transform;
            groundPlayer = controller.isGrounded;

            if(groundPlayer && playerVelocity.y < 0) {
                playerVelocity.y = 0f;
            }

            Vector2 moveInput = inputManager.GetPlayerMovement();
            Vector3 move = new Vector3(moveInput.x,0f,moveInput.y);
            move=cameraTransform.forward * move.z + cameraTransform.right * move.x;
            move.y = 0f;

            bool isWalking = move.magnitude > 0.1f && groundPlayer;

            // Play walking sound with cooldown
            if (isWalking && walkSoundCooldown <= 0f)
            {
                PlaySoundServerRpc("walk");
                walkSoundCooldown = 0.5f;
            }
            walkSoundCooldown -= Time.deltaTime;

            controller.Move(move*Time.deltaTime*moveSpeed);

            //Logic for jumping action and sound
            if(inputManager.PlayerJumpedThisFrame() && groundPlayer){
                playerVelocity.y += Mathf.Sqrt(jumpHeight*-2.0f*gravityValue);
                PlaySoundServerRpc("jump");
            }

            playerVelocity.y += gravityValue* Time.deltaTime;//*moveSpeed;
            controller.Move(playerVelocity * Time.deltaTime);
            Debug.Log("Speed is" + moveSpeed);
            Debug.Log("From playerInputController" + thisScore);

            // UpdateAnchorProxyServerRpc(anchor.position, anchor.rotation);
            PlayerScrapInteractions();
            PlayerDoorInteraction();
        }
    }

    //Giving player the ability to interact with doors
    void PlayerDoorInteraction() {
        if (inputManager.DoorInteracted()) {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit, 3)) {
                GameObject obj = hit.collider.gameObject;

                if (obj.tag == "Door") {
                    obj.transform.GetComponent<DoorHandler>().ToggleDoor();
                }
            }
        }
    }


    void PlayerScrapInteractions() {
        if (inputManager.PlayerInteracted()) {
            // if player has object in hand, drop it where it is
            if (inHand != null) {

                inHand.TryGetComponent<NetworkObject>(out var netObj);
                DropObjectServerRpc(netObj);

                inHand = null;
            }
            else {
                // process what user is interacting with (scrap)
                ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hit, 3)) {
                    GameObject obj = hit.collider.gameObject;

                    // if player lookcing at scrap and doesn't have item in hand
                    if (obj.tag == "Scrap" && inHand == null) {
                        // marks item inhand on client side
                        inHand = obj;

                        // stores network reference to let server handle the rest
                        inHand.TryGetComponent<NetworkObject>(out var netObj);

                        HandlePickUpServerRpc(netObj);

                    }
                }
            }
        }
    }

    //Handling Items over server
    [ServerRpc]
    void HandlePickUpServerRpc(NetworkObjectReference objRef, ServerRpcParams rpcParams = default) 
    {
        ulong senderId = rpcParams.Receive.SenderClientId;
        NetworkManager.Singleton.ConnectedClients.TryGetValue(senderId, out NetworkClient targetClient);

        // arguments: reference to scrap object, reference to player interacting with object
        PickupObjectServerRpc(objRef, targetClient.PlayerObject.NetworkObjectId);
    }

    // Picking up items over server
    [ServerRpc]
    void PickupObjectServerRpc(NetworkObjectReference objRef, ulong targetNetPlayerObjId)
    {
        if (objRef.TryGet(out var obj))
        {
            if (NetworkManager.Singleton.SpawnManager.SpawnedObjects.TryGetValue(targetNetPlayerObjId, out NetworkObject playerObj))
            {
                Collider itemBC = obj.GetComponent<BoxCollider>();
                Rigidbody itemRB = obj.GetComponent<Rigidbody>();

                itemRB.useGravity = false;
                itemBC.isTrigger = true;

                obj.transform.SetParent(playerObj.transform);

                Transform cameraTransform = playerObj.transform.Find("MainCamera");
                obj.transform.localPosition = new Vector3(0f, -0.5f, 1f);
                obj.transform.localRotation = Quaternion.identity;

                PlaySoundClientRpc("pickup");
            }
        }
    }

    //Droping objects over server
    [ServerRpc]
    void DropObjectServerRpc(NetworkObjectReference objRef)
    {
        if (objRef.TryGet(out var obj))
        {
            Collider itemBC = obj.GetComponent<BoxCollider>();
            Rigidbody itemRB = obj.GetComponent<Rigidbody>();

            itemRB.useGravity = true;
            itemBC.isTrigger = false;
            obj.transform.SetParent(null);
        }
    }

    //Logic for playing sounds over server
    [ServerRpc]
    void PlaySoundServerRpc(string soundType, ServerRpcParams rpcParams = default)
    {
        ClientRpcParams ownerOnly = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new ulong[] { rpcParams.Receive.SenderClientId }
            }
        };

        PlaySoundClientRpc(soundType, ownerOnly);
    }

    //Client side list of sounds to play
    [ClientRpc]
    void PlaySoundClientRpc(string soundType, ClientRpcParams clientRpcParams = default)
    {
        AudioClip clipToPlay = null;
        switch (soundType)
        {
            case "jump":
                clipToPlay = jumpClip;
                break;
            case "walk":
                clipToPlay = walkClip;
                break;
            case "pickup":
                clipToPlay = pickupClip;
                break;
        }

        //Plays the sound for client
        if (clipToPlay != null && audioSource != null)
        {
            audioSource.PlayOneShot(clipToPlay);
        }
    }
}
