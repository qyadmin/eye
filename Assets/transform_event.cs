using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class transform_event : MonoBehaviour
{

    public Image TargetImage;

    [SerializeField]
    Sprite finish_sprit;
    // Use this for initialization



    private void Update()
    {
        _timer.UpdateRepeatTimer(Time.deltaTime);
    }

    public void bt_type(string obj)
    {
        if (obj == "打款" || obj == "收款")
            this.transform.parent.GetComponent<Button>().interactable = true;
        else
            this.transform.parent.GetComponent<Button>().interactable = false;
    }
    public void pp_type(string obj)
    {
        if (obj == "已完成")
            this.transform.parent.parent.GetComponent<Image>().sprite = finish_sprit;

        switch (obj)
        {
            case "0":
                this.GetComponent<Text>().text = "等待排单";
                break;
            case "1":
                this.GetComponent<Text>().text = "等待匹配";
                break;
            case "2":
                this.GetComponent<Text>().text = "部分匹配";
                break;
            case "3":
                this.GetComponent<Text>().text = "完全匹配";
                break;
            case "4":
                this.GetComponent<Text>().text = "已完成";
                break;
        }
    }
    Timer _timer = new Timer(1);
    int time;

    public void Getjson(string value)
    {
        this.GetComponent<Text>().text = string.Empty;
        _timer.EndTimer();
        _timer.tickEvnet -= time_event2;
        time = int.Parse(value);
        _timer.tickEvnet += time_event2;
        _timer.StartTimer();
    }

    void time_event2()
    {
        if (time <= 0)
        {
            this.GetComponent<Text>().text = "已到期";
            _timer.EndTimer();
        }
        else
        {
            this.GetComponent<Text>().text = Mathf.Floor((time % 86400) / 3600) + "时" + Mathf.Floor((time % 86400 % 3600) / 60) + "分" + Mathf.Floor(time % 86400 % 3600 % 60) + "秒";
            time--;
        }
    }


    public void LoadImage(string obj)
    {
        if (obj != null)
        {
            StartCoroutine(loadtexture(obj));
        }

    }

    IEnumerator loadtexture(string obj)
    {
        Debug.Log(obj);
        WWW www = new WWW(obj);
        yield return www;
        if (www.error != null)
        {
            Debug.Log(www.error);

        }
        if (www != null && string.IsNullOrEmpty(www.error))
        {
            Sprite sprite = Sprite.Create(www.texture, new Rect(0, 0, www.texture.width, www.texture.height), Vector2.zero);
            if (this.GetComponent<Image>() != null)
                this.GetComponent<Image>().sprite = sprite;
            else
                TargetImage.sprite = sprite;
            Button thisbutton = this.gameObject.AddComponent<Button>();
            thisbutton.onClick.AddListener(delegate () { });
        }
    }


}
