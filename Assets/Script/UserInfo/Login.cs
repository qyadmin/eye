using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using LitJson;
public class Login : btn_Http_BaseEvent
{
    protected override void DoAction(ReturnHttpMessage msg)
    {
        Debug.Log("LOGINS");
        if (msg.Code == HttpCode.SUCCESS)
        {
            JsonData data = msg.Data["data"];

            if (data.Keys.Contains("huiyuan_id"))
            {
                Static.Instance.AddValue("huiyuan_id", data["huiyuan_id"].ToString());
                SceneManager.LoadScene("LabbyScene");
            }
            else
            {
                MessageManager._Instantiate.WindowShowMessage("登陆失败，没有获得到会员权限");
            }
        }
      
    }

}
