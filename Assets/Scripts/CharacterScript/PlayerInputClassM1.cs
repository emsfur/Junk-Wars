using UnityEngine;
using Unity.Netcode;
using Cinemachine;

[RequireComponent(typeof(CharacterController))]
public class PlayerInputClassM1 : NetworkBehaviour
//public class playerInputClass : MonoBehaviour
{
   // private static InputManager _instance;
    // Start is called before the first frame update
    //public Rigidbody2D rb;
    //private PlayerInput playerInput;
    //private CinemachineVirtualCamera vc;
    [SerializeField] private Cinemachine.CinemachineVirtualCamera vc;
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip walkClip;
    [SerializeField] private AudioClip pickupClip;
    [SerializeField] private AudioSource audioSource;
    //[SerializeField]private CinemachineCamera.CinemachineVirtualCamera vc;
    //[SerializeField] private AudioClip walkAudio;
    //[SerializeField] private AudioClip jumpAudio;
    private CharacterController controller;

    //^ a presdesigned scripted?
    private InputManager inputManager;
    private Transform cameraTransform;
    public bool groundPlayer;
    // for the ability to jump;
    //private PlayerInput playerInput;
    public float moveSpeed =2f;
    //move speed duh
    //public InputAction playerControls;
    public PlayerControls playerControls;
    //the stupid fucking aciton map
    private Vector3 playerVelocity;
    
    //for the need for speed
    public float jumpHeight =100f;
    //For how one jumps
    //Vector2 moveDirection = Vector2.zero;
    //private InputAction move;
    private float gravityValue = -9.81f;

    public int thisScore =0;
    //private AudioSource jumpAudioSource;
    //private AudioSource walkAudioSource;
    
//   private static InputManager _instance;

    //handing player interaction
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
         //inputManager = InputManager.Instance;
         inputManager = InputManager.Instance;
        //  if(inputManager == null) {
        //     Debug.LogError("nOt foud");
        //  }
        cameraTransform = Camera.main.transform;
       // jumpAudioSource = GetComponent<AudioSource>();
       // walkAudioSource = GetComponent<AudioSource>();

       
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


            // anchorProxyRb = anchorProxyObject.GetComponent<Rigidbody>();
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
        else if(IsOwner) {
            cameraTransform = Camera.main.transform;
            groundPlayer = controller.isGrounded;

            if(groundPlayer && playerVelocity.y < 0) {
                playerVelocity.y = 0f;
            }
        
            //Vector2 moveInput = playerControls.Touch.Moving.ReadValue<Vector2>();
            //Vector2 moveInput = playerControls.Player.Move.ReadValue<Vector2>();
            Vector2 moveInput = inputManager.GetPlayerMovement();
            //Debug.Log(this.transform.position);
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

                // if(move != Vector3.zero) {
                //     //controller.Move(move);
                //     gameObject.transform.forward=move;
                // }
            
            //if (playerControls.Player.Jump.triggered && groundPlayer) {
            if(inputManager.PlayerJumpedThisFrame() && groundPlayer){
                // AudioSource.PlayClipAtPoint(jumpAudio,transform.position, 1f);
                //jumpAudioSource.clip = jumpAudio;
                    //jumpAudioSource.Play();
                playerVelocity.y += Mathf.Sqrt(jumpHeight*-2.0f*gravityValue);
                PlaySoundServerRpc("jump");
            }
            //   if(move != Vector3.zero) {
            //     gameObject.transform.forward = move; 
            //     }
            playerVelocity.y += gravityValue* Time.deltaTime;//*moveSpeed;
            controller.Move(playerVelocity * Time.deltaTime);
            // walkAudioSource.clip = walkAudio;
            // walkAudioSource.Play();
                //AudioSource.PlayClipAtPoint(walkAudio, transform.position,1f);
            Debug.Log("Speed is" + moveSpeed);
            // Debug.Log(controller.pos);
            Debug.Log("From playerInputController" + thisScore);

            // UpdateAnchorProxyServerRpc(anchor.position, anchor.rotation);
            PlayerScrapInteractions();
            PlayerDoorInteraction();
        }
    }

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

    [ServerRpc]
    void HandlePickUpServerRpc(NetworkObjectReference objRef, ServerRpcParams rpcParams = default) 
    {
        ulong senderId = rpcParams.Receive.SenderClientId;
        NetworkManager.Singleton.ConnectedClients.TryGetValue(senderId, out NetworkClient targetClient);

        // arguments: reference to scrap object, reference to player interacting with object
        PickupObjectServerRpc(objRef, targetClient.PlayerObject.NetworkObjectId);
    }


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

                // obj.transform.localPosition = Vector3.zero;
                obj.transform.localRotation = Quaternion.identity;

                PlaySoundClientRpc("pickup");
            }
        }
    }

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

        if (clipToPlay != null && audioSource != null)
        {
            audioSource.PlayOneShot(clipToPlay);
        }
    }

    // partial implementation where pick up involves more physics
    // void PlayerScrapInteractions() {
    //     if (inputManager.PlayerInteracted()) {
    //         // if player has object in hand, drop it where it is
    //         if (inHand != null) {

    //             inHand.TryGetComponent<NetworkObject>(out var netObj);
    //             DropObjectServerRpc(netObj);

    //             inHand = null;
    //         }
    //         else {
    //             // process what user is interacting with (scrap/door/others)
    //             ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    //             if (Physics.Raycast(ray, out RaycastHit hit, 3)) {
    //                 GameObject obj = hit.collider.gameObject;

    //                 // if player lookcing at scrap and doesn't have item in hand
    //                 if (obj.tag == "Scrap" && inHand == null) {
    //                     // marks item inhand on client side
    //                     inHand = obj;

    //                     Debug.Log("ems: picking up scrap");

    //                     // stores network reference to let server handle the rest
    //                     inHand.TryGetComponent<NetworkObject>(out var netObj);

    //                     InteractWithObjectServerRpc(netObj, hit.transform.position);


    //                 }
    //             }
    //         }
    //     }
    // }

    // [ServerRpc]
    // private void UpdateAnchorProxyServerRpc(Vector3 anchorPosition, Quaternion anchorRotation)
    // {
    //     anchorProxyObject.position = anchorPosition;
    //     anchorProxyObject.rotation = anchorRotation;
    // }


    // [ServerRpc]
    // private void DropObjectServerRpc(NetworkObjectReference objRef)
    // {
    //     if (objRef.TryGet(out var obj))
    //     {
    //         SpringJoint joint = obj.GetComponent<SpringJoint>();
    //         Rigidbody rb = obj.GetComponent<Rigidbody>();

    //         rb.useGravity = true;
    //         Destroy(joint);
    //     }
    // }

    // [ServerRpc]
    // private void InteractWithObjectServerRpc(NetworkObjectReference objRef, Vector3 hitPos) {
    //     if (objRef.TryGet(out var obj))
    //     {
    //         Rigidbody itemRb = obj.GetComponent<Rigidbody>();
    //         itemRb.useGravity = false;
    //         itemRb.velocity = Vector3.zero;
    //         anchorProxyObject.position = hitPos;

    //         Rigidbody anchorRb = anchorProxyRb; 
    //         itemRb.angularVelocity = Vector3.zero;

    //         SpringJoint joint = obj.gameObject.AddComponent<SpringJoint>();
    //         joint.connectedBody = anchorRb;

    //         joint.spring = 100f; // pull force towards anchor
    //         joint.damper = 50f; // wobble modifier

    //         // spring stretch limits
    //         joint.minDistance = 0.01f;
    //         joint.maxDistance = 0.05f;
    //     }
    // }



}
