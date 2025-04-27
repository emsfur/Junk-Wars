using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEditor;
#if UNITY_EDITOR


[CustomEditor(typeof(WarehouseModular))]
public class WarehouseModularEditor : Editor
{
    public override void OnInspectorGUI()
    {
        var style = new GUIStyle(GUI.skin.button);
        style.fontSize = 14;
        style.normal.textColor = Color.yellow;

        DrawDefaultInspector();
        GUILayout.Label("Walls & Corners");
        WarehouseModular warehouseModular = (WarehouseModular)target;
        if(GUILayout.Button("Large Wall"))
        {
            warehouseModular.BuildNextItem(warehouseModular.largeWall);
        }
        if (GUILayout.Button("Medium Wall"))
        {
            warehouseModular.BuildNextItem(warehouseModular.mediumWall);
        }
        if (GUILayout.Button("small Wall"))
        {
            warehouseModular.BuildNextItem(warehouseModular.smallWall);
        }
        if (GUILayout.Button("Mini Wall"))
        {
            warehouseModular.BuildNextItem(warehouseModular.miniWall);
        }

        if (GUILayout.Button("Tiny Wall"))
        {
            warehouseModular.BuildNextItem(warehouseModular.tinyWall);
        } 

        if (GUILayout.Button("Left Corner"))
        {
            warehouseModular.BuildNextItem(warehouseModular.innerCorner);
        }
        if (GUILayout.Button("Right Corner"))
        {
            warehouseModular.BuildNextItem(warehouseModular.outerCorner);
        }

        GUILayout.Label("Doors & Windows");
        if (GUILayout.Button("Garage Frame"))
        {
            warehouseModular.BuildNextItem(warehouseModular.garageFrame);
        }

        if (GUILayout.Button("Door "))
        {
            warehouseModular.BuildNextItem(warehouseModular.doorFrame);
        }
        if (GUILayout.Button("Double Door"))
        {
            warehouseModular.BuildNextItem(warehouseModular.doubleDoorFrame);
        }

        if (GUILayout.Button("Window"))
        {
            warehouseModular.BuildNextItem(warehouseModular.windowWall);
        }

        if (GUILayout.Button("Small Window"))
        {
            warehouseModular.BuildNextItem(warehouseModular.smallWindowWall);
        }

        GUILayout.Label("Destroy");

        if (GUILayout.Button("Delete Last Part",style))

        {
            if(warehouseModular.itemsList.Count > 0)
                warehouseModular.DeleteLastItem();
        }

    }


}
#endif