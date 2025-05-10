using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkItemSpawner : NetworkBehaviour
{
    public GameObject ItemSpawnPoints;
    public GameObject BookObject;
    public GameObject CardboardBoxObject;
    private Vector3[] spawnPoints; 




    void Awake() {
        Transform[] allChildren = ItemSpawnPoints.GetComponentsInChildren<Transform>();
        List<Vector3> collectedSpawnPoints = new List<Vector3>();

        foreach (Transform child in allChildren) {
            if (child != ItemSpawnPoints.transform) {
                    collectedSpawnPoints.Add(child.position);
            }
        }
        spawnPoints = collectedSpawnPoints.ToArray();
    }



    public override void OnNetworkSpawn() {


        int i =0;
       // if(IsServer)
       // {   
            Debug.Log("Spawning");
            foreach (Vector3 spawnPos in spawnPoints) {
               
                if (i % 2 == 0) {
                     GameObject itemSpawn= Instantiate(BookObject, spawnPos, Quaternion.identity);
                     itemSpawn.GetComponent<Rigidbody>().isKinematic = false;
                     Debug.Log("Print Book");
                     
                }
                if (i % 2 == 1) {
                     GameObject itemSpawn= Instantiate(CardboardBoxObject, spawnPos, Quaternion.identity);
                     itemSpawn.GetComponent<Rigidbody>().isKinematic = false;
                     Debug.Log("Print Box");
                }
               
                i+=1;
           }
       // }

    }
}