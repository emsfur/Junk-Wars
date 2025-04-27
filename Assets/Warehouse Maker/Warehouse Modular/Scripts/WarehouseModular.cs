using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

[ExecuteInEditMode]

public class WarehouseModular : MonoBehaviour
{
    [HideInInspector]
    public List<GameObject> itemsList = new List<GameObject>();
    [HideInInspector]
    public GameObject largeWall,mediumWall,smallWall,miniWall,
                      tinyWall,windowWall,smallWindowWall,innerCorner,
                      outerCorner,garageFrame,doorFrame,doubleDoorFrame;
    private MeshFilter myMeshFilter;

    private void Start()
    {
        myMeshFilter = GetComponent<MeshFilter>();
        largeWall = Resources.Load("Models/LargeWall")as GameObject;
        mediumWall = Resources.Load("Models/MediumWall") as GameObject;
        smallWall = Resources.Load("Models/SmallWall") as GameObject;
        miniWall = Resources.Load("Models/Extra_SmallWall") as GameObject;
        tinyWall = Resources.Load("Models/Extra_SmallWall1") as GameObject;
        windowWall = Resources.Load("Models/WindowWall") as GameObject;
        smallWindowWall = Resources.Load("Models/SmallWindowWall") as GameObject;
        innerCorner = Resources.Load("Models/LeftCorner") as GameObject;
        outerCorner = Resources.Load("Models/RightCorner") as GameObject;
        garageFrame = Resources.Load("Models/GarageDoorFrame") as GameObject;
        doorFrame = Resources.Load("Models/DoorWall") as GameObject;
        doubleDoorFrame = Resources.Load("Models/DoubleDoorWall") as GameObject;
    }

    public void BuildNextItem(GameObject item)
    {
        if (itemsList.Count == 0)
        {
            GameObject first = Instantiate(item, transform.position, item.transform.rotation);
            first.transform.SetParent(transform);
            itemsList.Add(first);
        }
        else {

           Transform lastItem = itemsList.Last<GameObject>().transform.GetChild(0);
           GameObject tmp =  Instantiate(item, lastItem.position, lastItem.rotation);
           tmp.transform.SetParent(transform);
           itemsList.Add(tmp);
        }

    }

    public void DeleteLastItem()
    {
        GameObject lastItem = itemsList.Last<GameObject>();
        if(Application.isPlaying)
            Destroy(lastItem);
        if (Application.isEditor)
            DestroyImmediate(lastItem);
        itemsList.Remove(lastItem);
    }


}
