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
    //[SerializeField]private CinemachineCamera.CinemachineVirtualCamera vc;
    //[SerializeField] private AudioClip walkAudio;
    //[SerializeField] private AudioClip jumpAudio;
    private CharacterController controller;

    //^ a presdesigned scripted?
    private InputManager inputManager;
    private Transform cameraTransform;
    private bool groundPlayer;
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
    Ray ray;
    
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
        if(IsOwner) {
            // begin first person cam
            vc.Priority =1;

            // disable cursor when game begins
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
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
       Debug.Log(this.transform.position);
       Vector3 move = new Vector3(moveInput.x,0f,moveInput.y);
       move=cameraTransform.forward * move.z + cameraTransform.right * move.x;
       move.y = 0f;
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

        // handling player interaction
        if (inputManager.PlayerInteracted()) {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit)) {
                Debug.Log(hit.collider.gameObject.name + " was hit.");
            }
        }
    }
    }    
}
