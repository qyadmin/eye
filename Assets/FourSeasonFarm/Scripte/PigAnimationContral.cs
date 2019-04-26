using LitJson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigAnimationContral : MonoBehaviour {
    [SerializeField]
    Animator pig_animtor;
    [SerializeField]
    Animator camera_animtor;

    Timer _timer = new Timer(3);

    Vector3 pig_StartLocalPos, camera_StartLocalPos;

    bool canwalk = false;

    [SerializeField]
    float walkspeed = 1;
    // Use this for initialization


    [SerializeField]
    Camera  cam_aniamtor;
    [SerializeField]
    GameObject UI;
	void Start () {
        pig_StartLocalPos = pig_animtor.transform.localPosition;
        // animationReset();
        GameManager.GetGameManager.indexData.Addlistener(PlayerYin);
    }

    private void PlayerYin(JsonData data)
    {
        if(data["bxflag"].ToString()=="0")
        animationReset();
    }
	
    public void animationReset()
    {
        GetComponent<yinxiao>().PALYER();
        cam_aniamtor.gameObject.SetActive(true);
        UI.SetActive(false);
        camera_reset();
        pig_reset();
        canwalk = false;
        _timer.EndTimer();
        
        pig_animtor.transform.localPosition = pig_StartLocalPos;
        WalkAnimation();
        MovingAnimation();
        _timer.tickEvnet += StandAnimation;
        _timer.StartTimer();
    }

    void MovingAnimation()
    {
        camera_animtor.SetBool("move",true);
    }
    void camera_reset()
    {
        camera_animtor.SetBool("move", false);
    }

    void pig_reset()
    {
        pig_animtor.SetBool("walk", false);
    }

    void WalkAnimation()
    {
        canwalk = true;
        pig_animtor.SetBool("walk", true);
    }
    void StandAnimation()
    {
        canwalk = false;
        pig_animtor.SetBool("walk", false);
    }

    void forwardMove()
    {
        if(canwalk)
        pig_animtor.transform.Translate(Vector3.forward * Time.deltaTime * walkspeed);
    }

	// Update is called once per frame
	void Update () {
        _timer.UpdateTimer(Time.deltaTime);
        forwardMove();

        //if (Input.GetKeyDown(KeyCode.F))
        //{
        //    animationReset();
        //}
    }

    public void BackCamera()
    {
        Invoke("back",1);
    }

    void back()
    {
        cam_aniamtor.gameObject.SetActive(false);
        UI.SetActive(true);
    }
}
