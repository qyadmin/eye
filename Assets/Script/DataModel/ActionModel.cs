using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using LitJson;

//泛型分发机制
public class EventPatcher<T>
{
    public Action<T> listener;

    public void Addlistener(Action<T> GetObj)
    {
        listener += GetObj;
    }

    public void Removelistener(Action<T> GetObj)
    {
        listener -= GetObj;
    }

    public void Send(T GetObj)
    {
        if(listener!=null)
        listener.Invoke(GetObj);
    }
    public void ClearAllEevnt()
    {
        while (this.listener != null)
        {
            this.listener -= this.listener;
        }
    }
}


//无参数分发机制
public class EventNoneParamPatcher
{
    public Action listener;

    public void Addlistener(Action GetObj)
    {
        listener += GetObj;
    }

    public void Removelistener(Action GetObj)
    {
        listener -= GetObj;
    }

    public void Send()
    {
        if (listener != null)
            listener.Invoke();
    }

    public void ClearAllEevnt()
    {
        while (this.listener != null)
        {
            this.listener -= this.listener;
        }
    }
}






//lvl与color颜色换算
public class GetCL
{
    //获取颜色序号
    public static int GetColorNub(string lvl)
    {
        int nub = System.Convert.ToInt32(Mathf.Floor(int.Parse(lvl)/5));
        nub = nub > 3 ? 3 : nub;
        return nub;
    }

    //获取颜色等级.暂时没用
    public static string Getlvlfloor(string lvl)
    {
      
        int nub = System.Convert.ToInt32(Mathf.Floor(int.Parse(lvl) / 5));
        nub = nub > 3 ? 3 : nub;
        return ((nub+1)*5).ToString();
    }

    //获取颜色等级.暂时没用
    public static int Getlvlint(string lvl)
    {
        int trynub = 100;
        bool get= int.TryParse(lvl, out trynub);
        if (get)
        {
            lvl = lvl == "0" ? "1" : lvl;
            int nub = System.Convert.ToInt32(Mathf.Floor(int.Parse(lvl) / 5));
            nub = nub > 3 ? 3 : nub;
            return ((nub + 1) * 5);
        }
        return trynub;
    }

    public static bool Compare(string V1, string V2)
    {
        return Getlvlint(V1) > Getlvlint(V2) ? true : false;
    }
}