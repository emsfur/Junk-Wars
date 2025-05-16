using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WinCheck : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject gameManager;

    [SerializeField] private Timer timer;
    private PointsHandler pointsHandler;
    [SerializeField] private TextMeshProUGUI winText;
    //[SerializeField] private TextMeshProUGUI p1Points;

    void Start()
    {
        gameManager = GameObject.Find("GameManager");
        pointsHandler = gameManager.GetComponent<PointsHandler>();
        
    }
    void Update()
    {
        if (timer.timerFin() == false)
        {
            if (pointsHandler.GetP1Points() > pointsHandler.GetP2Points())
            {
                winText.text = "P1 Winner!";
            }
            if (pointsHandler.GetP1Points() < pointsHandler.GetP2Points())
            {
                winText.text = "P2 Winner!";
            }
            if (pointsHandler.GetP1Points() == pointsHandler.GetP2Points())
            {
                winText.text = "TIE LOL";
            }
        }
        else {
            winText.text = "";
        }
    }
   
}
