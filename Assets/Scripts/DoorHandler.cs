using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    public hinge hingeState;

    public enum hinge {
        leftHinge,
        rightHinge
    }

    // begins door state as closed
    private bool doorOpen = false;
    private int hingeFlip = 1;

    // Start is called before the first frame update
    void Start()
    {
        // if the door hinge is on the left, then make sure door rotates properly
        if (hingeState == hinge.leftHinge) {
            hingeFlip = -1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleDoor() {
        // if the door is open, rotate it to close
        if (doorOpen) {
            transform.Rotate(0f, 90.0f * hingeFlip, 0f, Space.World);
            doorOpen = false;
        }
        // if door is closed, rotate it to open
        else {
            transform.Rotate(0f, -90.0f * hingeFlip, 0f, Space.World);
            doorOpen = true;
        }
    }
}
