using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInfo : btn_Http_BaseEvent {


   

    
    protected override void Start()
    {
        base.Start();
        HttpModel login = GameManager.GetGameManager.http_body.GetTValue("GetUseInfo");
        btn.onClick.AddListener(delegate ()
        {
            login.Get();
        });
    }
    protected override void DoAction(ReturnHttpMessage msg)
    {
       
    }
}
