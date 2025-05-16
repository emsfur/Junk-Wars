using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.InputSystem;

public class NetworkPlayerMovement : NetworkBehaviour
{
	//Movement logic for Playable Characters
    private void Update() {
        if (!IsOwner) return;    

        Vector3 moveDir = Vector3.zero;

        if (Input.GetKey(KeyCode.W)) moveDir += transform.forward;
        if (Input.GetKey(KeyCode.S)) moveDir -= transform.forward;
        if (Input.GetKey(KeyCode.A)) moveDir -= transform.right;
        if (Input.GetKey(KeyCode.D)) moveDir += transform.right;

        MovePlayerServerRpc(moveDir);

        RotatePlayerServerRpc(Camera.main.transform.eulerAngles.y);
        

	}

    //Moving and rotating characters over the server
    [ServerRpc]
    void MovePlayerServerRpc(Vector3 moveDir) {
        transform.position += moveDir * 3f * Time.deltaTime;
    }

    [ServerRpc]
    void RotatePlayerServerRpc(float val) {
        transform.rotation = Quaternion.Euler(0f, val, 0f);
    }

}