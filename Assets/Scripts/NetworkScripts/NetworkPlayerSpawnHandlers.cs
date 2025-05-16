using Unity.Netcode;
using UnityEngine;
using System.Collections.Generic;

public class NetworkPlayerSpawnHandlers : NetworkBehaviour
{
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private AudioSource audioSource;

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

    //Spawns players on different zones depending on host or client
    public void Respawn()
    {
        if (IsHost) {
            transform.position = spawnPoint1.position;
        }
        else if (IsClient) {
            transform.position = spawnPoint2.position;
        }  
    }

    //Logic for respawning player on death and plays death sound
    public void HandleDeathAndRespawn()
    {
        PlayDeathSoundClientRpc();
        Respawn();
    }

    //Logic for playing sound on player death
    [ClientRpc]
    private void PlayDeathSoundClientRpc()
    {
        if (!IsOwner) return; 

        if (deathSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(deathSound);
        }
    }
}
