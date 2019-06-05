using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class test : MonoBehaviour
{

    [SerializeField]
    Text abc;
    //[SerializeField]
    //Button Copy, Paste;
    //[SerializeField]
    //InputField ert;

    // Use this for initialization
    void Start()
    {
        this.transform.GetComponent<Button>().onClick.AddListener(delegate
        {
            CopyToBoard();
        });
        //Paste.onClick.AddListener(delegate {

        //    BoardToOut();
        //});
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void CopyToBoard()
    {
        UniClipboard.SetText(abc.text);
    }

    public string BoardToOut()
    {
        return UniClipboard.GetText();
    }
}
