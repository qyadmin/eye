using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aniamtor_UI : MonoBehaviour
{
    [SerializeField]
    UnityEngine.Animator ani, ani1;
    bool iSCLOSE = false;
    public void CHANGE()
    {
        iSCLOSE = !iSCLOSE;
        if (iSCLOSE)
        {
            ani.SetBool("a", true);
            ani1.SetBool("a", true);
        }
        else
        {
            ani.SetBool("b", true);
            ani1.SetBool("b", true);
        }
    }
}
