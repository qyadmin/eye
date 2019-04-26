using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataItem;
using UnityEngine.UI;
public class Address : MonoBehaviour
{

    [SerializeField]
    private GetCity CurrentCity;
    [SerializeField]
    private HttpModel Http_Add;
    [SerializeField]
    private HttpModel Http_Update;
    [SerializeField]
    private Transform AddWindow;
    [SerializeField]
    private Button btn_open_addwin;
    private DataDic<AddressData> _data;
    [SerializeField]
    private InsGroup Group;

    public AddressUpdate AddressTansform;

    [SerializeField]
    private InputField CurrentCityInput_Update;
    [SerializeField]
    private InputField CurrentCityInput_Add;

    public string[] GetCityData(InputField input)
    {
        string[] str_dara = new string[] { "", "", "" };
        string[] str = input.text.Split('/');
        for (int i = 0; i < str.Length; i++)
        {
            str_dara[i] = str[i];
        }
        return str_dara;
    }

    //// Use this for initialization
    void Start()
    {
        btn_open_addwin.onClick.AddListener(delegate ()
        {
            GameManager.GetGameManager.OpenWindow(AddWindow);
            TransformData ADDdata = AddWindow.GetComponent<TransformData>().GetBody();
            Button ok = ADDdata.GetObjectValue<Button>("OK");
            Button CHOSE = ADDdata.GetObjectValue<Button>("chose");
            CHOSE.onClick.AddListener(delegate ()
            {
                CHOSE.GetComponent<ButtonState>().ChangeState();
            });
            ok.onClick.AddListener(delegate ()
            {
                Add(CurrentCity,CHOSE.GetComponent<ButtonState>());
            });

        });
        // HttpModel login = GameManager.GetGameManager.http_body.GetTValue(HttpName);
        DataManager.GetDataManager.Address.EventObj.Addlistener(UpdateData);
    }

    public void Add(GetCity nowcity,ButtonState bst)
    {
        Http_Add.Data.AddData("province",nowcity.Pri_str);
        Http_Add.Data.AddData("city",nowcity.City_str);
        Http_Add.Data.AddData("area", nowcity.Aera_str);
       // Http_Add.Data.AddData("moren", "0");
        int num = System.Convert.ToInt32(bst.GetState);
        Http_Add.Data.AddData("moren", num.ToString());
        Http_Add.HttpSuccessCallBack.Addlistener(delegate (ReturnHttpMessage msg)
        {
            if (msg.Code == HttpCode.SUCCESS)
            {
                HttpModel httpAddress = GameManager.GetGameManager.http_body.GetTValue("address_get");
                httpAddress.Get();
                GameManager.GetGameManager.CloseWindow(AddWindow);
            }
        });
        Http_Add.Get();
    }

    private void UpdateData(object data)
    {
        if (data == null) return;
        _data = data as DataDic<AddressData>;
        Group.Clear();
        foreach (AddressData child in _data.Data)
        {
            TransformData transformData = Group.AddItem().GetComponent<TransformData>().GetBody();
            transformData.GetObjectValue<Text>("desc").text = child.province + child.city + child.area + child.address;
            transformData.GetObjectValue<Text>("name").text = child.name;
            transformData.GetObjectValue<Text>("phone").text = child.tel;
            Button btn_chose = transformData.GetObjectValue<Button>("chose");
            Button btn_Editor = transformData.GetObjectValue<Button>("Editor");

            if (child.moren != (System.Convert.ToInt32(btn_chose.GetComponent<ButtonState>().GetState)).ToString())
            btn_chose.GetComponent<ButtonState>().ChangeState();

            btn_chose.onClick.AddListener(delegate ()
            {
                UpdateAddress(child, btn_chose,true);
            });
            btn_Editor.onClick.AddListener(delegate ()
            {
                UpdateAddressTransform(child);
            });
        }
    }


