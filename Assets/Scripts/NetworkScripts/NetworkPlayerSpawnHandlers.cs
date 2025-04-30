using Unity.Netcode;
using UnityEngine;
using System.Collections.Generic;

public class NetworkPlayerSpawnHandlers : NetworkBehaviour
{   
    public GameObject PlayerSpawnPoints;

    private Vector3[] spawnPoints; 
    
    private NetworkVariable<int> playerNum = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone);

    void Awake() {

         Transform[] allChildren = PlayerSpawnPoints.GetComponentsInChildren<Transform>();
         List<Vector3> collectedSpawnPoints = new List<Vector3>();        

         foreach (Transform child in allChildren)
            {
                if (child != PlayerSpawnPoints.transform) // Skip the root object itself
{
                    collectedSpawnPoints.Add(child.position);
                    Debug.Log($"Spawn Point: {child.name} at {child.position}");
            }
        }
        spawnPoints = collectedSpawnPoints.ToArray();
    }




    public override void OnNetworkSpawn() {
        int spawnIndex = (int)OwnerClientId % spawnPoints.Length;
        transform.position = spawnPoints[spawnIndex];
        
    }


}
