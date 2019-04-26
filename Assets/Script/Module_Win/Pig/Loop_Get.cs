using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loop_Get : MonoBehaviour {

    [SerializeField]
    HttpModel http_Loop;
	// Use this for initialization
	void Start ()
    {    
        InvokeRepeating("Get",2,30);
	}

    void Get()
    {
        http_Loop.Get();
    }
}
