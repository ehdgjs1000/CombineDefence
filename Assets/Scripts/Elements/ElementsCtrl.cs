using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementsCtrl : MonoBehaviour
{
    [SerializeField] private bool isDragging = false;
    private bool canAttack = true;

    //Draw Circle
    public LineRenderer circleRenderer;
    public int subdivision = 10;

    //Stats
    public float damge;
    //public float moveSpeed;
    public float attackSpeed;
    private float attackTerm;
    public float criticalPercent;
    public float criticalDamage;
    public float attackRange;


    private void Update()
    {
        attackTerm -= Time.deltaTime;

        canAttack = !isDragging;
        
        if (isDragging)
        {
            Move();
            //ShowAttackRange();
        }
        if (attackTerm <= attackSpeed)
        {
            Attack();
        }


    }
    private void OnMouseDown()
    {
        isDragging = !isDragging;
    }
    private void ShowAttackRange()
    {
        
        circleRenderer.positionCount = subdivision;

        for(int i = 0; i<subdivision; i++)
        {
            float angleStep = 2f * Mathf.PI / subdivision;
            float posX = attackRange*Mathf.Cos(angleStep * i);
            float posY = attackRange*Mathf.Sin(angleStep * i);

            Vector3 pointInCircle = new Vector3(posX,posY,0);

            circleRenderer.SetPosition(i, pointInCircle);
        }
    }
    private void Move()
    {
        Vector2 movePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (movePos.x >= GameManager.instance.maxX) movePos.x = GameManager.instance.maxX;
        if (movePos.x <= GameManager.instance.minX) movePos.x = GameManager.instance.minX;
        if (movePos.y >= GameManager.instance.maxY) movePos.y = GameManager.instance.maxY;
        if (movePos.y <= GameManager.instance.minY) movePos.y = GameManager.instance.minY;
        transform.position = movePos;
    }
    private void OnDrawGizmos()
    {

    }
    public void Attack()
    {
        attackTerm = attackSpeed;
        //Physics2D

    }
}
