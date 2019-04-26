using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ControlOPC : MonoBehaviour {


    public void Open(Transform obj)
    {
        GameManager.GetGameManager.OpenWindow(obj);
    }

    public void Close(Transform obj)
    {
        GameManager.GetGameManager.CloseWindow(obj);

    }

	public void Wait(Button time)
    {
        StopAllCoroutines();
        StartCoroutine(Loop(time));
    }
    int nub = 0;

	IEnumerator Loop(Button obj)
    {
        obj.GetComponent<Image>().raycastTarget = false;
        obj.interactable = false;
        nub = 60;
		Text a = obj.GetComponentInChildren<Text> (); 
        while (nub > 0)
        {
			a.text=nub.ToString()+"秒";
            yield return new WaitForSeconds(1);
            nub--;
        }

		a.text = "获取验证码";
		obj.interactable = true;
        obj.GetComponent<Image>().raycastTarget = true;
    }
}
