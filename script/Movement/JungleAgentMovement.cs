using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;
using UnityEngine.AI;

public class JungleAgentMovement : MonoBehaviour
{
    #region Variables

    public float FindEnemyDistance = 15f;
    public string EnemyTag = "Ally";
    public float DistanceAttack = 3;
    public float TimeGetDistance = 0.4f;
    public float SpawDistance = 75;
    public float AttackDelay = 2;
    public NavMeshAgent Agent;
    public Vector3 destination;
    public Vector3 StartPosition;
    public CharactereStatus AttackAgent;
    public SpawJungle Spaw;
    public AnimatorHandle Anim;

    private bool insideRange;
    private GameObject[] EnemyList;
    private float timeCheckList = 10;
    private bool giveXP;
    private GameObject Enemy;

    private float timeToAttack = 1;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        Agent = Agent.GetComponent<NavMeshAgent>();
        Anim.AnimationPlayIdle();
        giveXP = true;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        StartCoroutine(PopulateEnemyList());
        if (AttackAgent.stats.GetHealthPoints() <= 0)
        {
            StartCoroutine(Death());
            StopCoroutine(DestinationDefine());
            return;
        }
        else
        {
                StartCoroutine(DestinationDefine());
            
        }

        if (Vector3.Distance(destination, Spaw.transform.position) < SpawDistance)
        {
            insideRange = true;
        }
        else
        {
            insideRange = false;
        }
        float checkDistance = Vector3.Distance(transform.position, destination);
        CheckDistanceToAction(checkDistance);
        if (!insideRange)
        {
            destination = Spaw.transform.position;
        }
        Agent.SetDestination(destination);
    }

    private void CheckDistanceToAction(float checkDistance)
    {
        if (checkDistance < FindEnemyDistance && checkDistance > DistanceAttack)
        {
            Moving();
        }
        if (checkDistance < DistanceAttack)
        {
            Attacking();
        }
        if(checkDistance > FindEnemyDistance)
        {
            Idle();
        }
    }

    private void Idle()
    {
        if (destination == Spaw.transform.position)
        {
            Agent.isStopped = true;
        }
        else
        {
            Agent.isStopped = false;
        }
    }

    private void Attacking()
    {
        transform.LookAt(destination);
        Agent.isStopped = true;
        if (timeToAttack > AttackDelay)
        {
            Anim.AnimationPlayAttack();
            timeToAttack = 0;
        }
        else
        {
            Anim.AnimationPlayStop();
        }
    }

    private void Moving()
    {
        Vector3 lTargetDir = destination - transform.position;
        Anim.AnimatonPlayRun();
        Agent.isStopped = false;
        lTargetDir.y = 0.0f;
        if (lTargetDir != Vector3.zero)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lTargetDir), Time.time * 0.6f);
        }
    }

    private IEnumerator DestinationDefine()
    {
            for (var i = 0; i < EnemyList.Length; i++)
            {
                if (EnemyList[i] != null && Vector3.Distance(transform.position, EnemyList[i].transform.position) < Vector3.Distance(transform.position, destination) || Enemy == null)
                {
                    destination = EnemyList[i].transform.position;
                    Enemy = EnemyList[i];
                }
                yield return null;                
            }
        yield return new WaitForSeconds(TimeGetDistance);
    }
    public IEnumerator Death()
    {
        destination = transform.position;
        Anim.AnimationPlayDeath();
        Spaw.ThereIsMinion = false;
        Anim.enabled = false;
        
        if (EnemyTag == "Ally" && giveXP)
        {
            PlayerStatus[] players = PopulateArray.Players();
            int xp = AttackAgent.stats.GiveXp();
            foreach (var player in players)
            {
                if (Vector3.Distance(transform.position, player.transform.position) < 100)
                {
                    player.stats.GainXP(xp);
                }
                yield return null;
            }
            giveXP = false;
        }
        Destroy(gameObject, 1f);
    }
    private IEnumerator PopulateEnemyList()
    {
        EnemyList = GameObject.FindGameObjectsWithTag(EnemyTag);
        yield return new WaitForSeconds(timeCheckList);
    }
    public void PlayAudio(AudioSource step)
    {
        if (!step.isPlaying)
        {
            step.Play();
        }
    }
}
