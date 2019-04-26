using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using UnityEngine.UI;
public class KeFu : btn_Http_BaseEvent {

    [SerializeField]
    private Text text;
    protected override void DoAction(ReturnHttpMessage msg)
    {
        if (msg.Code == HttpCode.SUCCESS)
        {
            JsonData jd = msg.Data["data"];
            text.text = "微信："+jd["wx"].ToString() + "\n"+ "QQ：" + jd["qq"].ToString();
        }

    }
}
