using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    [SerializeField] private Transform door1;
    [SerializeField] private Transform door2;

    private bool doorOpen = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ToggleDoor() {
        if (door1 != null) {
            door1.Rotate(0f, -90.0f, 0f, Space.World);
        }

        if (door2 != null) {
            door2.Rotate(0f, -90.0f, 0f, Space.World);
        }
    }


}
