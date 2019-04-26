using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
public class ChanFang : btn_Http_BaseEvent
{

    [SerializeField]
    InsGroup GROUP;

    [SerializeField]
    Text brithday, bili_ccz, bili_jkz;
    [SerializeField]
    Image czz, jkz;
    [SerializeField]
    Button dz, wy, zs, sy;

    [SerializeField]
    HttpModel sy_http, wy_http, dz_http, zs_http;

    [SerializeField]
    LivestockFarm pig_refrece;

    [SerializeField]
    Transform win_zs, win_dz;

    [SerializeField]
    Button brn_hy_cf;


    [SerializeField]
    Transform cf;
    [SerializeField]
    GameObject cfblank;
    protected override void Start()
    {
        base.Start();
        http = GameManager.GetGameManager.http_body.GetTValue(HttpName);
        brn_hy_cf.onClick.AddListener(delegate ()
        {
            http.HttpSuccessCallBack.Addlistener(delegate (ReturnHttpMessage msg)
            {
                DoAction(msg);
            });
            http.Get();
        });
    }

    bool isoepn = false;
    protected override void DoAction(ReturnHttpMessage msg)
    {
        wy.gameObject.SetActive(false);
        dz.gameObject.SetActive(false);
        zs.gameObject.SetActive(false);
        if (msg.Code != HttpCode.SUCCESS)
            return;

        bool isFriend = Static.Instance.IsFriend;

        GROUP.Clear();
        JsonData jd = msg.Data["data"];

        Debug.Log(JsonMapper.ToJson(jd));

        if (jd.Count <= 0)
        {
            cfblank.SetActive(false);
            return;
        }
        cfblank.SetActive(true);

        isoepn = true;
        foreach (JsonData child in jd)
        {
            TransformData transformData = GROUP.AddItem().GetComponent<TransformData>().GetBody();
            Button btn_ck = transformData.GetObjectValue<Button>("icon");
           


            btn_ck.onClick.AddListener(delegate ()
            {
                SetData(child, transformData);
            });

        }
       
        SetData(jd[0],GROUP.Parent.GetChild(1).GetComponent<TransformData>());

    }

    TransformData CurrentBtn = null;
    private void SetData(JsonData child, TransformData btn)
    {
        bool isFriend = Static.Instance.IsFriend;
        if (float.Parse(child["ccz"].ToString()) >= 10)
        {
            if (!isFriend)
            {
                sy.gameObject.SetActive(true);
                sy.onClick.RemoveAllListeners();
                sy.onClick.AddListener(delegate ()
                {
                    sy_http.HttpSuccessCallBack.Addlistener(delegate (ReturnHttpMessage ms)
                    {
                        if (ms.Code == HttpCode.SUCCESS)
                        {
                            pig_refrece.Start();
                              UpdateSyc();
                        }
                           
                    });
                    sy_http.Get();
                });
            }
        }
        else
        {
            sy.gameObject.SetActive(false);
        }
        if (CurrentBtn != null)
        {
            CurrentBtn.GetBody().GetObjectValue<Image>("mark").gameObject.SetActive(false);
        }
        CurrentBtn = btn;
        Debug.Log(CurrentBtn.gameObject.name);
        CurrentBtn.GetBody().GetObjectValue<Image>("mark").gameObject.SetActive(true);

        sy_http.Data.AddData("sy_id", child["sy_id"].ToString());



        //喂养
        wy.gameObject.SetActive(true);
        wy.onClick.RemoveAllListeners();
        wy.onClick.AddListener(delegate ()
        {
            wy_http.HttpSuccessCallBack.Addlistener(delegate (ReturnHttpMessage ms)
            {
                if (ms.Code == HttpCode.SUCCESS)
                    UpdateSyc();
            });
            wy_http.Data.AddData("sy_id", child["sy_id"].ToString());
            Debug.Log(child["sy_id"].ToString());
            wy_http.Data.AddData("slflag", "0");
            wy_http.Get();

        });

        //打针
        dz.gameObject.SetActive(!isFriend);
        dz.onClick.RemoveAllListeners();
        dz.onClick.AddListener(delegate ()
        {
            GameManager.GetGameManager.OpenWindow(win_dz);
            TransformData ts = win_dz.GetComponent<TransformData>();

            GetButton(ts, "diji").onClick.AddListener(delegate ()
            {
                dz_http.HttpSuccessCallBack.Addlistener(delegate (ReturnHttpMessage ms)
                {
                    if (ms.Code == HttpCode.SUCCESS)
                        UpdateSyc();
                });
                dz_http.Data.AddData("sy_id", child["sy_id"].ToString());
                dz_http.Data.AddData("yfzflag", "0");
                dz_http.Get();
            });

            GetButton(ts, "gaoji").onClick.AddListener(delegate ()
            {
                dz_http.HttpSuccessCallBack.Addlistener(delegate (ReturnHttpMessage ms)
                {
                    if (ms.Code == HttpCode.SUCCESS)
                        UpdateSyc();
                });
                dz_http.Data.AddData("yfzflag", "1");
                dz_http.Get();
            });

        });

        //赠送
        zs.gameObject.SetActive(!isFriend);
        zs.onClick.RemoveAllListeners();
        zs.onClick.AddListener(delegate ()
        {
            GameManager.GetGameManager.OpenWindow(win_zs);
            TransformData ts = win_zs.GetComponent<TransformData>();
            ts.GetObjectValue<Button>("send_btn").onClick.AddListener(delegate ()
            {
                zs_http.Data.AddData("sy_id", child["sy_id"].ToString());
                zs_http.Get();
            });
        });



        bili_ccz.text = child["ccz"].ToString() + "/100";
        bili_jkz.text = child["jkz"].ToString() + "/100";
        czz.fillAmount = float.Parse(child["ccz"].ToString()) / 100;
        jkz.fillAmount = float.Parse(child["jkz"].ToString()) / 100;
        brithday.text = child["sj"].ToString();
    }



    private Button GetButton(TransformData ts, string btnname)
    {
        Button btn = ts.GetObjectValue<Button>(btnname);
        btn.onClick.RemoveAllListeners();
        return btn;
    }


    private void UpdateSyc()
    {
        http.HttpSuccessCallBack.Addlistener(delegate (ReturnHttpMessage msg)
        {
            if (msg.Code == HttpCode.SUCCESS)
            {
                DoAction(msg);
            }
        });
        http.Get();
    }
}



