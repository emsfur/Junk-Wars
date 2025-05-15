using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Unity.Netcode;

public class NetworkItemSpawner : NetworkBehaviour
{   
    // contains 4 spawn zones
    [SerializeField] private GameObject ItemSpawnPoints; 
    private Vector3[] spawnPoints; 

    // populate in editor with prefabs that'll be used for scrap
    public GameObject[] items;

    public override void OnNetworkSpawn() {
        if (!IsServer) return;

        // go through all 4 spawn zones
        for (int i = 0; i < 4; i++) {
            // target the zone as a gameobject
            GameObject spawnZone = ItemSpawnPoints.transform.GetChild(i).gameObject;
            
            // track all the spawn points within the zone
            Transform[] allChildren = spawnZone.GetComponentsInChildren<Transform>();

            // target the position of all the spawn points
            spawnPoints = allChildren
                            .Where(t => t != spawnZone.transform)   // makes sure zone component isn't considered as an item spawn point
                            .Select(child => child.position)        // takes the position value 
                            .ToArray();                             // converts output into array to store into spawnPoints

            // iterate though all the spawn points for this zone
            for (int j = 0; j < spawnPoints.Length; j++) {
                // targets one specific spawn point
                Vector3 spawnPos = spawnPoints[j];

                // locally instantiate item
                GameObject itemSpawn = Instantiate(items[0], spawnPos, Quaternion.identity);
                itemSpawn.GetComponent<Rigidbody>().isKinematic = false;

                // get the network obj component to spawn item over network
                var instanceNetworkObject = itemSpawn.GetComponent<NetworkObject>();
                instanceNetworkObject.Spawn();   
            }
        }        

    }
}