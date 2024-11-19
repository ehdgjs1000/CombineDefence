using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyCtrl : MonoBehaviour
{
    //Stats
    public float hp;
    private float maxHp;
    public float moveSpeed;
    public int gold;
    public float Type; // 0=Fire, 1=Water, 2=Earth, 3=Dark, 4=Special
    private bool isDie = false;
    [SerializeField] private Image hpImage;

    private int moveCheckPoint = 0;
    [SerializeField] private Transform[] movePos;

    Rigidbody rb;

    private void Awake()
    {
        maxHp = hp;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (hp<= 0.0f && !isDie)
        {
            StartCoroutine(Die());
        }
        hpImage.fillAmount = (hp / maxHp);

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
    public void GetDamage(float damage)
    {
        hp -= damage;
    }
    private IEnumerator Die()
    {
        GameManager.instance.Gold += gold;
        isDie = true;
        SpawnManager.instance.remainEnemy--;
        yield return new WaitForSeconds(0.1f);
        Destroy(this.gameObject);
    }

}
