// ==================================================================
// 作    者：A.R.I.P.风暴洋-宋杨
// 説明する：个人信息窗口组件
// 作成時間：2018-07-26
// 類を作る：Transform_User.cs
// 版    本：v 1.0
// 会    社：大连仟源科技
// QQと微信：731483140
// ==================================================================

using UnityEngine;
using UnityEngine.UI;
using DataItem;
using LitJson;
public class Transform_User : MonoBehaviour
{
    //个人信息界面信息数据
    public Text _userNameText, out_userNameText;
    public Text _userIDText, out_userIDText, _yuguangid;
    public Text _userMoneyTxt, _MainMoney, _car;
    public Text _userLvlTxt, out_userLvlTxt;
    public RawImage Head, headout, erweima;
    public Transform shangjiwd;
    public HttpModel sj, http_loadimg01, http_loadimg02, http_loadimg;
    private DataInfo<UserData> _data;
    public Text allnum;
    public InputField zhifubao, backcard, bank;
    public Text TEST;

    void Gedata(object data)
    {

    }

    void Start()
    {
        DataManager.GetDataManager.user.EventObj.Addlistener(UpdateData);
        sj.HttpSuccessCallBack.Addlistener(delegate (ReturnHttpMessage obj)
       {
           if (obj.Code == HttpCode.SUCCESS)
               GameManager.GetGameManager.CloseWindow(shangjiwd);
       });

        http_loadimg01.HttpSuccessCallBack.Addlistener(delegate (ReturnHttpMessage obj01)
       {
           if (obj01.Code == HttpCode.SUCCESS)
           {
               http_loadimg.HttpSuccessCallBack.Addlistener(delegate (ReturnHttpMessage obj)
               {
                   LoadImage.GetLoadIamge.Load(obj.Data["img"].ToString(), new RawImage[] { erweima });
               });
               http_loadimg.Data.AddData("urllink", Static.Instance.URL + "/app?params=" + obj01.Data["tuiguangcode"].ToString());
               http_loadimg.Get();
           }
       });
    }

    private void UpdateData(object data)
    {
        if (data == null) return;
        _data = data as DataInfo<UserData>;
        out_userNameText.text = _userNameText.text = _data.Data.nickname;
        http_loadimg.Data.AddData("phone", _data.Data.phone.ToString());
        Static.Instance.AddData("phone", _data.Data.phone.ToString());
        _yuguangid.text = out_userIDText.text = _userIDText.text = "ID: " + _data.Data.id.ToString();
        _userMoneyTxt.text = _data.Data.sb.ToString();
        _MainMoney.text = _data.Data.sb.ToString();
        _car.text = _data.Data.totalpower.ToString();
        out_userLvlTxt.text = _userLvlTxt.text = _data.Data.strlevel;
        zhifubao.text = _data.Data.zhifubao;
        backcard.text = _data.Data.bankcard;
        bank.text = _data.Data.bank;
        LoadImage.GetLoadIamge.Load(_data.Data.avatar, new RawImage[] { Head, headout });
        allnum.text = "团队总人数：" + _data.Data.cumulative.ToString();
        CheckBD();
    }

    //检查是否有上级玩家
    public void CheckBD()
    {
        if (_data.Data.superior == 0)
        {
            try
            {
                string aa = GetComponent<test>().BoardToOut();
                string bb = string.Empty;
                if (aa.Contains("%"))
                {
                    int num = aa.IndexOf("%");
                    bb = aa.Substring(0, num);
                    Debug.Log(aa + "---SUB---" + bb);
                }
                else
                    bb = aa;
                if (aa == string.Empty)
                {
                    GameManager.GetGameManager.OpenWindow(shangjiwd);
                }
                else
                {
                    HttpModel bind = GameManager.GetGameManager.http_body.GetTValue("http_bind");
                    bind.Data.URL = "bind/bind";
                    bind.Data.AddData("jwt", Static.Instance.GetValue("jwt"));
                    bind.Data.AddData("tuiguangcode", bb);
                    bind.HttpSuccessCallBack.Addlistener(delegate (ReturnHttpMessage msg)
                    {
                        if (msg.Code != HttpCode.SUCCESS)
                        {
                            GameManager.GetGameManager.OpenWindow(shangjiwd);
                        }
                    });
                    bind.Get();
                }
            }
            catch
            {
                GameManager.GetGameManager.OpenWindow(shangjiwd);
            }
        }
    }
}
