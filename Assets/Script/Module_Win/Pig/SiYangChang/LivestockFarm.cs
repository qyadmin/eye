using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using UnityEngine.UI;
public class LivestockFarm : MonoBehaviour
{


    [SerializeField]
    private Transform win_sy;
    [SerializeField]
    Model3DButton BTN;
    [SerializeField]
    HttpModel cs_hhtp, ck_http, cz_http, wy_http, ys_http, chanzai_http, sj_http;

    HttpModel http_syc;
    [SerializeField]
    Transform win_xd, win_wy, win_up;

    [SerializeField]
    Transform PigModel;
    List<GameObject> PigGroup = new List<GameObject>();

    [SerializeField]
    InsGroup GROUP;
    public void Start()
    {
        http_syc = GameManager.GetGameManager.http_body.GetTValue("ajax_syc_list.php");
        BTN.onClick.Addlistener(delegate ()
        {
            Debug.Log("***************");
            UpdateSyc();
        });

        foreach (Transform child in PigModel)
        {
            PigGroup.Add(child.gameObject);
        }
        foreach (GameObject child in PigGroup)
        {
            child.SetActive(false);
        }

        UpdateModelPig();
    }


    public void UpdateModelPig()
    {
        http_syc.HttpSuccessCallBack.Addlistener(delegate (ReturnHttpMessage msg)
        {
            if (msg.Code == HttpCode.SUCCESS)
            {
                DoAction(msg.Data);
            }
        });
        http_syc.Get();
    }


    private void UpdateSyc()
    {
        GameManager.GetGameManager.OpenWindow(win_sy);
        http_syc.HttpSuccessCallBack.Addlistener(delegate (ReturnHttpMessage msg)
        {
            if (msg.Code == HttpCode.SUCCESS)
            {
                DoAction(msg.Data);
            }
        });
        http_syc.Get();
    }

