using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindEnemyList : MonoBehaviour
{
    
    public Vector3 FindEnemyDestination(GameObject[] enemysList)
    {
        Vector3 dest = Vector3.zero;
        for (var i = 0; i < enemysList.Length; i++)
        {
            if (enemysList[i] != null && Vector3.Distance(transform.position, enemysList[i].transform.position) < Vector3.Distance(transform.position, dest))
            {
                dest = enemysList[i].transform.position;
            }
        }
        return dest;
    }
}
