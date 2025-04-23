using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
#if UNITY_EDITOR

[CustomEditor(typeof(ACpipeModular))]
public class ACpipeModularEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var style = new GUIStyle(GUI.skin.button);
        style.fontSize = 14;
        style.normal.textColor = Color.yellow;

        GUILayout.Label("AC Pipes");
        DrawDefaultInspector();
        ACpipeModular acPipe = (ACpipeModular)target;
        if (GUILayout.Button("Large AC pipe"))
        {
            acPipe.BuildNextItem(acPipe.largeACPipe);
        }

        if (GUILayout.Button("small AC pipe"))
        {
            acPipe.BuildNextItem(acPipe.smallACpipe);
        }

        if (GUILayout.Button("Left Corner"))
        {
            acPipe.BuildNextItem(acPipe.innerCorner);
        }
        if (GUILayout.Button("Right Corner"))
        {
            acPipe.BuildNextItem(acPipe.outerCorner);
        }

        GUILayout.Label("Destroy");

        if (GUILayout.Button("Delete Last Part",style))
        {
            if (acPipe.itemsList.Count > 0)
                acPipe.DeleteLastItem();
        }
    }

}
#endif