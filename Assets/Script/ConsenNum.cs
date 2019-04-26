using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ConsenNum : MonoBehaviour {

    InputField CUR;
    private void Start()
    {
        CUR = GetComponent<InputField>();
        CUR.text = "1";
    }
    public void ChangeNum(InputField input)
    {
        string STR = CUR.text == string.Empty ? "0" : CUR.text;
        input.text = int.Parse(STR).ToString();
    }

}
