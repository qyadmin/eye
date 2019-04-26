using System.Collections;
using System.Collections.Generic;
using LitJson;
using UnityEngine;

public class QuickWeoYang_cf : QuickWeiYang
{


    public override void Suc(JsonData data)
    {
        MessageManager._Instantiate.WindowShowMessage(string.Format("是否花费{0}个幼崽饲料进行一键喂养", data["data"]["counts"].ToString()), DoAction, "确定", true);
    }


    public override void DoAction()
    {
        HttpModel http = GameManager.GetGameManager.http_body.GetTValue("ajax_yjwy_cf_put.php");
        http.Get();
    }

}