    //修改地址
    private void UpdateAddress(AddressData child, Button btn_chose,bool IsMoren=false)
    {
        Debug.Log("更新A");
        AddressTansform.name.text = child.name;
        AddressTansform.tel.text = child.tel;
        AddressTansform.address.text = child.address;
        AddressTansform.zip_code.text = child.zip_code;

        string[] ciry_str = GetCityData(CurrentCityInput_Update);
        Http_Update.Data.AddData("id", child.id);
        if (!IsMoren)
        {
            if (ciry_str[0] == "" && ciry_str[1] == "" && ciry_str[2] == "")
            {
                Http_Update.Data.AddData("province", child.province);
                Http_Update.Data.AddData("city", child.city);
                Http_Update.Data.AddData("area", child.area);
            }
            else
            {
                Http_Update.Data.AddData("province", ciry_str[0]);
                Http_Update.Data.AddData("city", ciry_str[1]);
                Http_Update.Data.AddData("area", ciry_str[2]);
            }
        }
        else
        {
            Http_Update.Data.AddData("province", child.province);
            Http_Update.Data.AddData("city", child.city);
            Http_Update.Data.AddData("area", child.area);
        }
        Http_Update.Data.AddData("name", AddressTansform.name.text);
        Http_Update.Data.AddData("tel", AddressTansform.tel.text);
        Http_Update.Data.AddData("address", AddressTansform.address.text);
        Http_Update.Data.AddData("zip_code", AddressTansform.zip_code.text);
        //btn_chose.onClick.AddListener(delegate ()
        //{
        //    btn_chose.GetComponent<ButtonState>().SetState();
        //});
        int num = System.Convert.ToInt32(btn_chose.GetComponent<ButtonState>().ChangeState());
        Http_Update.Data.AddData("moren", num.ToString());
        Http_Update.HttpSuccessCallBack.Addlistener(delegate (ReturnHttpMessage msg)
        {
            if (msg.Code == HttpCode.SUCCESS)
            {
                HttpModel httpAddress = GameManager.GetGameManager.http_body.GetTValue("address_get");
                httpAddress.Get();
                GameManager.GetGameManager.CloseWindow(AddressTansform.Win);
            }
        });
        Http_Update.Get();
    }

    //修改地址
    private void UpdateAddressTransform(AddressData child)
    {
        Debug.Log("更新B");
        GameManager.GetGameManager.OpenWindow(AddressTansform.Win);
        AddressTansform.name.text = child.name;
        AddressTansform.tel.text = child.tel;
        AddressTansform.address.text = child.address;
        AddressTansform.zip_code.text = child.zip_code;
        AddressTansform.City.text = child.province + "/" + child.city + "/" + child.area;
        Button btn_update = AddressTansform.Btn_Save;

        if (System.Convert.ToInt32(AddressTansform.btn_moren.ChangeState()).ToString() != child.moren)
            AddressTansform.btn_moren.ChangeState();
        AddressTansform.btn_moren.GetComponent<Button>().onClick.RemoveAllListeners();
        AddressTansform.btn_moren.GetComponent<Button>().onClick.AddListener(delegate ()
        {
            AddressTansform.btn_moren.ChangeState();
        });
        btn_update.onClick.RemoveAllListeners();
        btn_update.onClick.AddListener(delegate() 
        {
            Http_Update.Data.AddData("id", child.id);

            string[] ciry_str = GetCityData(CurrentCityInput_Add);

            Http_Update.Data.AddData("province", ciry_str[0]);
            Http_Update.Data.AddData("city", ciry_str[1]);
            Http_Update.Data.AddData("area", ciry_str[2]);
            Http_Update.Data.AddData("name", AddressTansform.name.text);
            Http_Update.Data.AddData("tel", AddressTansform.tel.text);
            Http_Update.Data.AddData("address", AddressTansform.address.text);
            Http_Update.Data.AddData("zip_code", AddressTansform.zip_code.text);
            int num = System.Convert.ToInt32(AddressTansform.btn_moren.GetState);
            Http_Update.Data.AddData("moren", num.ToString());
            Http_Update.HttpSuccessCallBack.Addlistener(delegate(ReturnHttpMessage msg) 
            {
                if (msg.Code == HttpCode.SUCCESS)
                {
                    HttpModel httpAddress = GameManager.GetGameManager.http_body.GetTValue("address_get");
                    httpAddress.Get();
                    GameManager.GetGameManager.CloseWindow(AddressTansform.Win);
                }
            });
            Http_Update.Get();
        });

        Button btn_del = AddressTansform.Btn_Del;
        btn_del.onClick.RemoveAllListeners();
        btn_del.onClick.AddListener(delegate ()
        {
            AddressTansform.httpdel.Data.AddData("id",child.id);
            AddressTansform.httpdel.HttpSuccessCallBack.Addlistener(delegate (ReturnHttpMessage msg)
            {
                if (msg.Code == HttpCode.SUCCESS)
                {
                    HttpModel httpAddress = GameManager.GetGameManager.http_body.GetTValue("address_get");
                    httpAddress.Get();
                    GameManager.GetGameManager.CloseWindow(AddressTansform.Win);
                }
            });
            AddressTansform.httpdel.Get();
        });
    }
}
