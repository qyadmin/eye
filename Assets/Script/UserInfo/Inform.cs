using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using UnityEngine.UI;
public class Inform : btn_Http_BaseEvent {

    [SerializeField]
    private  InsGroup InformData;
    protected override void DoAction(ReturnHttpMessage msg)
    {
        Debug.Log("GetData");
        JsonData data = msg.Data["data"];
        InformData.Clear();
        int i=1;
        while (data.Keys.Contains(i.ToString()))
        {         
            TransformData transformData=  InformData.AddItem().GetComponent<TransformData>();
            transformData.GetObjectValue<Text>("title").text = data[i.ToString()]["title"].ToString();
            transformData.GetObjectValue<Text>("time").text = data[i.ToString()]["sj"].ToString();
            string id = data[i.ToString()]["id"].ToString();
            Button btn= transformData.GetComponent<Button>();
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(delegate() 
            {
                GameObject win = GameManager.GetGameManager.OpenWindow("Window_GGMessage");
                HttpModel http= GameManager.GetGameManager.http_body.GetTValue("GongGaoMessage");
                http.Data.AddData("search_id", id);
                http.HttpSuccessCallBack.Addlistener(delegate(ReturnHttpMessage datamsg) 
                {
                    JsonData msg_show = datamsg.Data["data"];
                    TransformData MSGdata = win.GetComponent<TransformData>();
                    MSGdata.GetObjectValue<Text>("title").text = msg_show["title"].ToString();
                    MSGdata.GetObjectValue<Text>("time").text = msg_show["sj"].ToString();
                    MSGdata.GetObjectValue<Text>("connet").text = msg_show["nr"].ToString();
                });
                http.Get();
            });
            i++;
        }
    }
}


