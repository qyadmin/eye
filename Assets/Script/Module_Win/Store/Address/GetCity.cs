using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using UnityEngine.UI;
public class GetCity : btn_Http_BaseEvent
{

    [SerializeField]
    private GameObject obj;
    [SerializeField]
    private Button btn_Ok;
    [SerializeField]
    private Button btn_City;

    [SerializeField]
    private InsGroup Pri;

    [SerializeField]
    private InsGroup City;

    [SerializeField]
    private InsGroup Arex;
  
    public Text textp, textcity, textaexa;

    public string Pri_str;
    public string City_str;
    public string Aera_str;

    [SerializeField]
    private InputField input,inputT;

    private JsonData CITYDATA=null;
   
    protected override void Start()
    {
        HttpModel login = GameManager.GetGameManager.http_body.GetTValue(HttpName);
        btn.onClick.AddListener(delegate ()
        {
            if (CITYDATA != null)
            {
               // obj.SetActive(true);
                GameManager.GetGameManager.OpenWindow(obj.transform);
            }
            else
            {
                login.HttpSuccessCallBack.Addlistener(delegate (ReturnHttpMessage msg)
                {
                    DoAction(msg);
                });
                login.Get();
            }
        });

        btn_City.onClick.AddListener(delegate ()
        {
            if (CITYDATA != null)
            {
              //  obj.SetActive(true);
                GameManager.GetGameManager.OpenWindow(obj.transform);
            }
            else
            {
                login.HttpSuccessCallBack.Addlistener(delegate (ReturnHttpMessage msg)
                {
                    DoAction(msg);
                });
                login.Get();
            }
        });


        btn_Ok.onClick.AddListener(delegate ()
        {
            inputT.text= input.text = textp.text + "/" + textcity.text + "/" + textaexa.text;

            Pri_str = textp.text;
            City_str = textcity.text;
            Aera_str = textaexa.text;
           // obj.SetActive(false);
            GameManager.GetGameManager.CloseWindow(obj.transform);
        });
    }
    protected override void DoAction(ReturnHttpMessage msg)
    {
        if (msg.Code == HttpCode.SUCCESS)
        {
            //obj.SetActive(true);
            GameManager.GetGameManager.OpenWindow(obj.transform);
            JsonData jd = msg.Data["data"];
            CITYDATA = jd;
            Pri.Clear();
            for (int i = 0; i < jd.Count; i++)
            {
                JsonData data = jd[i];
                GetDataPri(data, textp, "province_name",Pri);              
            }
        }
    }

    private void GetDataPri(JsonData jd, Text text_level,string levelname,InsGroup Group)
    {
        Transform transformData = Group.AddItem().transform;
        Text text = transformData.GetComponentInChildren<Text>();
        text.text = jd[levelname].ToString();
        Button btn = transformData.GetComponent<Button>();
        btn.onClick.AddListener(delegate ()
        {
            text_level.text = text.text;
            if (jd.Keys.Contains("city"))
            {
                JsonData city = jd["city"];
                City.Clear();
                for (int j = 0; j < city.Count; j++)
                {
                    JsonData datacity = city[j];
                    GetDataCity(datacity, textcity, "city_name", City);                  
                }
            }
        });
    }

    private void GetDataCity(JsonData jd, Text text_level, string levelname, InsGroup Group)
    {
        Transform transformData = Group.AddItem().transform;
        Text text = transformData.GetComponentInChildren<Text>();
        text.text = jd[levelname].ToString();
        Button btn = transformData.GetComponent<Button>();
        btn.onClick.AddListener(delegate ()
        {
            text_level.text = text.text;
            Arex.Clear();
         
            if (jd.Keys.Contains("area"))
            {
                JsonData area = jd["area"];
                Debug.Log(JsonMapper.ToJson(area));
                for (int k = 0; k < area.Count; k++)
                {
                    JsonData dataarea = area[k];
                    GetDataAera(dataarea, textaexa,k, Arex);
                }
            }
        });
    }

    private void GetDataAera(JsonData jd, Text text_level, int levelname, InsGroup Group)
    {
        Transform transformData = Group.AddItem().transform;
        Text text = transformData.GetComponentInChildren<Text>();
        text.text = jd.ToString();
        Button btn = transformData.GetComponent<Button>();
        btn.onClick.AddListener(delegate ()
        {
            text_level.text = text.text;
        });
    }
}
