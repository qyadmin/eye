using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DataItem;
using LitJson;
public class Goods : MonoBehaviour
{
    private DataDic<GoodsData> _data;
    private DataDic<ShopCar> Shop_data;
    [SerializeField]
    private InsGroup Group, GroupT;
    [SerializeField]
    private InsGroup GroupShop;
    [SerializeField]
    private InsGroup GroupBuy;

    [SerializeField]
    private Button btn;
    [SerializeField]
    private Button btn_delete_shopcar;
    [SerializeField]
    private Transform WinShopCar;
    HttpModel http;

    [SerializeField]
    private Transform ShowWin, BuyWin;
    [SerializeField]
    private Text CallMoney;
    [SerializeField]
    private Text CallMoneyBuy;
    public float baseMoney = 0;


    [SerializeField]
    private PayPay paydata;
    [SerializeField]
    private Button Show_Car, Add_Car, Shop_Buy, btn_Buy, btn_buy_single;
    public class ShopCarData
    {
        public string id;
        public ShopCar data;
        public AddNub num;
    }

    private GoodsData CurrentData;
    private List<ShopCar> CurrentCar;
    private List<ButtonState> CurrentButtonState = new List<ButtonState>();
    Dic<string, ShopCarData> Goods_ID = new Dic<string, ShopCarData>();
    Dic<string, ShopCar> WaitToAction = new Dic<string, ShopCar>();

    ButtonState MyStateShopCall;

    private int appId = 0;

    [SerializeField]
    private GameObject Ret_Item_List;
    [SerializeField]
    private ScrollRect A, B;
    // Use this for initialization
    void Start()
    {
        appId = Static.Instance.AppID;
        Ret_Item_List.SetActive(appId==0);
        if (appId == 0)
        {
            A.vertical=false;
            B.vertical = false;
        }
        Static.Instance.AddValue("type_app",appId.ToString());
        GetAddress();
        http = GameManager.GetGameManager.http_body.GetTValue("goods_Shop_Car");
        DataManager.GetDataManager.Goods.EventObj.Addlistener(UpdateData);
        DataManager.GetDataManager.ShopCar.EventObj.Addlistener(UpdateDataShopCar);

        MyStateShopCall = btn_ShopCar.GetComponent<ButtonState>();


        //打开购物车
        btn.onClick.AddListener(delegate ()
        {
            OpenShopCar();
        });

        //打开购物车
        Show_Car.onClick.AddListener(delegate ()
        {
            OpenShopCar();
        });

        //添加购物车
        Add_Car.onClick.AddListener(delegate ()
        {
            AddToShopCar(CurrentData);
        });

        //显示购买列表
        Shop_Buy.onClick.AddListener(delegate ()
        {
            ShowBuyList();
        });

        //显示单个购买列表
        btn_buy_single.onClick.AddListener(delegate ()
        {
            ShowBuySingle();
        });

        //提交订单
        btn_Buy.onClick.AddListener(delegate ()
        {
            SubmitOrder();
        });

        //删除购物车
        btn_delete_shopcar.onClick.AddListener(delegate ()
        {
            ClearShopCar();
        });

        //购物车全选
        btn_ShopCar.onClick.AddListener(delegate ()
        {
            ButtonState MyState = btn_ShopCar.GetComponent<ButtonState>();
            bool IsChose = MyState.ChangeState();
            if (IsChose)
            {
                WaitToAction.Clear();
                foreach (ButtonState child in CurrentButtonState)
                {
                    child.SetState(true);
                }
                foreach (ShopCar child in CurrentCar)
                {
                    WaitToAction.Add(child.id, child);
                }
            }
            else
            {
                WaitToAction.Clear();
                foreach (ButtonState child in CurrentButtonState)
                {
                    child.SetState(false);
                }
            }
        });
    }


    //购物车提交
    private void ShowBuyList()
    {
        GameManager.GetGameManager.OpenWindow(BuyWin);
        List<ShopCar> BuyLsit = new List<ShopCar>(WaitToAction.Body.Values);
        BuyList(BuyLsit, GroupBuy);
        TransformData td = BuyWin.GetComponent<TransformData>().GetBody();
        JsonData add_data = GetAddress();
        if (add_data != null)
        {
            td.GetObjectValue<Text>("title").text = add_data["name"].ToString() + "," + add_data["tel"].ToString();
            td.GetObjectValue<Text>("desc").text = add_data["province"] + " " + add_data["city"] + " " + add_data["area"] + " " + add_data["address"];
        }
        AddPay();
    }


