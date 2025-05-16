using UnityEngine;

//Logic for player turninig on movement
public class PlayerTurning : MonoBehaviour
{
    public float turningSpeed = 100f;

    void Update() {
        Vector2 lookInput = InputManager.Instance.GetMouseDelta();
        float yaw = lookInput.x * turningSpeed * Time.deltaTime;
        transform.Rotate(Vector3.up * yaw);
    }
}
