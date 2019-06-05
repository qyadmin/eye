using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
public class TuiGuang : btn_Http_BaseEvent
{

    [SerializeField]
    HttpModel http_bf,http_wy;
    [SerializeField]
    InsGroup GROUP;


    [SerializeField]
    private GameObject[] Hide, Show;

    [SerializeField]
    Transform win_bf,win_grzx;



    public void bf(bool IsFriend)
    {
        Static.Instance.IsFriend = IsFriend;

        if (IsFriend)
        {
            http_wy.Data.URL = "ajax_hy_wy_put.php";
            
        }
        else
        {
            http_wy.Data.URL = "ajax_wy_put.php";

        }

        foreach (GameObject child in Hide)
        {
            child.SetActive(!IsFriend);
        }
        foreach (GameObject child in Show)
        {
            child.SetActive(IsFriend);
        }
    }

    protected override void DoAction(ReturnHttpMessage msg)
    {
        if (msg.Code == HttpCode.SUCCESS)
        {
            GROUP.Clear();
            JsonData jd = msg.Data["data"];
            if (jd.Count <= 0)
                return;
            foreach (JsonData child in jd)
            {
                TransformData transformData = GROUP.AddItem().GetComponent<TransformData>().GetBody();
                //ConfigManager.GetConfigManager.SetIamge(transformData.GetObjectValue<Image>("icon"), int.Parse(child["img"].ToString()));
                LoadImage.GetLoadIamge.Load(child["img"].ToString(), new RawImage[] { transformData.GetObjectValue<RawImage>("icon") });
                transformData.GetObjectValue<Text>("ID").text = child["bianhao"].ToString();
                if (child["name"] == null)
                {
                    transformData.GetObjectValue<Text>("name").text = "暂无名称";
                }
                else
                transformData.GetObjectValue<Text>("name").text = child["name"].ToString();
                transformData.GetObjectValue<Text>("state").text = child["zhuangtai"].ToString();
                Button btn_talk = transformData.GetObjectValue<Button>("bf");
                btn_talk.onClick.AddListener(delegate ()
                {
                    GameManager.GetGameManager.CloseWindow(win_bf);
                    GameManager.GetGameManager.CloseWindow(win_grzx);
                    http_bf.Data.AddData("hy_id",child["hy_id"].ToString());
                    http_bf.Get();
                    bf(true);
                });

            }
        }
    }
}
