using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataItem;
using LitJson;
public class HeadPageImg : btn_Http_BaseEvent
{
 
    [SerializeField]
    Transform[] GroupImg;
    Vector3 pos01, pos02;
    [SerializeField]
    private float speed = 1;

    [SerializeField]
    private InsGroup imgGroup;

    protected override void Start()
    {
        pos01 = GroupImg[0].position;
        pos02 = GroupImg[1].position;
        http = GameManager.GetGameManager.http_body.GetTValue(HttpName);

        http.HttpSuccessCallBack.Addlistener(delegate (ReturnHttpMessage msg)
        {
            DoAction(msg);
        });
        http.Get();    
    }
    [SerializeField]
    Transform A, B;
    protected override void DoAction(ReturnHttpMessage msg)
    {
        if (msg.Code == HttpCode.SUCCESS)
        {
           
            JsonData data = msg.Data["tp_list"];
            int index = data.Count;
            A = GroupImg[0];
            B = GroupImg[1];
            int i = 0;
            foreach (JsonData child in data)
            {
                GameObject transformData = imgGroup.AddItem();
                LoadImage.GetLoadIamge.Load(Static.Instance.URL + child["img"].ToString(), new RawImage[] { transformData.GetComponent<RawImage>() });

                if(i==0)
                    LoadImage.GetLoadIamge.Load(Static.Instance.URL + child["img"].ToString(), new RawImage[] { A.GetComponent<RawImage>() });
                if (i == 1)
                    LoadImage.GetLoadIamge.Load(Static.Instance.URL + child["img"].ToString(), new RawImage[] { B.GetComponent<RawImage>() });

                if (imgGroup.Parent.childCount == index)
                    StartCoroutine("Move");
                i++;
            }
      
        }
    }

    int i = 0;
    IEnumerator Move()
    {
        A = GroupImg[0];
        B = GroupImg[1];
        A.SetParent(B);
        Transform Current = B;

        while (imgGroup.Parent.childCount <= 0)
        {
            yield return  0;
        }
        //Debug.Log("LaodDone..................................");
        //A.GetComponent<RawImage>().texture = GetUrl();
        //B.GetComponent<RawImage>().texture = GetUrl();
        //Debug.Log("hasveGet..................................");

        while (true)
        {
            Current.position = Vector3.MoveTowards(Current.position, pos01, Time.deltaTime * speed);
            if (Vector3.Distance(Current.position, pos01) < 0.01f)
            {
                if (Current == B)
                {
                    var Current_Parent = B.parent;
                    A.SetParent(Current_Parent);
                    A.position = pos02;
                    B.SetParent(A);
                    Current = A;
                    A.GetComponent<RawImage>().texture = GetUrl();
                }
                else
                {
                    var Current_Parent = A.parent;
                    B.SetParent(Current_Parent);
                    B.position = pos02;
                    A.SetParent(B);
                    Current = B;
                    B.GetComponent<RawImage>().texture = GetUrl();
                }

                yield return new WaitForSeconds(5);
            }
            yield return 0;
        }
    }


    private Texture GetUrl()
    {
        if (i == imgGroup.Parent.childCount)
            i = 0;
        int index = i;
        i++;
        Debug.Log(index+"首页轮播");
        return imgGroup.Parent.GetChild(index).GetComponent<RawImage>().texture;
    }
}
