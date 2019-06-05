using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class WeChatLoadingDemo : MonoBehaviour {

    [SerializeField]
    Button click;
    [SerializeField]
    Text id;
    [SerializeField]
    HttpModel login;

#if UNITY_IPHONE
//    [DllImport("__Internal")]
//    private static extern void _WeichatLogin();
#endif

    private void Start()
    {
        click.onClick.AddListener(delegate {
            if (agree.isOn)
                LoadingWeiXin();
            else
            {
                MessageManager._Instantiate.Show("你需要同意用户协议，才能登陆");
            }
        });
    }

    public void LoadingWeiXin()
    {
#if UNITY_ANDROID
        WeChat_Android_Loading();
#elif UNITY_IPHONE
        Wechat_IOS_Loading();
#endif
    }

    //微信登录-安卓
    void WeChat_Android_Loading()
    {
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        AndroidJavaObject jo = jc.GetStatic<AndroidJavaObject>("currentActivity");
        jo.Call("WeChatLoginStart");
    }

    void Wechat_IOS_Loading()
    {
        //_WeichatLogin();
    }

    //微信回调.返回Code数据 安卓苹果定义同方法
    public Toggle agree;
    public void LoginCallBack(string obj)
    {
        //id.text = obj;
        login.Data.AddData("code", obj);
        login.Get();
    }
    
}
