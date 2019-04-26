using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Pig_Buy : MonoBehaviour
{

    [System.Serializable]
    public class BuyData
    {
        public TransformData databuy;
        public string priceNum;
        public bool IsNum=true;
    }

    [SerializeField]
    BuyData[] data_zj,data_sb,data_sl;

    
    [SerializeField]
    HttpModel buy;
    [SerializeField]
    Transform buy_win;
    [SerializeField]
    Pig_Price pricebody;
    // Use this for initialization
     public void StartSet( )
    {
        //猪
        foreach (BuyData child in data_zj)
        {
            SetData(child);
        }


        //设备
        foreach (BuyData child in data_sb)
        {
            SetData(child);
        }


        //饲料
        foreach (BuyData child in data_sl)
        {
            SetData(child);
        }
    }


    private void SetData(BuyData child)
    {
        TransformData ts = child.databuy.GetComponent<TransformData>().GetBody();
        string price = pricebody.PriceData.GetTValue(child.priceNum);
        ts.GetObjectValue<Text>("price").text = price;
        ts.GetObjectValue<Button>("btn").onClick.AddListener(delegate ()
        {
            GameManager.GetGameManager.OpenWindow(buy_win);

            TransformData buydata = buy_win.GetComponent<TransformData>();
            AddNub num = buydata.GetObjectValue<AddNub>("AddNum");
            //if (!child.IsNum)
            //{
            //    num.gameObject.SetActive(false);
            //}
            buydata.GetObjectValue<Image>("icon").sprite = ts.GetObjectValue<Image>("icon").sprite;

            Button btn_b = buydata.GetObjectValue<Button>("btn");
            btn_b.onClick.RemoveAllListeners();
            btn_b.onClick.AddListener(delegate ()
            {
                buy.Data.AddData("sl", num.Body.text);
                buy.Data.AddData("flag", child.priceNum);
                buy.HttpSuccessCallBack.Addlistener(delegate (ReturnHttpMessage msg)
                {
                    GameManager.GetGameManager.CloseWindow(buy_win);
                });
                buy.Get();
            });
        });
    }

}
