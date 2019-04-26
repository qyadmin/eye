using UnityEngine;
using UnityEngine.Events;
public class ButtonNormalEvent : ButtonEventBase {

    [SerializeField]
    private GameObject ShowObj, HideObj, hideSecondaryPanel, showFirstPanel;
    [TextArea(2,5)]
    public string Message;
    [SerializeField]
    private HttpModel Model;


    [SerializeField]
    UnityEvent clickevent; 
    public override void Start()
    {
        base.Start();

        ActionEvent += OnClick;

        if (Model != null)
            ActionEvent += SendModel;
       
    }

    void SendModel()
    {
        Model.Get();
    }

    void OnClick()
    {
        clickevent.Invoke();
        if (ShowObj != null)
            GameManager.GetGameManager.OpenWindow(ShowObj.transform);
        if (HideObj != null)
            GameManager.GetGameManager.CloseWindow(HideObj.transform);
        if (Message != string.Empty)
            MessageManager._Instantiate.WindowShowMessage(Message);
        if (hideSecondaryPanel != null && showFirstPanel != null)
        {
            hideSecondaryPanel.SetActive(false);
            showFirstPanel.SetActive(true);
        }
    }

    public void SetObjActive(GameObject Obj,bool State)
    {
        if(Obj!=null)
        Obj.SetActive(State);
    }

    public void PlayAnimationGo(Animator GetAnimtor)
    {
        GetAnimtor.SetBool("Go",true);
    }

    public void PlayAnimationBack(Animator GetAnimtor)
    {
        GetAnimtor.SetBool("Back", true);
    }
}
