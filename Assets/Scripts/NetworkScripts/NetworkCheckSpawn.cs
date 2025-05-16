using UnityEngine;
using Unity.Netcode;
using System;

//Logic for spawning and despawning playable characters
public class NetworkCheckSpawn : NetworkBehaviour
{

    public static event Action<GameObject> OnPlayerSpawn;
    public static event Action<GameObject> OnPlayerDespawn;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public override void OnNetworkSpawn()
    {
        
        base.OnNetworkSpawn();
        OnPlayerSpawn?.Invoke(this.gameObject);
        Debug.Log("Spawn");
    }
    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
        OnPlayerDespawn?.Invoke(this.gameObject);
        Debug.Log("Despawn");
    }
}
