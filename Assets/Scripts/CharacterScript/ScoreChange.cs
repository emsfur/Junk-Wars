using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreChange : MonoBehaviour
{
    // Start is called before the first frame update
    //private Text thisText;
    //private TMP_Text thisText;
    public TextMeshProUGUI thisText;
    public int score =0;

    void Update() {

        //Debug.Log(thisText.)
        
        thisText.text = score.ToString( "#,0" );
        //thisText.text = score.ToString( "#,0" );
    }
}