    GameObject curenobj = null;
    public void Clear()
    {
        if (curenobj != null)
            curenobj.SetActive(false);
    }
    void DoAction(JsonData data)
    {
        int num = 0;

        foreach (GameObject child in PigGroup)
        {
            child.SetActive(false);
        }

        GROUP.Clear();
        JsonData jd = data["data"];
        if (jd.Count <= 0)
            return;
        bool IsFried = Static.Instance.IsFriend;


        foreach (JsonData child in jd)
        {
            TransformData transformData = GROUP.AddItem().GetComponent<TransformData>().GetBody();
            //  Button btn_icon = transformData.GetObjectValue<Button>("icon_btn");
            Button btn_ck = transformData.GetObjectValue<Button>("ck");
            Button btn_cs = transformData.GetObjectValue<Button>("cs");
            Button btn_sj = transformData.GetObjectValue<Button>("sj");
            Button btn_ys = transformData.GetObjectValue<Button>("ys");
            Button btn_wy = transformData.GetObjectValue<Button>("wy");
            Button btn_cz = transformData.GetObjectValue<Button>("cz");
            Image num_cz = transformData.GetObjectValue<Image>("num_cz");
            Image num_jk = transformData.GetObjectValue<Image>("num_jk");
            Text bili_cz = transformData.GetObjectValue<Text>("bili_cz");
            Text bili_zk = transformData.GetObjectValue<Text>("bili_zk");

            Text offer_id = transformData.GetObjectValue<Text>("offer_id");
            //Text zhu_id = transformData.GetObjectValue<Text>("zhu_id");
            Text money = transformData.GetObjectValue<Text>("money");
            Text shouyi = transformData.GetObjectValue<Text>("shouyi");
            Text creat_time = transformData.GetObjectValue<Text>("creat_time");
            Text start_time = transformData.GetObjectValue<Text>("start_time");
            Text end_time = transformData.GetObjectValue<Text>("end_time");

            offer_id.text = child["offer_id"].ToString();
            money.text = child["money"].ToString();
            shouyi.text = child["shouyi"].ToString();
            creat_time.text = child["creat_time"].ToString();
            start_time.text = child["start_time"].ToString();
            end_time.text = child["end_time"].ToString();

            //bili_cz.text = child["ccz"].ToString() + "/100";
            //bili_zk.text = child["jkz"].ToString() + "/100";
            //num_cz.fillAmount = float.Parse(child["ccz"].ToString()) / 100;
            //num_jk.fillAmount = float.Parse(child["jkz"].ToString()) / 100;

            //升级


            btn_sj.onClick.AddListener(delegate ()
            {
                GameManager.GetGameManager.OpenWindow(win_up);
                TransformData ts = win_up.GetComponent<TransformData>();

                GetButton(ts, "j").onClick.AddListener(delegate ()
                 {
                     sj_http.HttpSuccessCallBack.Addlistener(delegate (ReturnHttpMessage msg)
                     {
                         if (msg.Code == HttpCode.SUCCESS)
                             UpdateSyc();
                     });

                     sj_http.Data.AddData("sy_id", child["sy_id"].ToString());
                     sj_http.Data.AddData("type_id", "1");
                     sj_http.Get();
                 });

                GetButton(ts, "z").onClick.AddListener(delegate ()
                {
                    sj_http.HttpSuccessCallBack.Addlistener(delegate (ReturnHttpMessage msg)
                    {
                        if (msg.Code == HttpCode.SUCCESS)
                            UpdateSyc();
                    });
                    sj_http.Data.AddData("sy_id", child["sy_id"].ToString());
                    sj_http.Data.AddData("type_id", "2");
                    sj_http.Get();
                });
            });


            btn_cs.gameObject.SetActive(!IsFried);
            //出售
            btn_cs.onClick.AddListener(delegate ()
            {
                cs_hhtp.HttpSuccessCallBack.Addlistener(delegate (ReturnHttpMessage msg)
                {
                    if (msg.Code == HttpCode.SUCCESS)
                        UpdateSyc();
                });
                cs_hhtp.Data.AddData("sy_id", child["sy_id"].ToString());
                cs_hhtp.Data.AddData("money", child["money"].ToString());
                cs_hhtp.Data.AddData("txflag", "1");
                cs_hhtp.Get();
            });


            btn_ys.gameObject.SetActive(!IsFried);


            //药水
            btn_ys.onClick.AddListener(delegate ()
            {
                GameManager.GetGameManager.OpenWindow(win_xd);
                TransformData ts = win_xd.GetComponent<TransformData>();

                GetButton(ts, "diji").onClick.AddListener(delegate ()
                 {
                     ys_http.HttpSuccessCallBack.Addlistener(delegate (ReturnHttpMessage msg)
                     {
                         if (msg.Code == HttpCode.SUCCESS)
                             UpdateSyc();
                     });
                     ys_http.Data.AddData("sy_id", child["sy_id"].ToString());
                     ys_http.Data.AddData("ysflag", "0");
                     ys_http.Get();
                 });

                GetButton(ts, "gaoji").onClick.AddListener(delegate ()
                {
                    ys_http.HttpSuccessCallBack.Addlistener(delegate (ReturnHttpMessage msg)
                    {
                        if (msg.Code == HttpCode.SUCCESS)
                            UpdateSyc();
                    });
                    ys_http.Data.AddData("sy_id", child["sy_id"].ToString());
                    ys_http.Data.AddData("ysflag", "1");
                    ys_http.Get();
                });
            });

            //喂养
            btn_wy.onClick.AddListener(delegate ()
            {

                GameManager.GetGameManager.OpenWindow(win_wy);
                TransformData ts = win_wy.GetComponent<TransformData>();

                GetButton(ts, "diji").onClick.AddListener(delegate ()
                 {
                     wy_http.HttpSuccessCallBack.Addlistener(delegate (ReturnHttpMessage msg)
                     {
                         if (msg.Code == HttpCode.SUCCESS)
                             UpdateSyc();
                     });
                     wy_http.Data.AddData("sy_id", child["sy_id"].ToString());
                     wy_http.Data.AddData("slflag", "1");
                     wy_http.Get();
                 });

                GetButton(ts, "gaoji").onClick.AddListener(delegate ()
                 {
                     wy_http.HttpSuccessCallBack.Addlistener(delegate (ReturnHttpMessage msg)
                     {
                         if (msg.Code == HttpCode.SUCCESS)
                             UpdateSyc();
                     });
                     wy_http.Data.AddData("sy_id", child["sy_id"].ToString());
                     wy_http.Data.AddData("slflag", "2");
                     wy_http.Get();
                 });
            });

            btn_cz.gameObject.SetActive(!IsFried);
            //产仔
            btn_cz.onClick.AddListener(delegate ()
            {
                chanzai_http.Data.AddData("sy_id", child["sy_id"].ToString());
                chanzai_http.Get();
            });

            //查看
            btn_ck.onClick.AddListener(delegate ()
            {
                ck_http.HttpSuccessCallBack.Addlistener(delegate (ReturnHttpMessage msg)
                {
                    if (msg.Code == HttpCode.SUCCESS)
                        UpdateSyc();
                });
                ck_http.Data.AddData("offer_id", child["offer_id"].ToString());
                ck_http.Get();
            });

            TransformData PIGTS = null;
            if (num < PigModel.childCount)
            {

                PIGTS = PigGroup[num].GetComponent<TransformData>();
                PigGroup[num].SetActive(true);
                num++;
            }
            /*
            if (child["type_id"].ToString() == "钻石猪")
            {
                transformData.GetObjectValue<Image>("z").gameObject.SetActive(true);
                if (PIGTS != null)
                {
                    PIGTS.GetObjectValue<Transform>("MAOZI").gameObject.SetActive(true);
                    PIGTS.GetObjectValue<Transform>("YIFU").gameObject.SetActive(true);
                }

            }

            if (child["type_id"].ToString() == "金猪")
            {
                transformData.GetObjectValue<Image>("j").gameObject.SetActive(true);
                if (PIGTS != null)
                {
                    PIGTS.GetObjectValue<Transform>("YIFU").gameObject.SetActive(true);
                }

            }

            if (child["type_id"].ToString() == "银猪")
            {
                if (!IsFried)
                    btn_sj.gameObject.SetActive(true);

                transformData.GetObjectValue<Image>("y").gameObject.SetActive(true);
            }
            */
        }
    }


    private Button GetButton(TransformData ts, string btnname)
    {
        Button btn = ts.GetObjectValue<Button>(btnname);
        btn.onClick.RemoveAllListeners();
        return btn;
    }
}