    //显示购买组件
    private void AddPay()
    {
        TransformData td = BuyWin.GetComponent<TransformData>().GetBody();
        Transform paytype = td.GetObjectValue<Image>("ChoseType").transform;
        int count = paytype.parent.childCount;
        paytype.SetSiblingIndex(count - 1);
        paytype.gameObject.SetActive(true);
    }


    //单个物品提交
    private void ShowBuySingle()
    {
        GameManager.GetGameManager.OpenWindow(BuyWin);
        List<ShopCar> BuyLsit = new List<ShopCar>();
        ShopCar item = new ShopCar();
        item.id = "-1";
        item.goods_id = CurrentData.id;
        item.goods_num = "1";
        item.sj = "currenttime";
        BuyLsit.Add(item);
        BuyList(BuyLsit, GroupBuy);
        GetBuyAddress();
        AddPay();
    }

    public void GetBuyAddress()
    {
        TransformData td = BuyWin.GetComponent<TransformData>().GetBody();
        JsonData add_data = GetAddress();
       // Debug.Log(JsonMapper.ToJson( add_data));
        if (add_data != null)
        {
            td.GetObjectValue<Text>("title").text = add_data["name"].ToString() + "," + add_data["tel"].ToString();
            td.GetObjectValue<Text>("desc").text = add_data["province"] + " " + add_data["city"] + " " + add_data["area"] + " " + add_data["address"];
        }
        else
        {          
            td.GetObjectValue<Text>("title").text = "";
            td.GetObjectValue<Text>("desc").text = "请添加收货地址";
        }
    }


    //提交订单
    private void SubmitOrder()
    {
        HttpModel httpbuy = GameManager.GetGameManager.http_body.GetTValue("Shop_buy");
        string strID = string.Empty;
        string strNum = string.Empty;
        foreach (ShopCar child in CurrentbUYData)
        {
            if (strID != string.Empty)
                strID += ",";
            if (strNum != string.Empty)
                strNum += ",";

            strID += child.goods_id;
            strNum += child.goods_num;
        }
        httpbuy.Data.AddData("address_id", "1");
        httpbuy.Data.AddData("goods_id", strID);
        httpbuy.Data.AddData("goods_num", strNum);
        httpbuy.Data.AddData("total_price", CurrentMoney.ToString());
        if (CurrentAddressData == null)
        {
            MessageManager._Instantiate.WindowShowMessage("请天添加收货地址");
        }
        else
        {
            httpbuy.Data.AddData("address_id", CurrentAddressData["id"].ToString());
            httpbuy.HttpSuccessCallBack.Addlistener(delegate (ReturnHttpMessage msg)
            {
                if (msg.Code == HttpCode.SUCCESS)
                {
                    JsonData data = msg.Data["data"];
                    paydata.Pay(data["order_id"].ToString(), data["need_pay"].ToString(), CurrentbUYData[0].sj);
                }
            });
            httpbuy.Get();
        }
    }


    //清除购物车
    private void ClearShopCar()
    {
        List<string> allkey = new List<string>(WaitToAction.Body.Keys);
        if (allkey.Count > 0)
        {
            http.Data.AddData("type", "2");
            string str = string.Empty;

            for (int i = 0; i < allkey.Count; i++)
            {
                if (i == 0)
                    str += allkey[i];
                else
                {
                    str += "," + allkey[i];
                }
            }

            http.Data.AddData("id", str);
            http.Get();
        }
    }


    //打开购物车
    private void OpenShopCar()
    {
        GameManager.GetGameManager.OpenWindow(WinShopCar);
        http.Data.AddData("type", "3");
        http.Get();
    }


