using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneRefresh : MonoBehaviour
{

    public Transform Content;

    public void RefreshItemView()
    {
        var childs = Content.GetComponentsInChildren<ItemState>();
        foreach (var item in childs)
        {
            item.SetState();
        }
    }
}
