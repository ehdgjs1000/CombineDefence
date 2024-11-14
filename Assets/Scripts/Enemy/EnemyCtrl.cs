using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCtrl : MonoBehaviour
{
    //Stats
    public float hp;
    public float moveSpeed;
    public float gold;
    public float Type; // 0=Fire, 1=Water, 2=Earth, 3=Dark, 4=Special
    private bool isDie;

    private int moveCheckPoint = 0;
    [SerializeField] private Transform[] movePos;

    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (!isDie)
        {
            Move(moveCheckPoint);
        }
    }
    private void Move(int point)
    {
        if (point == 4) point = 0;
        transform.position = Vector2.MoveTowards(this.transform.position, movePos[point+1].position, moveSpeed);
        CheckArrive();

    }
    private void CheckArrive()
    {
        if (this.transform.position == movePos[moveCheckPoint + 1].position) //Arrive
        {
            moveCheckPoint++;
            if(moveCheckPoint ==4)moveCheckPoint = 0;
        }
    }


}
