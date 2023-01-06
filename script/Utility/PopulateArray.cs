using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class PopulateArray : MonoBehaviour
{
    public static GameObject[] ListEnemies;
    public static GameObject[] ListAllyes;
    private float timePopulate = 10;
    private string enemy = "Enemy";
    private string ally = "Ally";

    private void Start()
    {
        ListEnemies = FindEnemyListByTag();
        ListAllyes = FindAllyListByTag();
    }
    private void FixedUpdate()
    {
        timePopulate += Time.deltaTime;
        if (timePopulate > 10)
        {
            ListEnemies = FindEnemyListByTag();
            ListAllyes = FindAllyListByTag();
            timePopulate = 0;
        }
    }
    public GameObject[] FindEnemyListByTag()
    {
        GameObject[] enemysList = GameObject.FindGameObjectsWithTag(enemy);
        return enemysList;
    }
    public GameObject[] FindAllyListByTag()
    {
        GameObject[] enemysList = GameObject.FindGameObjectsWithTag(ally);
        return enemysList;
    }

    public static GameObject[] GetListEnemy()
    {
        return ListEnemies;
    }
    public static GameObject[] GetListAlly()
    {
        return ListAllyes;
    }
    public static PlayerStatus[] Players()
    {
        return FindObjectsOfType<PlayerStatus>();

    }
}
