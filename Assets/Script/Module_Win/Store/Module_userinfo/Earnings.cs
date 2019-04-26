using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using UnityEngine.UI;
public class Earnings : btn_Http_BaseEvent {


    [SerializeField]
    private Transform WIN;
    [SerializeField]
    InsGroup GROUP;
    protected override void Start()
    {
        base.Start();
        btn.onClick.AddListener(delegate() 
        {
            GameManager.GetGameManager.OpenWindow(WIN);
        });
    }


    protected override void DoAction(ReturnHttpMessage msg)
    {
        if (msg.Code == HttpCode.SUCCESS)
        {
            JsonData JDdata = msg.Data["data"];
            GROUP.Clear();
            foreach (JsonData child in JDdata)
            {
                TransformData data = GROUP.AddItem().GetComponent<TransformData>();
                data.GetObjectValue<Text>("coin").text = child["fy_money"].ToString();
                data.GetObjectValue<Text>("time").text = child["sj"].ToString();
                data.GetObjectValue<Text>("account").text = child["tel"].ToString();
            }
        }
    }
}
