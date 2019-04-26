using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yinxiao : MonoBehaviour {


    [SerializeField]
    private AudioSource ADUO,audo_y;

    public void PALYER()
    {
        ADUO.Play();
    }


    public void PALYER_y()
    {
        audo_y.Play();
    }
}
