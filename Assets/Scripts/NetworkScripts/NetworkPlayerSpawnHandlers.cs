using Unity.Netcode;
using UnityEngine;
using System.Collections.Generic;

public class NetworkPlayerSpawnHandlers : NetworkBehaviour
{   
    private GameObject PlayerSpawnPoints;
    Transform spawnPoint1;
    Transform spawnPoint2;  

    public override void OnNetworkSpawn() {
        // when the player spawns, find both spawn points
        PlayerSpawnPoints = GameObject.Find("PlayerSpawns");
        spawnPoint1 = PlayerSpawnPoints.transform.GetChild(0);
        spawnPoint2 = PlayerSpawnPoints.transform.GetChild(1);

        // move the player to spawn point based on if client/host
        Respawn();
    }

    public void Respawn()
    {
        if (IsHost) {
            transform.position = spawnPoint1.position;
        }
        else if (IsClient) {
            transform.position = spawnPoint2.position;
        }  
    }

}
