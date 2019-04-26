using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
public class LetterBox : btn_Http_BaseEvent {


    [SerializeField]
    InsGroup Group;

    private HttpModel http_look;

    [SerializeField]
    private Transform win_nr;

    protected override void Start()
    {
        base.Start();
        http_look = GameManager.GetGameManager.http_body.GetTValue("ajax_ly_get.php");
    }

    protected override void DoAction(ReturnHttpMessage msg)
    {
        if (msg.Code == HttpCode.SUCCESS)
        {
            Group.Clear();
            JsonData data = msg.Data["data"];
            if (data.Count <= 0)
                return;
          
            foreach (JsonData child in data)
            {
                TransformData transformData = Group.AddItem().GetComponent<TransformData>().GetBody();
                // ConfigManager.GetConfigManager.SetIamge(transformData.GetObjectValue<Image>("icon"), int.Parse(child["img"].ToString()));
                LoadImage.GetLoadIamge.Load(child["img"].ToString(), new RawImage[] { transformData.GetObjectValue<RawImage>("icon") });
                transformData.GetObjectValue<Text>("name").text = child["name"].ToString();
                Button btn = transformData.GetObjectValue<Button>("ok");
                btn.onClick.AddListener(delegate ()
                {
                    http_look.Data.AddData("ly_id", child["ly_id"].ToString());
                    http_look.HttpSuccessCallBack.Addlistener(delegate(ReturnHttpMessage msgms) 
                    {
                        if (msgms.Code == HttpCode.SUCCESS)
                        {
                            GameManager.GetGameManager.OpenWindow(win_nr);
                            TransformData transformnr = win_nr.GetComponent<TransformData>().GetBody();
                            transformnr.GetObjectValue<Text>("nr").text=msgms.Data["data"]["nr"].ToString();
                        }
                    });
                    http_look.Get();
                });
            }
        }
    }

}
