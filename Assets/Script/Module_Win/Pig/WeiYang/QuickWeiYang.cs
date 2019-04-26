using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson;
public class QuickWeiYang : btn_Http_BaseEvent {


    protected override void DoAction(ReturnHttpMessage msg)
    {

        if (msg.Code == HttpCode.SUCCESS)
        {
            Suc(msg.Data);
        }

        if (msg.Code == HttpCode.FAILED)
        {
            Fal(msg.Data);
        }
    }



    public virtual void Suc(JsonData data)
    {
        MessageManager._Instantiate.WindowShowMessage(string.Format("是否花费{0}个成年饲料进行一键喂养",data["data"]["counts"].ToString()),DoAction,"确定",true);
    }


    public virtual void Fal(JsonData data)
    {
        MessageManager._Instantiate.WindowShowMessage(data["msg"].ToString());
    }


    public virtual void DoAction()
    {
        HttpModel http= GameManager.GetGameManager.http_body.GetTValue("ajax_yjwy_nc_put.php");
        http.Get();
    }

}
