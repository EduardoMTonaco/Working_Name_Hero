using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;

public class TowerAttack : FindEnemyList
{
    #region Variables
    public string EnemyTag = "Ally";
    public string AllyTag = "Enemy";
    
    public ProjectileMove CannonBall;
    public GameObject Cannon;
    public float FindEnemyDistance = 30f;
    public float AttackDelay = 1f;
    public float BallSpeed = 25;
    public float Damage = 50;
    public TowerLife Tower;


    public Quaternion AAA;

    private Vector3 enemyLocation;
    private GameObject[] EnemyList;
    private float timeCheckList = 10;
    private float timeToAttack = 1f;
    private Rigidbody TopoTorre;
    #endregion

    void Start()
    {
        TopoTorre = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        TowerRotation();
    }

    private void TowerRotation()
    {
        StartCoroutine(PopulateEnemyList());
        Vector3 enemy = FindEnemyDestination(EnemyList);
        enemyLocation = enemy;
        float diference = Vector3.Distance(enemy, transform.position);
        if (diference < FindEnemyDistance)
        {
            timeToAttack += Time.deltaTime;
            Vector3 direction = enemy - transform.position;
            
            direction.y = 0;            
            TopoTorre.MoveRotation(Quaternion.LookRotation(direction).normalized);
            if(timeToAttack > AttackDelay)
            {
                ShotCannonBall();
                timeToAttack = 0;
            }
        }
    }
    void ShotCannonBall()
    {
        ProjectileMove ball = Instantiate(CannonBall, Cannon.transform.position, Cannon.transform.rotation);
        ball.transform.LookAt(enemyLocation + Vector3.up);
        ball.speed = BallSpeed;
        ball.Damage = Damage;
        ball.EnemyTag = EnemyTag;
        ball.AllyTag = AllyTag;
    }
    private IEnumerator PopulateEnemyList()
    {
        EnemyList = GameObject.FindGameObjectsWithTag(EnemyTag);
        yield return new WaitForSeconds(timeCheckList);
    }

}
