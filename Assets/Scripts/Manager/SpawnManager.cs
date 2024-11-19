using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public static SpawnManager instance;

    private bool isFinished = false;
    [SerializeField] private Transform spawnPos;
    [SerializeField] private GameObject[] enemies;

    //Spawn
    public float spawnTerm;
    private float initSpawnTerm;
    public int spawnCount = 15;
    public int remainEnemy = 0;

    //
    private float nextRoundTime = 30.0f;

    private void Awake()
    {
        instance = this;
        initSpawnTerm = spawnTerm;
    }
    private void Update()
    {
        spawnTerm -= Time.deltaTime;
    }
    private void FixedUpdate()
    {

        //Spawn Enemy
        if(spawnTerm <= 0.0f && spawnCount>0)
        {
            SpawnEnemy();
        }
        if (spawnCount == 0 && !isFinished)
        {
            StartCoroutine(FinishRound());
        }
    }
    private void SpawnEnemy()
    {
        spawnTerm = initSpawnTerm;
        remainEnemy++;
        spawnCount--;
        //Spawn Enemy
        int ranVal = Random.Range(0, enemies.Length);
        Instantiate(enemies[ranVal], spawnPos.position, Quaternion.identity);
    }
    private IEnumerator FinishRound()
    {
        GameManager.instance.Round++;
        isFinished = true;
        
        yield return new WaitForSeconds(nextRoundTime);
        spawnCount = 15;
        isFinished = false;

    }
}
