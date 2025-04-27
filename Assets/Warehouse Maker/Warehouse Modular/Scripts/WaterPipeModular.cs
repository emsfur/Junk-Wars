using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[ExecuteInEditMode]
public class WaterPipeModular : MonoBehaviour
{
    [HideInInspector]
    public List<GameObject> itemsList = new List<GameObject>();
    [HideInInspector]
    public GameObject largeWaterPipe, mediumWaterPipe,smallWaterpipe,innerCorner,outerCorner;

    private void Start()
    {
        largeWaterPipe = Resources.Load("Models/Water_Pipe_Long") as GameObject;
        mediumWaterPipe = Resources.Load("Models/Water_Pipe_Medium") as GameObject;
        smallWaterpipe = Resources.Load("Models/Water_Pipe_Small") as GameObject;
        innerCorner = Resources.Load("Models/Water_Pipe_left") as GameObject;
        outerCorner = Resources.Load("Models/Water_Pipe_right") as GameObject;
    }

    public void BuildNextItem(GameObject item)
    {
        if (itemsList.Count == 0)
        {
            GameObject first = Instantiate(item, transform.position, item.transform.rotation);
            first.transform.SetParent(transform);
            itemsList.Add(first);
        }
        else
        {
            Transform lastItem = itemsList.Last<GameObject>().transform.GetChild(0);
            GameObject tmp = Instantiate(item, lastItem.position, lastItem.rotation);
            tmp.transform.SetParent(transform);
            itemsList.Add(tmp);
        }

    }

    public void DeleteLastItem()
    {
        GameObject lastItem = itemsList.Last<GameObject>();
        if (Application.isPlaying)
            Destroy(lastItem);
        if (Application.isEditor)
            DestroyImmediate(lastItem);
        itemsList.Remove(lastItem);
    }
}
