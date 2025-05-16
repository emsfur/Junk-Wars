using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnData : MonoBehaviour
{
    public bool spawnUsed = false;

    public bool isSpawnUsed() {
        return spawnUsed;
    }

    public void setSpawnUsed(bool val) {
        spawnUsed = val;
    }
}
