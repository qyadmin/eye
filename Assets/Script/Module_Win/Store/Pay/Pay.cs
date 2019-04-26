using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Pay : MonoBehaviour {

    [SerializeField]
    Button[] PayGroup;
    [SerializeField]
    Text text;
    public PayPay PayAction;

    void Start()
    {
        int i = 0;
        foreach (Button child in PayGroup)
        {
            child.onClick.AddListener(delegate() 
            {
                text.text = child.gameObject.name;
                PayAction.Type = i;
                GameManager.GetGameManager.CloseWindow(transform);
            });
            i++;
        }
    }

}
