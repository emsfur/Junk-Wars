using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[ExecuteInEditMode]

public class ACpipeModular : MonoBehaviour
{
    [HideInInspector]
    public List<GameObject> itemsList = new List<GameObject>();
    [HideInInspector]
    public GameObject largeACPipe, smallACpipe,innerCorner,outerCorner;

    private void Start()
    {
        largeACPipe = Resources.Load("Models/AC_Pipe_Long") as GameObject;
        smallACpipe = Resources.Load("Models/AC_Pipe_Medium") as GameObject;
        innerCorner = Resources.Load("Models/AC_Pipe_Side_left") as GameObject;
        outerCorner = Resources.Load("Models/AC_Pipe_Side_Right") as GameObject;
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
            print(lastItem.gameObject.transform.parent.gameObject.name);
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
