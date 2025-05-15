using Unity.Netcode;
using UnityEngine;
using System.Collections.Generic;

public class NetworkPlayerSpawnHandlers : NetworkBehaviour
{   
    private GameObject PlayerSpawnPoints;

    public override void OnNetworkSpawn() {
        // when the player spawns, find both spawn points
        PlayerSpawnPoints = GameObject.Find("PlayerSpawns");
        Transform spawnPoint1 = PlayerSpawnPoints.transform.GetChild(0);
        Transform spawnPoint2 = PlayerSpawnPoints.transform.GetChild(1);

        // move the player to spawn point based on if client/host
        if (IsHost) {
            transform.position = spawnPoint1.position;
        }
        else if (IsClient) {
            Debug.Log("ems: This is clident");
            transform.position = spawnPoint2.position;
        }
        
        
    }


}
