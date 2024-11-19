using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementsCtrl : MonoBehaviour
{
    [SerializeField] private bool isDragging = false;
    private bool canAttack = true;

    //Stats
    public float damage;
    //public float moveSpeed;
    public float attackSpeed;
    public float attackTerm;
    public float criticalPercent;
    public float criticalDamage;
    public float attackRange;

    //Attack
    public RaycastHit2D[] enemies;
    public Transform neariestEnemy;
    [SerializeField] LayerMask enemyLayer;

    //Combine
    [SerializeField] GameObject[] nextLevelGO;

    private void FixedUpdate()
    {
        attackTerm -= Time.deltaTime;

        canAttack = !isDragging;
        
        if (isDragging)
        {
            Move();
        }
        if (attackTerm <= 0.0f && canAttack)
        {
            CheckEnemy();
        }


    }
    private void OnMouseDown()
    {
        isDragging = !isDragging;
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

    private void CheckEnemy()
    {
        enemies = Physics2D.CircleCastAll(transform.position, attackRange, Vector2.zero, 0, enemyLayer);
        neariestEnemy = GetNearest();
        if(neariestEnemy != null) Attack();
    }
    Transform GetNearest()
    {
        Transform result = null;
        float diff = 100;
        foreach (RaycastHit2D target in enemies)
        {
            Vector3 myPos = transform.position;
            Vector3 enemyPos = target.transform.position;
            float curDiff = Vector3.Distance(myPos,enemyPos);

            if (curDiff < diff)
            {
                diff = curDiff;
                result = target.transform;
            }
        }
        return result;
    }
    private void Attack()
    {
        attackTerm = attackSpeed;
        neariestEnemy.GetComponent<EnemyCtrl>().GetDamage(this.damage);
        StartCoroutine(ScaleChange());
    }
    //Check Attack
    private IEnumerator ScaleChange()
    {
        this.transform.localScale = new Vector3(1.0f,1.0f,1);
        yield return new WaitForSeconds(0.1f);
        this.transform.localScale = new Vector3(1.2f, 1.2f, 1);
    }
}
