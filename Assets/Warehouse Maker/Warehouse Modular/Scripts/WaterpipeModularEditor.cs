using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
#if UNITY_EDITOR


[CustomEditor(typeof(WaterPipeModular))]

public class WaterpipeModularEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var style = new GUIStyle(GUI.skin.button);
        style.fontSize = 14;
        style.normal.textColor = Color.yellow;

        GUILayout.Label("Water Pipes");
        DrawDefaultInspector();
        WaterPipeModular waterPipe = (WaterPipeModular)target;
        if (GUILayout.Button("Large WaterPipe"))
        {
            waterPipe.BuildNextItem(waterPipe.largeWaterPipe);
        }
        if (GUILayout.Button("Medium Waterpipe"))
        {
            waterPipe.BuildNextItem(waterPipe.mediumWaterPipe);
        }
        if (GUILayout.Button("small Waterpipe"))
        {
            waterPipe.BuildNextItem(waterPipe.smallWaterpipe);
        }

        if (GUILayout.Button("Left Corner"))
        {
            waterPipe.BuildNextItem(waterPipe.innerCorner);
        }
        if (GUILayout.Button("Right Corner"))
        {
            waterPipe.BuildNextItem(waterPipe.outerCorner);
        }

        GUILayout.Label("Destroy");

        if (GUILayout.Button("Delete Last Part",style))
        {
            if (waterPipe.itemsList.Count > 0)
                waterPipe.DeleteLastItem();
        }
    }
}
#endif