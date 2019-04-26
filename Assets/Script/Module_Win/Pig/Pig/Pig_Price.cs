using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
using Dh_json;

public class Pig_Price : btn_Http_BaseEvent
{

    public class price
    {
        public string js_zsz_price;
        public string js_jz_price;
        public string js_xds_price;
        public string js_gjys_price;
        public string js_gjyfz_price;
        public string js_djys_price;
        public string js_djyfz_price;
        public string js_yzsl_price;
        public string js_cnsl_price;
        public string js_czsl_price;
        public string ptnc_sx;
        public string zjnc_sx;
        public string gjnc_sx;
        public string ptnc_up;
        public string zjnc_up;
    }

    public Dic<string, string> PriceData = new Dic<string, string>();
    public JsonData pricedata;
    [SerializeField]
    Pig_Buy buy;
    protected override void Start()
    {
        http = GameManager.GetGameManager.http_body.GetTValue(HttpName);
        http.HttpSuccessCallBack.Addlistener(delegate (ReturnHttpMessage msg)
        {
            DoAction(msg);
        });
        http.Get();
    }

    protected override void DoAction(ReturnHttpMessage msg)
    {
        if (msg.Code == HttpCode.SUCCESS)
        {
            pricedata = msg.Data["data"];
            price price_data = DataJson.ToObject<price>(JsonMapper.ToJson(pricedata));
            PriceData.Add("0", price_data.js_zsz_price);
            PriceData.Add("1", price_data.js_jz_price);
            PriceData.Add("2", price_data.js_xds_price);
            PriceData.Add("3", price_data.js_gjys_price);
            PriceData.Add("4", price_data.js_gjyfz_price);
            PriceData.Add("5", price_data.js_djys_price);
            PriceData.Add("6", price_data.js_djyfz_price);
            PriceData.Add("7", price_data.js_yzsl_price);
            PriceData.Add("8", price_data.js_cnsl_price);
            PriceData.Add("9", price_data.js_czsl_price);
        }
       // Debug.Log(PriceData.Body.Count+"**************************************");
        buy.StartSet();
    }

}
