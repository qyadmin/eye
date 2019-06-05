using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NumFollow : MonoBehaviour {


    [SerializeField]
    Text TEXT;
    [SerializeField]
    InputField inputTEXT;
    public void Change(InputField INPUT)
    {
        TEXT.text = (int.Parse(INPUT.text)*10).ToString();
    }

    public void ChangeInput(InputField INPUT)
    {
        float bili =float.Parse( Static.Instance.GetValue("zs_bilv"));
        inputTEXT.text = (int.Parse(INPUT.text) * bili).ToString();
    }

}
