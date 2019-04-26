using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ReMember : MonoBehaviour {

    public static ReMember ins;
    private void Awake()
    {
        ins = this;
    }

    [SerializeField]
    Toggle remb;
    [SerializeField]
    InputField zh, mm;
    public void Start()
    {
        string str = load("ison");
        Debug.Log(str + "------ISON");
        remb.isOn = str == "True" ?true:false;
        if (remb.isOn)
        {
            zh.text = load("zh");
            mm.text = load("mm");
        }
    }

    void Save(string name,string value)
    {
        Static.Instance.DeleteFile(Application.persistentDataPath, name+".txt");
        Static.Instance.CreateFile(Application.persistentDataPath, name+".txt", value);

    }

    private string load(string name)
    {
        ArrayList infoall = Static.Instance.LoadFile(Application.persistentDataPath, name + ".txt");
        if (infoall == null)
            return string.Empty;
        String sr = null;
        foreach (string str in infoall)
        {
            sr += str;
        }
        return sr;
    }


    public void Savezp()
    {
        if (remb.isOn)
        {
            Save("zh", zh.text);
            Save("mm", mm.text);         
        }
    }

    public void ChangeState()
    {
        Save("ison", remb.isOn.ToString());
        Debug.Log(remb.isOn.ToString());
    }

}
