using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
public class Friend : MonoBehaviour
{


    [SerializeField]
    InsGroup Firned, sqlist;
    [SerializeField]
    private Button Search_btn, Sqlist_btn, hy_list_btn;
    HttpModel search_http,
        add_http,
        dear_http,
        sq_list_http,
        hy_list,
        liutan_http
        ;

    [SerializeField]
    Transform win_liyan;
    // Use this for initialization
    void Start()
    {
        search_http = GameManager.GetGameManager.http_body.GetTValue("ajax_hy_search.php");
        add_http = GameManager.GetGameManager.http_body.GetTValue("ajax_hy_sq.php");
        dear_http = GameManager.GetGameManager.http_body.GetTValue("ajax_hy_dear.php");
        sq_list_http = GameManager.GetGameManager.http_body.GetTValue("ajax_hy_sqlist.php");
        hy_list = GameManager.GetGameManager.http_body.GetTValue("ajax_hy_list.php");
        liutan_http = GameManager.GetGameManager.http_body.GetTValue("ajax_ly_put.php");
        Search();
        sq_list();
        showFriendList();
    }


    //显示好友
    private void showFriendList()
    {
        hy_list_btn.onClick.AddListener(delegate ()
        {
            UpdateFiendList();
        });
    }

    private void UpdateFiendList()
    {
        hy_list.HttpSuccessCallBack.Addlistener(delegate (ReturnHttpMessage msg)
        {
            if (msg.Code == HttpCode.SUCCESS)
            {
                Firned.Clear();
                JsonData data = msg.Data["data"];
                if (data.Count <= 0)
                    return;

                foreach (JsonData child in data)
                {
                    TransformData transformData = Firned.AddItem().GetComponent<TransformData>().GetBody();
                    //  ConfigManager.GetConfigManager.SetIamge(transformData.GetObjectValue<Image>("icon"),int.Parse(child["img"].ToString()));
                    LoadImage.GetLoadIamge.Load(child["img"].ToString(), new RawImage[] { transformData.GetObjectValue<RawImage>("icon") });
                    transformData.GetObjectValue<Text>("ID").text = child["bianhao"].ToString();
                    transformData.GetObjectValue<Text>("name").text = child["name"].ToString();
                    Button btn_talk = transformData.GetObjectValue<Button>("talk");
                    btn_talk.gameObject.SetActive(true);
                    btn_talk.onClick.AddListener(delegate ()
                    {
                        GameManager.GetGameManager.OpenWindow(win_liyan);
                        TransformData transformDataly = win_liyan.GetComponent<TransformData>().GetBody();
                        transformDataly.GetObjectValue<Button>("send").onClick.AddListener(delegate ()
                        {
                            liutan_http.Data.AddData("hy_id", child["hy_id"].ToString());
                            liutan_http.Get();
                        });

                    });
                }
            }
        });

        hy_list.Get();
    }


    //搜索好友
    private void Search()
    {
        Search_btn.onClick.AddListener(delegate ()
        {
            search_http.HttpSuccessCallBack.Addlistener(delegate (ReturnHttpMessage msg)
            {
                if (msg.Code == HttpCode.SUCCESS)
                {
                    Firned.Clear();
                    JsonData data = msg.Data["data"];
                    if (data.Count <= 0)
                        return;
                    TransformData transformData = Firned.AddItem().GetComponent<TransformData>().GetBody();
                    foreach (JsonData child in data)
                    {
                        //ConfigManager.GetConfigManager.SetIamge(transformData.GetObjectValue<Image>("icon"), int.Parse(child["img"].ToString()));
                        LoadImage.GetLoadIamge.Load(child["img"].ToString(), new RawImage[] { transformData.GetObjectValue<RawImage>("icon") });
                        transformData.GetObjectValue<Text>("ID").text = child["bianhao"].ToString();
                        transformData.GetObjectValue<Text>("name").text = child["name"].ToString();
                        transformData.GetObjectValue<Button>("ok").gameObject.SetActive(true);
                        transformData.GetObjectValue<Button>("ok").onClick.AddListener(delegate ()
                        {
                            add_http.Data.AddData("hy_id", child["hy_id"].ToString());
                            add_http.Get();
                        });
                    }
                }
            });

            search_http.Get();
        });
    }


    //申请列表
    private void sq_list()
    {
        Sqlist_btn.onClick.AddListener(delegate ()
        {
            UpdateLsit();         
        });
    }

    public void UpdateLsit()
    {
        sq_list_http.HttpSuccessCallBack.Addlistener(delegate (ReturnHttpMessage msg)
        {
            if (msg.Code == HttpCode.SUCCESS)
            {

                sqlist.Clear();
                JsonData data = msg.Data["data"];
                if (data.Count <= 0)
                    return;
                TransformData transformData = sqlist.AddItem().GetComponent<TransformData>().GetBody();
                foreach (JsonData child in data)
                {
                    // ConfigManager.GetConfigManager.SetIamge(transformData.GetObjectValue<Image>("icon"), int.Parse(child["img"].ToString()));
                    LoadImage.GetLoadIamge.Load(child["img"].ToString(), new RawImage[] { transformData.GetObjectValue<RawImage>("icon") });
                    transformData.GetObjectValue<Text>("ID").text = child["bianhao"].ToString();
                    transformData.GetObjectValue<Text>("name").text = child["name"].ToString();
                    transformData.GetObjectValue<Button>("ok").onClick.AddListener(delegate ()
                    {
                        dear_http.Data.URL = "ajax_hy_ty.php";
                        dear_http.Data.AddData("hy_id", child["hy_id"].ToString());
                        dear_http.HttpSuccessCallBack.Addlistener(delegate(ReturnHttpMessage sg) 
                        {
                            if (sg.Code == HttpCode.SUCCESS)
                            {
                                UpdateLsit();
                                UpdateFiendList();
                            }
                        });
                        dear_http.Get();
                    });
                    transformData.GetObjectValue<Button>("lost").onClick.AddListener(delegate ()
                    {
                        dear_http.Data.URL = "ajax_hy_jj.php";
                        dear_http.HttpSuccessCallBack.Addlistener(delegate (ReturnHttpMessage sg)
                        {
                            if (sg.Code == HttpCode.SUCCESS)
                            {
                                UpdateLsit();
                                UpdateFiendList(); 
                            }
                        });
                        dear_http.Data.AddData("hy_id", child["hy_id"].ToString());
                        dear_http.Get();
                    });
                }
            }
        });

        sq_list_http.Get();
    }
}