    //刷新商品列表
    private void UpdateData(object data)
    {
        if (data == null) return;
        _data = data as DataDic<GoodsData>;
        Group.Clear();

        foreach (GoodsData child in _data.Data)
        {
            if (appId == 0)
            {
                if (child.type_id == "2")
                {
                    continue;
                }
            }
            else
            {
                if (child.type_id == "1")
                {
                    continue;
                }
            }

            TransformData transformData = null;

            if (appId == 0)
            {
                if (child.type_s == "1")
                {
                    transformData = Group.AddItem().GetComponent<TransformData>();
                    transformData.GetObjectValue<Text>("money").text = "¥" + child.price;
                    transformData.GetObjectValue<Text>("moneyT").text = string.Format("二次购买价格为{0}元", child.price_second);
                }
                else
                {
                    transformData = GroupT.AddItem().GetComponent<TransformData>();
                    transformData.GetObjectValue<Text>("money").text = "¥" + child.price_second;
                }
            }
            else
            {
                transformData = Group.AddItem().GetComponent<TransformData>();
                transformData.GetObjectValue<Text>("money").text = "¥" + child.price;
            }


            LoadImage.GetLoadIamge.Load(child.url, new RawImage[] { transformData.GetObjectValue<RawImage>("icon") });
            transformData.GetObjectValue<Text>("desc").text = child.title;

            Button btnGoods = transformData.GetObjectValue<Button>("icon");
            btnGoods.onClick.AddListener(delegate ()
            {
                if (appId == 0)
                {
                    if (Static.Instance.GetValue("huiyuan_type") == "298")
                        ShowGoods(child);
                    else
                    {
                        if (child.type_s == "2")
                            MessageManager._Instantiate.WindowShowMessage("请先购买298商品");
                        else
                            ShowGoods(child);
                    }
                }
                else
                {
                    ShowGoods(child);
                }
            });

            Button btn = transformData.GetObjectValue<Button>("Add");
            btn.onClick.RemoveAllListeners();
            btn.onClick.AddListener(delegate ()
            {
                if (appId == 0)
                {
                    if (Static.Instance.GetValue("huiyuan_type") == "298")
                        AddToShopCar(child);
                    else
                    {
                        if (child.type_s == "2")
                            MessageManager._Instantiate.WindowShowMessage("请先购买298商品");
                        else
                            AddToShopCar(child);
                    }
                }
                else
                {
                    AddToShopCar(child);
                }
            });
        }
    }


    //添加到购物车
    private void AddToShopCar(GoodsData child)
    {

        if (!Goods_ID.Body.ContainsKey(child.id))
        {
            http.Data.AddData("type", "1");
            http.Data.AddData("goods_id", child.id);
            http.Data.AddData("goods_num", "1");
            http.HttpSuccessCallBack.Addlistener(delegate (ReturnHttpMessage msg)
            {
                if (msg.Code == HttpCode.SUCCESS)
                {
                    if (Goods_ID.Body.ContainsKey(child.id))
                        OpenShopCar();
                }
            });
            http.Get();
        }
        else
        {
            OpenShopCar();
        }
    }


    public JsonData CurrentAddressData;
    //获取默认地址
    private JsonData GetAddress(Text text = null)
    {
        HttpModel httpAddress = GameManager.GetGameManager.http_body.GetTValue("address_get");
        httpAddress.HttpSuccessCallBack.Addlistener(delegate (ReturnHttpMessage msg)
        {
            if (msg.Code == HttpCode.SUCCESS)
            {
                JsonData data = msg.Data["data"];
                if (data.Count <= 0)
                {
                    TransformData td = BuyWin.GetComponent<TransformData>().GetBody();
                    td.GetObjectValue<Text>("title").text = "";
                    td.GetObjectValue<Text>("desc").text = "请添加收货地址";
                }
                JsonData addredata = null;
                foreach (JsonData item in data)
                {
                    if (item["moren"].ToString() == "1")
                    {
                        addredata = item;
                        break;
                    }
                }
                if (addredata == null)
                {
                    if(data.Count>0)
                    addredata = data[0];
                }

                if (text != null)
                {
                    if (data.Count > 0)
                        text.text = addredata["province"] + " " + addredata["city"] + " " + addredata["area"] + " " + addredata["address"];
                    else
                        text.text = "请添加收货地址"; ;
                }

                CurrentAddressData = addredata;
                if (CurrentAddressData != null)
                {
                    TransformData td = BuyWin.GetComponent<TransformData>().GetBody();
                    td.GetObjectValue<Text>("title").text = CurrentAddressData["name"].ToString() + "," + CurrentAddressData["tel"].ToString();
                    td.GetObjectValue<Text>("desc").text = CurrentAddressData["province"] + " " + CurrentAddressData["city"] + " " + CurrentAddressData["area"] + " " + CurrentAddressData["address"];
                }
            }
        });
        httpAddress.Get();
        return CurrentAddressData;
    }


    //刷新回调接口
    [SerializeField]
    private Text text_buy_address;
    public void UpdateGoodsSatte()
    {
        if (CurrentData == null)
            return;
        ShowGoods(CurrentData);
        //GetAddress(text_buy_address);
    }

