using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using UnityEngine.UI;
public class Team : btn_Http_BaseEvent
{
    [SerializeField]
    private Text Title;
    [SerializeField]
    private Transform WIN;
    [SerializeField]
    InsGroup GROUP;

    [SerializeField]
    Button teamA, teamB, teamAll;

    [SerializeField]
    HttpModel http_Rep;
    protected override void Start()
    {
        http = GameManager.GetGameManager.http_body.GetTValue(HttpName);
        teamA.onClick.AddListener(delegate ()
        {
            GameManager.GetGameManager.OpenWindow(WIN);
            Title.text = "一级代理";
            http.Data.URL = "ajax_team_tj.php";
            http.HttpSuccessCallBack.Addlistener(delegate (ReturnHttpMessage msg)
            {
                Do(msg);
            });
            http.Get();
        });

        teamB.onClick.AddListener(delegate ()
        {
            GameManager.GetGameManager.OpenWindow(WIN);
            Title.text = "二级代理";
            http.Data.URL = "ajax_team_cj.php";
            http.HttpSuccessCallBack.Addlistener(delegate (ReturnHttpMessage msg)
            {
                Do(msg);
            });
            http.Get();
        });

        teamAll.onClick.AddListener(delegate ()
        {
            GameManager.GetGameManager.OpenWindow(WIN);
            Title.text = "总计";



            http.Data.URL = "ajax_team_cj.php";
            http.HttpSuccessCallBack.Addlistener(delegate (ReturnHttpMessage msg)
            {
                if (msg.Code == HttpCode.SUCCESS)
                {
                    levle01 = msg.Data["data"];

                    http_Rep.Data.URL = "ajax_team_tj.php";
                    http_Rep.HttpSuccessCallBack.Addlistener(delegate (ReturnHttpMessage msgrep)
                    {
                        if (msgrep.Code == HttpCode.SUCCESS)
                        {
                            level02 = msgrep.Data["data"];
                            DoAll();
                        }

                    });
                    http_Rep.Get();
                }

            });
            http.Get();
        });
    }

    JsonData levle01, level02;

    private void DoAll()
    {
        GROUP.Clear();
        if (levle01.Count > 0)
        {
            int i = 1;
            while (levle01.Keys.Contains(i.ToString()))
            {
                //  Debug.Log(level02.Count + "--------------1");
                TransformData data = GROUP.AddItem().GetComponent<TransformData>();
                data.GetObjectValue<Text>("tel").text = levle01[i.ToString()]["tel"].ToString();
                data.GetObjectValue<Text>("name").text = levle01[i.ToString()]["nikname"].ToString();
                data.GetObjectValue<Text>("time").text = levle01[i.ToString()]["sj"].ToString();
                i++;
            }
        }
        if (level02.Count > 0)
        {
            //Debug.Log(level02.Count+"--------------2");
            int j = 1;
            while (level02.Keys.Contains(j.ToString()))
            {
                TransformData data = GROUP.AddItem().GetComponent<TransformData>();
                data.GetObjectValue<Text>("tel").text = level02[j.ToString()]["tel"].ToString();
                data.GetObjectValue<Text>("name").text = level02[j.ToString()]["nikname"].ToString();
                data.GetObjectValue<Text>("time").text = level02[j.ToString()]["sj"].ToString();
                j++;
            }
        }
    }



    private void Do(ReturnHttpMessage msg)
    {
        if (msg.Code == HttpCode.SUCCESS)
        {
            GROUP.Clear();
            JsonData JDdata = msg.Data["data"];
            if (JDdata.ToString() == "" || JDdata.Count <= 0)
                return;
            int i = 1;
            while (JDdata.Keys.Contains(i.ToString()))
            {
                TransformData data = GROUP.AddItem().GetComponent<TransformData>();
                data.GetObjectValue<Text>("tel").text = JDdata[i.ToString()]["tel"].ToString();
                data.GetObjectValue<Text>("name").text = JDdata[i.ToString()]["nikname"].ToString();
                data.GetObjectValue<Text>("time").text = JDdata[i.ToString()]["sj"].ToString();
                i++;
            }
        }
    }
}
