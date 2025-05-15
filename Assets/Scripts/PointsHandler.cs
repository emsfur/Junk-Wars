using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PointsHandler : NetworkBehaviour
{
    public override void OnNetworkSpawn() {
        Debug.Log("I'm ALIVE");
    }
}
