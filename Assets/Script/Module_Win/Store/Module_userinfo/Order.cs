using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
public class Order : btn_Http_BaseEvent
{
    //[SerializeField]
    //Button btn_xq;
    [SerializeField]
    private Transform WIN;
    [SerializeField]
    InsGroup GROUP,GroupT;

    [SerializeField]
    private Button[] ChangeList;
    [SerializeField]
    private GameObject Top01, Top02;
    public void ChangeSatte(bool IsNoe)
    {
        Top01.SetActive(IsNoe);
        Top02.SetActive(!IsNoe);
    }
    protected override void Start()
    {
        http = GameManager.GetGameManager.http_body.GetTValue(HttpName);
        btn.onClick.AddListener(delegate ()
        {
            ChangeSatte(true);
            GameManager.GetGameManager.OpenWindow(WIN);
            http.Data.AddData("type", "4");
            http.HttpSuccessCallBack.Addlistener(delegate (ReturnHttpMessage msg)
            {
                DoAction_M(msg);
            });
            http.Get();
        });

        //btn_xq.onClick.AddListener(delegate ()
        //{
        //    ChangeSatte(true);
        //    GameManager.GetGameManager.OpenWindow(WIN);
        //    http.Data.AddData("type", "4");
        //    http.HttpSuccessCallBack.Addlistener(delegate (ReturnHttpMessage msg)
        //    {
        //        DoAction_M(msg);
        //    });
        //    http.Get();
        //});

        ChangeList[0].onClick.AddListener(delegate ()
        {
            ChangeSatte(true);
            http.Data.AddData("type", "4");
            http.HttpSuccessCallBack.Addlistener(delegate (ReturnHttpMessage msg)
            {
                DoAction_M(msg);
            });
            http.Get();
        });

        ChangeList[1].onClick.AddListener(delegate ()
        {
            ChangeSatte(false);
            http.Data.AddData("type", "1");
            http.HttpSuccessCallBack.Addlistener(delegate (ReturnHttpMessage msg)
            {
                DoAction_Z(msg);
            });
            http.Get();
        });
        ChangeList[2].onClick.AddListener(delegate ()
        {
           ChangeSatte(false);
            http.Data.AddData("type", "2");
            http.HttpSuccessCallBack.Addlistener(delegate (ReturnHttpMessage msg)
            {
                DoAction_J(msg);
            });
            http.Get();
        });
        ChangeList[3].onClick.AddListener(delegate ()
        {
            ChangeSatte(false);
            http.Data.AddData("type", "3");
            http.HttpSuccessCallBack.Addlistener(delegate (ReturnHttpMessage msg)
            {
                DoAction_F(msg);
            });
            http.Get();
        });
    }


    //我的订单
    protected  void DoAction_M(ReturnHttpMessage msg)
    {
        if (msg.Code == HttpCode.SUCCESS)
        {
            JsonData JDdata = msg.Data["data"];
            GROUP.Clear();
            GroupT.Clear();
            if (JDdata.ToString() == "" || JDdata.Count <= 0)
                return;
            foreach (JsonData child in JDdata)
            {
                TransformData transformData = GROUP.AddItem().GetComponent<TransformData>();
                transformData.GetObjectValue<Text>("order").text = child["order_num"].ToString();
                transformData.GetObjectValue<Text>("name").text = child["goods_name"].ToString();
                transformData.GetObjectValue<Text>("time").text = child["sj"].ToString();
                transformData.GetObjectValue<Text>("state").text = child["zhuangtai"].ToString();
            }
        }

    }
    //直接订单
    protected  void DoAction_Z(ReturnHttpMessage msg)
    {
        if (msg.Code == HttpCode.SUCCESS)
        {
            JsonData JDdata = msg.Data["data"];
            GroupT.Clear();
            GROUP.Clear();
            if (JDdata.ToString() == "" || JDdata.Count <= 0)
                return;
            foreach (JsonData child in JDdata)
            {
                TransformData data = GroupT.AddItem().GetComponent<TransformData>();
                data.GetObjectValue<Text>("tel").text = child["tel"].ToString();
                data.GetObjectValue<Text>("money").text = child["fy_money"].ToString();
                data.GetObjectValue<Text>("time").text = child["sj"].ToString();
            }
        }

    }

    //间接订单
    protected  void DoAction_J(ReturnHttpMessage msg)
    {
        if (msg.Code == HttpCode.SUCCESS)
        {
            JsonData JDdata = msg.Data["data"];
            GroupT.Clear();
            GROUP.Clear();
            if (JDdata.ToString() == "" || JDdata.Count <= 0)
                return;
            foreach (JsonData child in JDdata)
            {
                TransformData data = GroupT.AddItem().GetComponent<TransformData>();
                data.GetObjectValue<Text>("tel").text = child["tel"].ToString();
                data.GetObjectValue<Text>("money").text = child["fy_money"].ToString();
                data.GetObjectValue<Text>("time").text = child["sj"].ToString();
            }
        }

    }

    //复销订单
    protected  void DoAction_F(ReturnHttpMessage msg)
    {
        if (msg.Code == HttpCode.SUCCESS)
        {
            JsonData JDdata = msg.Data["data"];
            GroupT.Clear();
            GROUP.Clear();
            if (JDdata.ToString() == "" || JDdata.Count <= 0)
                return;
            foreach (JsonData child in JDdata)
            {
                TransformData data = GroupT.AddItem().GetComponent<TransformData>();
                data.GetObjectValue<Text>("tel").text = child["tel"].ToString();
                data.GetObjectValue<Text>("money").text = child["fy_money"].ToString();
                data.GetObjectValue<Text>("time").text = child["sj"].ToString();
            }
        }

    }
}
