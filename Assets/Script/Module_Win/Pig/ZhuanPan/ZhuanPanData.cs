using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using LitJson;
public class ZhuanPanData : btn_Http_BaseEvent
{


    [SerializeField]
    DaZhuanPan zp;

    List<int> allnum = new List<int>();

    [SerializeField]
    private Transform GroupDESC;
    [SerializeField]
    Transform dzp;
    protected override void Start()
    {
        base.Start();     
    }

    protected override void DoAction(ReturnHttpMessage msg)
    {

        if (msg.Code == HttpCode.SUCCESS)
        {
            JsonData data = msg.Data["data"];
            if (data.Count <= 0)
                return;

            allnum = new List<int>();
            for (int i = 1; i < 101; i++)
            {
                allnum.Add(i);
            }

            int j = 1;
            foreach (JsonData child in data)
            {
                lucky item = new lucky();
                item.bili = float.Parse(child["bl"].ToString());

                item.Aengle = 60 * j;

                int bilinum = (int)(item.bili * 100);
                bilinum = bilinum > 0 ? bilinum : 1;

                for (int i = 0; i < bilinum; i++)
                {
                    int currentNum = allnum[Random.Range(0, allnum.Count)];
                    item.NumGroup.Add(currentNum);
                    allnum.Remove(currentNum);
                }
                item.name = j.ToString();
                zp.Lucky.Add(item);
                GroupDESC.GetChild(j - 1).GetComponent<Text>().text = GetDesc(j).Replace("s", child["sl"].ToString());
                item.desc = GetDesc(j).Replace("s", child["sl"].ToString());
                j++;
            }
            zp.CanGo = true;

        }
        else
        {
            zp.CanGo = false;
        }
    }



    public string GetDesc(int num)
    {
        string des = "";
        switch (num)
        {
            case 1:
                des = "s个积分";
                break;
            case 2:
                des = "s个积分";
                break;
            case 3:
                des = "s袋普通饲料";
                break;
            case 4:
                des = "s个钻石";
                break;
            case 5:
                des = "s只金猪幼崽";
                break;
            case 6:
                des = "谢谢惠顾";
                break;
        }
        return des;
    }
}
