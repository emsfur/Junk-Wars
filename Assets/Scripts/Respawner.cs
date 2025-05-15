using Unity.Netcode;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : NetworkBehaviour
{
    void OnTriggerEnter(Collider other) 
    {   
        if (other.CompareTag("Player")) {
            var spawnHandler = other.GetComponent<NetworkPlayerSpawnHandlers>();
            if (spawnHandler != null)
            {
                spawnHandler.HandleDeathAndRespawn();
            }
        }

    }
}
