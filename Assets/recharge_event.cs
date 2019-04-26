using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class recharge_event : MonoBehaviour {

    ToggleGroup group;

    private void Start()
    {
        group = this.GetComponent<ToggleGroup>();
        Static.Instance.AddValue("txflag", "0");
    }

    public void Onclick()
    {
        IEnumerable<Toggle> toggleGroup = group.ActiveToggles();
        foreach (Toggle t in toggleGroup)
        {//遍历这个存放Toggle的按钮组IEnumerable，此乃C#的一个枚举集合，一般直接用foreach遍历
            if (t.isOn)//遍历到一个被选择的Toggle
            {
                Static.Instance.AddValue("txflag", t.name);
            }
        }
    }
}