    //显示商品页面
    private void ShowGoods(GoodsData child)
    {
        CurrentData = child;
        GameManager.GetGameManager.OpenWindow(ShowWin);
        TransformData td = ShowWin.GetComponent<TransformData>();
        LoadImage.GetLoadIamge.Load(child.url, new RawImage[] { td.GetObjectValue<RawImage>("icon") });
        td.GetObjectValue<Text>("desc").text = child.content;

        if (appId == 0)
        {
            if (child.type_s == "1")
            {
                td.GetObjectValue<Text>("moneyT").text = string.Format("二次购买价格为{0}元", child.price_second);
                td.GetObjectValue<Text>("money").text = "¥" + child.price;
            }
            else
            {
                td.GetObjectValue<Text>("money").text = "¥" + child.price_second;
            }
        }
        else
        {
            td.GetObjectValue<Text>("money").text = "¥" + child.price;
        }


       td.GetObjectValue<Text>("Connect").text=child.content;

        GetAddress(td.GetObjectValue<Text>("address"));

    }


    //获取选择按钮状态
    private bool GetChoseState()
    {
        bool isALL = true;
        foreach (ButtonState child in CurrentButtonState)
        {
            if (!child.GetState)
            {
                isALL = false;
                break;
            }
        }
        return isALL;
    }

    //刷新购物车列表
    private void UpdateDataShopCar(object data)
    {
        if (data == null) return;
        Shop_data = data as DataDic<ShopCar>;
        CurrentCar = Shop_data.Data;
        GroupShop.Clear();
        Goods_ID.Clear();
        baseMoney = 0;
        CurrentButtonState.Clear();
        WaitToAction.Clear();
        ButtonState btn_shopcarstate = btn_ShopCar.GetComponent<ButtonState>();
        foreach (ShopCar child in CurrentCar)
        {
            if (!Goods_ID.Body.ContainsKey(child.id))
            {
                GoodsData goods = _data.GetItem(child.goods_id);
                if (appId == 0)
                {
                    if (goods.type_id == "2")
                    {
                        continue;
                    }
                }
                else
                {
                    if (goods.type_id == "1")
                    {
                        continue;
                    }
                }
                TransformData transformData = GroupShop.AddItem().GetComponent<TransformData>().GetBody();
                LoadImage.GetLoadIamge.Load(goods.url, new RawImage[] { transformData.GetObjectValue<RawImage>("icon") });
                transformData.GetObjectValue<Text>("desc").text = goods.content;

                if (appId == 0)
                {
                    if (GameManager.GetGameManager.IsHot(goods.type_s))
                    {
                        transformData.GetObjectValue<Text>("money").text = "¥" + goods.price;
                        transformData.GetObjectValue<Text>("moneyT").text = string.Format("二次购买价格为{0}元", goods.price_second);
                        baseMoney += float.Parse(goods.price) * int.Parse(child.goods_num);
                    }
                    else
                    {
                        baseMoney += float.Parse(goods.price_second) * int.Parse(child.goods_num);
                        transformData.GetObjectValue<Text>("money").text = "¥" + goods.price_second;
                    }
                }
                else
                {
                    baseMoney += float.Parse(goods.price) * int.Parse(child.goods_num);
                    transformData.GetObjectValue<Text>("money").text = "¥" + goods.price;
                }

                Button btn_delete = transformData.GetObjectValue<Button>("Chose");
                ButtonState state = btn_delete.GetComponent<ButtonState>();
                CurrentButtonState.Add(state);

                btn_delete.onClick.AddListener(delegate ()
                {
                    bool isdo = state.ChangeState();
                    if (isdo)
                    {
                        if (!WaitToAction.Body.ContainsKey(child.id))
                            WaitToAction.Add(child.id, child);
                        MyStateShopCall.SetState(GetChoseState());
                    }
                    else
                    {
                        if (WaitToAction.Body.ContainsKey(child.id))
                            WaitToAction.Remove(child.id);
                        MyStateShopCall.SetState(false);
                    }
                });

                if (btn_shopcarstate.GetState)
                {
                    state.SetState(true);
                    WaitToAction.Add(child.id, child);
                }
                else
                {
                    state.SetState(false);
                    WaitToAction.Remove(child.id);
                }


                AddNub num = transformData.GetObjectValue<AddNub>("AddNum");

                if (!GameManager.GetGameManager.IsLargeMoney(goods.type_id))
                {
                    GameObject numobj = num.gameObject;
                    numobj.SetActive(false);
                }
                else
                {
                    GameObject numobj = num.gameObject;
                    numobj.SetActive(true);
                }

                num.Body.text = child.goods_num;
                ShopCarData item = new ShopCarData();
                item.id = child.id;
                item.data = child;
                item.num = num;
                Goods_ID.Add(child.goods_id, item);

                num.Add_Button.onClick.AddListener(delegate ()
                {
                    http.Data.AddData("type", "4");
                    http.Data.AddData("id", child.id);
                    http.Data.AddData("goods_num", num.Add(1).ToString());
                    http.Get();
                });

                num.Dis_Button.onClick.AddListener(delegate ()
                {
                    http.Data.AddData("type", "4");
                    http.Data.AddData("id", child.id);
                    http.Data.AddData("goods_num", num.Add(-1).ToString());
                    http.Get();
                });
            }
            else
            {
                AddNub num = Goods_ID.GetTValue(child.goods_id).num;
                num.Add(1);
            }
        }
        CallMoney.text = "¥" + baseMoney.ToString();
    }


