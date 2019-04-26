using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHu : MonoBehaviour {

    [SerializeField]
    private List<Transform> point=new List<Transform>();

    [SerializeField]
    private Transform pointBody;

    [SerializeField]
    private int speed;
    private Vector3 CenterPos;
    bool IsMove;
    [SerializeField]
    private int a, b;

    private void Start()
    {
        foreach (Transform child in pointBody)
        {
            point.Add(child);
        }
    }

    IEnumerator StartMove(int s_num,int e_num)
    {
        while (s_num <= e_num)
        {
            transform.position = Vector3.MoveTowards(transform.position,point[s_num].position,Time.deltaTime* speed);
            if (Vector3.Distance(point[s_num].position, transform.position) < 0.1f)
                s_num++;
            yield return 0;
        }
        transform.position = point[e_num].position;
        IsMove = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            Move(a,b);
    }

    private void Move(int a,int b)
    {
        if (IsMove)
            return;
        IsMove = true;
        if (a<b)
        StartCoroutine(StartMove(a,b));
        else
        StartCoroutine(StartMoveBack(a, b));
    }


    IEnumerator StartMoveBack(int s_num, int e_num)
    {
        while (s_num >= e_num)
        {
            transform.position = Vector3.MoveTowards(transform.position, point[s_num].position, Time.deltaTime * speed);
            if (Vector3.Distance(point[s_num].position, transform.position) < 0.1f)
                s_num--;
            yield return 0;
        }
        transform.position = point[e_num].position;
        IsMove = false;
    }

}
