using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

//Logic for scoring in the game
public class ScoreChange : MonoBehaviour
{
    // Start is called before the first frame update
    public TextMeshProUGUI thisText;
    public int score =0;

    void Update() {
        thisText.text = score.ToString( "#,0" );
    }
}
