using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class AddressUpdate
{
    public InputField name;
    public InputField tel;
    public InputField address;
    public InputField zip_code;
    public InputField City;
    public ButtonState btn_moren;

    public Button Btn_Save;
    public Button Btn_Del;

    public Transform Win;
    public HttpModel httpdel;

    public void Get(HttpModel Http,GetCity citydata)
    {

    }
}
