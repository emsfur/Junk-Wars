using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawner : MonoBehaviour
{


    void OnTriggerEnter(Collider other) 
    {   
        if (other.gameObject.tag == "Player") {
            // other.gameObject.GetComponent<NetworkPlayerSpawnHandlers>().Respawn() ();
        }
    }
}
