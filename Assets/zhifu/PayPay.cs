// ==================================================================
// 作    者：MJ.风暴洋
// 説明する：安卓支付宝支付demo
// 作成時間：2018-12-27
// 類を作る：PayPay.cs
// 版    本：v 1.0
// 会    社：大连仟源科技
// QQと微信：731483140
// ==================================================================

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

[System.Serializable]
public class PayInfo
{
    public string subject;  // 显示在按钮上的内容,跟支付无关系  
    public float money;     // 商品价钱  
    public string title;    // 商品描述  
    public string productid;    // 商品id
}

public class PayPay : MonoBehaviour
{
    private AndroidJavaObject currentActivity;
    private string ordersss;
    public int Type = 0;

    void Start()
    {
    }

    //[DllImport("__Internal")]
    //private static extern void iospay(string body, string amount, string trade_no);

    public void Pay(string order_id, string price, string title)
    {
        //if (Type == 0)
        //{
        //    Pay_zfb(order_id, price, title);
        //}
        //if (Type == 1)
        //{
        //    Pay_wx(order_id, price, title);
        //}
        Pay_zfb(order_id, price, title);
    }


    private void Pay_zfb(string order_id, string price, string title)
    {
#if UNITY_ANDROID
        PayInfo info = new PayInfo();
        info.subject = title;
        info.money = float.Parse("0.01");
        info.title = title;
        info.productid = order_id;
        Alipaypay(info);
#elif UNITY_IPHONE
		 //iospay(title,"0.01",order_id);
#endif
    }

    private void Pay_wx(string order_id, string price, string title)
    {
#if UNITY_ANDROID
        PayInfo info = new PayInfo();
        info.subject = title;
        info.money = float.Parse("0.01");
        info.title = title;
        info.productid = order_id;
        Alipaypay(info);
#elif UNITY_IPHONE
		 //iospay(title,"0.01",order_id);
#endif
    }
    public void Alipaypay(PayInfo payInfo)
    {
        // AlipayClient是Android里的方法名字，写死.  
        // payInfo.money是要付的钱，只能精确分.  
        // payInfo.title是商品描述信息，注意不能有空格.  
        // 固定写法  
        AndroidJavaClass jc = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
        currentActivity = jc.GetStatic<AndroidJavaObject>("currentActivity");

        ordersss = currentActivity.Call<string>("Alipay", payInfo.title, payInfo.money.ToString(), payInfo.productid);
        // Formatsss(ordersss);
    }
    //private void Formatsss(string ss)
    //{
    //    orderinfo.text = "";
    //    string[] temp = ss.Split('&');
    //    for (int i = 0; i < temp.Length; i++)
    //    {
    //        orderinfo.text = orderinfo.text + temp[i] + "\n";
    //    }
    //}

    public void PayResult(string productid)
    {
        //  buttons[int.Parse(productid)].gameObject.GetComponent<Image>().color = Color.red;
        MessageManager._Instantiate.WindowShowMessage("支付成功");
    }
}