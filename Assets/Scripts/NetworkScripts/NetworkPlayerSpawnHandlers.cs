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
        // RequestRespawnServerRpc(IsHost);   
        Respawn();
    }

    // public void RespawnP1()
    // {
    //     if (IsHost) {
    //         transform.position = spawnPoint1.position;
    //     }
    // }

    // public void RespawnP2()
    // {
    //     if (IsClient) {
    //         transform.position = spawnPoint2.position;
    //     }
    // }

    public void Respawn() 
    {
        transform.position = spawnPoint1.position;
    }

    // [ServerRpc]
    // public void RequestRespawnServerRpc(bool host)
    // {
    //     if (host) {
    //         Debug.Log("THIS RAN AGAIN");
    //         transform.position = spawnPoint1.position;
    //     }
    // }

}
