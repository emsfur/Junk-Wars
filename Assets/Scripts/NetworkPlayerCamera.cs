using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Cinemachine;

public class NetworkPlayerCamera : NetworkBehaviour
{
    private CinemachineVirtualCamera virtualCam;
    

    public override void OnNetworkSpawn()
    {
        if (IsOwner) {
            SetupVirtualCam();
            
            // disables cursor
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }


    }

    // ties player to virtual cam (cinemachine)
    private void SetupVirtualCam()
    {
        if (!IsOwner) return;

        virtualCam = FindObjectOfType<CinemachineVirtualCamera>();
        virtualCam.Follow = transform;
    }
}
