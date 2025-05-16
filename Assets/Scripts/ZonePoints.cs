using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZonePoints : MonoBehaviour
{
    public int player = 1;
    private GameObject gameManager;
    private PointsHandler pointsHandler;

    void Start() {
        gameManager = GameObject.Find("GameManager");
        pointsHandler = gameManager.GetComponent<PointsHandler>();
    }

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Scrap") {
            pointsHandler.UpdatePoints(player, 1);
        }
    }

    void OnTriggerExit(Collider other) {
        if (other.gameObject.tag == "Scrap") {
            pointsHandler.UpdatePoints(player, -1);
        }
    }
}
