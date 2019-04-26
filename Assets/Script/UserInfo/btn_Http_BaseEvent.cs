using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using LitJson;
public class btn_Http_BaseEvent : MonoBehaviour {

    public Button btn;
    public string HttpName;
    protected HttpModel http;
    protected virtual void Start()
    {
        http = GameManager.GetGameManager.http_body.GetTValue(HttpName);    
        btn.onClick.AddListener(delegate ()
        {
            http.HttpSuccessCallBack.Addlistener(delegate (ReturnHttpMessage msg)
            {
                DoAction(msg);
            });
            http.Get();
        });
    }

    protected virtual void DoAction(ReturnHttpMessage msg)
    {

    }


    protected virtual void DoListData<T>(object data,InsGroup Group) where T : class
    {
        if (data == null) return;
        T _data = data as T;
        Group.Clear();
       TransformData  transformData = Group.AddItem().GetComponent<TransformData>();
    }

    //protected virtual TransformData DoList(InsGroup Group) 
    //{
    //    Group.Clear();
    //    TransformData transformData = Group.AddItem().GetComponent<TransformData>();
    //    return transformData;
    //}

}