    //购买列表
    List<ShopCar> CurrentbUYData;
    public float CurrentMoney;
    public List<ShopCar> CurrentBuyData { get { return CurrentbUYData; } }
    private void BuyList(List<ShopCar> data, InsGroup Group)
    {
        Group.Clear();
        baseMoney = 0;
        foreach (ShopCar child in data)
        {
            TransformData transformData = Group.AddItem().GetComponent<TransformData>().GetBody();
            GoodsData goods = _data.GetItem(child.goods_id);
            LoadImage.GetLoadIamge.Load(goods.url, new RawImage[] { transformData.GetObjectValue<RawImage>("icon") });
            transformData.GetObjectValue<Text>("desc").text = goods.content;

            if (appId == 0)
            {

                if (GameManager.GetGameManager.IsHot(goods.type_s))
                {
                    baseMoney += float.Parse(goods.price) * int.Parse(child.goods_num);
                    transformData.GetObjectValue<Text>("moneyT").text = string.Format("二次购买价格为{0}元", goods.price_second);
                    transformData.GetObjectValue<Text>("money").text = "¥" + goods.price;
                }
                else
                {
                    baseMoney += float.Parse(goods.price_second) * int.Parse(child.goods_num);
                    transformData.GetObjectValue<Text>("money").text = "¥" + goods.price_second;
                }
            }
            else
            {
                baseMoney += float.Parse(goods.price) * int.Parse(child.goods_num);
                transformData.GetObjectValue<Text>("money").text = "¥" + goods.price;
            }

            AddNub num = transformData.GetObjectValue<AddNub>("AddNum");

            if (!GameManager.GetGameManager.IsLargeMoney(goods.type_id))
            {
                GameObject numobj = num.gameObject;
                numobj.SetActive(false);
            }
            else
            {
                GameObject numobj = num.gameObject;
                numobj.SetActive(true);
            }

            num.Add(int.Parse(child.goods_num) - 1);
            num.Add_Button.onClick.AddListener(delegate ()
            {
                child.goods_num = num.Add(1).ToString();

                if (appId == 0)
                {
                    if (goods.type_s == "1")
                        baseMoney += float.Parse(goods.price);
                    else
                        baseMoney += float.Parse(goods.price_second);
                }
                else
                {
                    baseMoney += float.Parse(goods.price);
                }

                CallMoneyBuy.text = "¥" + baseMoney.ToString();
            });
            num.Dis_Button.onClick.AddListener(delegate ()
            {
                bool IsGone = false;
                child.goods_num = num.Add(-1, out IsGone).ToString();
                if (IsGone)
                {
                    if (appId == 0)
                    {
                        if (goods.type_s == "1")
                            baseMoney -= float.Parse(goods.price);
                        else
                            baseMoney -= float.Parse(goods.price_second);
                    }
                    else
                    {
                        baseMoney -= float.Parse(goods.price);
                    }
                }
                CallMoneyBuy.text = "¥" + baseMoney.ToString();
            });

        }
        CallMoneyBuy.text = "¥" + baseMoney.ToString();
        CurrentMoney = baseMoney;
        CurrentbUYData = data;

    }

    [Header("全选按钮")]
    [SerializeField]
    private Button btn_ShopCar;

}
