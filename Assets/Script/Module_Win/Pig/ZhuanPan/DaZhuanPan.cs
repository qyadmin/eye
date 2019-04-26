using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class lucky
{
    public string name;
    public string desc;
    public float bili;
    public int num;
    public float Aengle = 0;
    public List<int> NumGroup = new List<int>();

    public bool IsMine(int num)
    {
        return NumGroup.Contains(num);
    }
}
public class DaZhuanPan : MonoBehaviour
{
    public List<lucky> Lucky = new List<lucky>();
    [SerializeField]
    Transform PAN;
    [SerializeField]
    float speed=10;
    [SerializeField]
    int MAXAroundNum = 10;
    [SerializeField]
    int MINAroundNum = 3;
    [SerializeField]
    bool IsDoing = false;

    [SerializeField]
    HttpModel http_jg;

    private float zerospeed;


    [SerializeField]
    Transform win;
    public bool CanGo = false;
    // Use this for initialization
    void Start()
    {
        zerospeed = speed;
    }

    public void Res()
    {
        PAN.eulerAngles = Vector3.zero;
        IsDoing = false;
        http_jg.Data.AddData("jg_id", CurrentLuck.name);
        http_jg.Get();
    }

    Coroutine c;
    public void DoAciton()
    {
        if (!CanGo)
        {
            MessageManager._Instantiate.WindowShowMessage("今日已抽奖！");
            return;
        }
        if (IsDoing|| Lucky.Count<=0)
            return;
        if(c!=null)
        StopCoroutine(c);
        c= StartCoroutine("LuckyGo");
    }

    lucky CurrentLuck = null;

    IEnumerator LuckyGo()
    {
        IsDoing = true;
        int currentNUm = Random.Range(0, 101);
        PAN.eulerAngles = Vector3.zero;
        float currenAengler = 0;
        foreach(lucky child in Lucky)
        {
            if (child.IsMine(currentNUm))
            {
                currenAengler = child.Aengle;
                CurrentLuck = child;
                Debug.Log(child.name);
                break;
            }
        }

        float CUE = Random.Range(currenAengler - 55, currenAengler - 5);
        float targrvalue = CUE+Random.Range(MINAroundNum, MAXAroundNum)*360;
        float AddValue = 0;
        while (AddValue <= targrvalue)
        {
            Debug.Log(AddValue);
            PAN.Rotate(0, 0, Time.deltaTime * speed);
            AddValue += Time.deltaTime * speed;
            if (targrvalue - AddValue < 100&&targrvalue-AddValue>30)
            {
                speed -= 10;
            }
            yield return 0;
        }
        PAN.eulerAngles = new Vector3(0,0, CUE);    
        speed =zerospeed;
        yield return new WaitForSeconds(1);

      //  MessageManager._Instantiate.WindowShowMessage(CurrentLuck.desc,Res,"确定",true);
        GameManager.GetGameManager.OpenWindow(win);
        TransformData transformData = win.GetComponent<TransformData>().GetBody();
        Button btn_ok= transformData.GetObjectValue<Button>("ok");
        btn_ok.onClick.RemoveAllListeners();
        btn_ok.onClick.AddListener(Res);
        transformData.GetObjectValue<Text>("contentTxt").text= CurrentLuck.desc;

    }
}
