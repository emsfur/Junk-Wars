using UnityEngine;

public class InputManager : MonoBehaviour 
{
    // Start is called before the first frame update
   
   
    private static InputManager _instance;
    public static InputManager Instance {
        get {
            return _instance;
        } 
    }
    private PlayerControls playerControls;
    private void Awake()
    {
        if(_instance != null && _instance != this) {
            //Destroy(this.gameObject);
        }
        else {
            _instance = this;
        }
        playerControls = new PlayerControls();
        //Cursor.visible=false;
    }
    private void OnEnable() {
        playerControls.Enable();
    }
    private void OnDisable()
    {
        playerControls.Disable();
    } 
    
    public Vector2 GetPlayerMovement() {
        return playerControls.Player.Move.ReadValue<Vector2>();

    }
    public Vector2 GetMouseDelta() {
        return playerControls.Player.Look.ReadValue<Vector2>();
    }
    public bool PlayerJumpedThisFrame() {
        return playerControls.Player.Jump.triggered;
    }

    public bool PlayerInteracted() {
        return playerControls.Player.Interaction.triggered;
    }

    public bool DoorInteracted() {
        return playerControls.Player.DoorInteract.triggered;
    }
}
