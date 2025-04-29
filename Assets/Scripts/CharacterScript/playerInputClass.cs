
using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class playerInputClass : MonoBehaviour
{
   // private static InputManager _instance;
    // Start is called before the first frame update
    //public Rigidbody2D rb;
    //private PlayerInput playerInput;
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
    
//   private static InputManager _instance;

    
    
    private void Start()
    {
         controller= GetComponent<CharacterController>();
         //inputManager = InputManager.Instance;
         inputManager = InputManager.Instance;
        //  if(inputManager == null) {
        //     Debug.LogError("nOt foud");
        //  }
        cameraTransform = Camera.main.transform;
    }
    //  private void OnEnable()
    // {
    //    // playerControls = playerControls.Player.Move;
    //     playerControls.Enable();
    //     //playerControls.Enable();
    // }
    // private void OnDisable()
    // {
    //     playerControls.Disable();       
    // }
    // private void Awake()
    // {
    //    // playerInput = new PlayerInput;
    //     playerControls = new PlayerControls();
    //     controller = GetComponent<CharacterController>();
    // }
   
    // Update is called once per frame
       void Update()
    {
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
        if(move != Vector3.zero) {
            gameObject.transform.forward = move; 
        }

        // if(move != Vector3.zero) {
        //     //controller.Move(move);
        //     gameObject.transform.forward=move;
        // }
       
       //if (playerControls.Player.Jump.triggered && groundPlayer) {
        if(inputManager.PlayerJumpedThisFrame() && groundPlayer){
            playerVelocity.y += Mathf.Sqrt(jumpHeight*-2.0f*gravityValue);
       }
       
        playerVelocity.y += gravityValue* Time.deltaTime;//*moveSpeed;
        controller.Move(playerVelocity * Time.deltaTime);
    }
    
}
