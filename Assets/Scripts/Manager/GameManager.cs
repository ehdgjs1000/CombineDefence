using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //UI
    [SerializeField] private Text goldTxt;
    [SerializeField] private Text Lv1CostTxt;
    [SerializeField] private Text Lv2CostTxt;
    [SerializeField] private Text Lv3CostTxt;
    [SerializeField] private Text roundTxt;
    [SerializeField] private Text remainEnemiesTxt;

    //EnemyPrefab
    [SerializeField] private GameObject[] Lv1Gos;

    public GameObject clickedElement;

    public int Round = 1;
    public int Gold = 50;
    private int Lv1Cost = 10;
    private int Lv2Cost = 50;
    private int Lv3Cost = 500;

    //Area
    public float minX = -1.5f;
    public float maxX = 1.5f;
    public float minY = -1.9f;
    public float maxY = 2.7f;

    //Percentage
    private float SpecialPercent = 5.0f;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        TextUpdate();
    }
    private void TextUpdate()
    {
        goldTxt.text = Gold.ToString();
        Lv1CostTxt.text = Lv1Cost.ToString() + "G";
        Lv2CostTxt.text = Lv2Cost.ToString() + "G";
        Lv3CostTxt.text = Lv3Cost.ToString() + "G";
        roundTxt.text = Round.ToString();
        remainEnemiesTxt.text = SpawnManager.instance.remainEnemy.ToString();
    }

    private Vector3 RandomPos()
    {
        float ranX = Random.Range(-1.5f, 1.5f);
        float ranY = Random.Range(-1.9f, 2.7f);

        return new Vector3(ranX, ranY,0);
    }
    public void BtnLv1Click()
    {
        if(Gold >= Lv1Cost)
        {
            Gold -= Lv1Cost;
            float specialPercent = Random.Range(0.0f, 100.0f);
            if (specialPercent > SpecialPercent)
            {
                int a = Random.Range(0, Lv1Gos.Length - 1);
                Instantiate(Lv1Gos[a], RandomPos(), Quaternion.identity);
            }
            else Instantiate(Lv1Gos[Lv1Gos.Length-1], RandomPos(), Quaternion.identity);


        }
    }
    public void BtnLv2Click()
    {

    }
    public void BtnLv3Click()
    {

    }

}
