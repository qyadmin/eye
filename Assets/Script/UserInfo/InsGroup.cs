using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InsGroup
{
    public Transform Parent;
    public GameObject item;
    List<GameObject> ItemGroup = new List<GameObject>();

    public void Remove(GameObject obj)
    {
        ItemGroup.Remove(obj);
    }

    public GameObject AddItem()
    {
        GameObject obj = GameObject.Instantiate(item);
        ItemGroup.Add(obj);
        obj.transform.SetParent(Parent);
        obj.transform.localScale = Vector3.one;
        obj.SetActive(true);
        return obj;
    }

    public void Clear()
    {      
        while (ItemGroup.Count > 0)
        {
            GameObject obj = ItemGroup[0];
            ItemGroup.Remove(obj);
            ObjectPool.GetInstance().RecycleObj(obj);
        }
        ItemGroup.Clear();
    }

}
