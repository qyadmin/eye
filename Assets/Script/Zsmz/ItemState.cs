using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemState : MonoBehaviour
{

    public GameObject[] StateGameObjects;

    public Text State;

    public Text Id;

    public HttpModel Activate;

    public void SetState()
    {
        HideAllState();
        switch (State.text)
        {
            case "未激活":
                StateGameObjects[1].SetActive(true);
                break;
                break;
            case "已激活":
                StateGameObjects[0].SetActive(true);
                break;
        }
        var jihuo = StateGameObjects[1].GetComponent<Button>();
        jihuo.onClick.AddListener(delegate ()
        {
            Activate.Data.AddData("hy_id", Id.text);
            Activate.Get();
        });
    }

    private void HideAllState()
    {
        foreach (var item in StateGameObjects)
        {
            item.SetActive(false);
        }
    }
}