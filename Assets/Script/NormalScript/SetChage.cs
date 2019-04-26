using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetChage : MonoBehaviour {


    [SerializeField]
    GameObject OK,LOST;
    public void CAHNGE(Toggle TG)
    {
        OK.SetActive(TG.isOn);
        LOST.SetActive(!TG.isOn);
    }

}
