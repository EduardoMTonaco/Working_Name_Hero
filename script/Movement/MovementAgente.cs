using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class MovementAgente : MonoBehaviour
{
    #region Variables
    public NavMeshAgent Agent;
    public Animator Anim;
    public float FindEnemyDistance = 15f;
    public string EnemyTag = "Ally";
    public string TowerTag = "Tower";

    public float TimeGetDistance = 2f;
    public AudioSource StepAudio;
    public CharactereStatus AttackAgent;

    private bool giveXp;
    public GameObject enemy;


    public Vector3 destination;
    [HideInInspector]
    public GameObject TowerDestination;
    public bool Ally;
    public CharactereList CharList;
    public List<GameObject> EnemyList;
    #endregion
    public float diference;

    // Start is called before the first frame update
    void Start()
    {
        CharList = FindObjectOfType<CharactereList>();
        destination = transform.position;
        if(this.gameObject.activeSelf)
        {
            if (Ally)
            {
                CharList.AllyList.Add(this.gameObject);
                EnemyList = CharList.EnemyList;
            }
            else
            {
                CharList.EnemyList.Add(this.gameObject);
                EnemyList = CharList.AllyList;
            }
        }
       
                    
        Revive();
        Agent = Agent.GetComponent<NavMeshAgent>();
        Anim = Anim.GetComponent<Animator>();
        TowerDestination = GameObject.FindWithTag(TowerTag);
        enemy = TowerDestination;
        destination = TowerDestination.transform.position;
        Agent.SetDestination(destination);
        StartCoroutine(DestinationDefine());
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (AttackAgent.stats.GetHealthPoints() <= 0)
        {
            StopCoroutine(DestinationDefine());
            StartCoroutine(Death());
        }
        if (Vector3.Distance(destination, transform.position) < AttackAgent.DistanceAttack||
            Vector3.Distance(TowerDestination.transform.position, transform.position) < 8)

        {
            Attacking();
        }
        else
        {
            Moving();
        }
        destination = enemy.transform.position;
        Agent.SetDestination(destination);
    }

    private void Attacking()
    {
        Anim.SetBool("Move", false);
        Anim.SetBool("Attack", true);
        Agent.isStopped = true;
        Vector3 lTargetDir = destination - transform.position;
        lTargetDir.y = 0.0f;
        if (lTargetDir != Vector3.zero)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(lTargetDir), Time.time * 0.6f);
        }
    }

    private void Moving()
    {
        Anim.SetBool("Move", true);
        Anim.SetBool("Attack", false);
        PlayAudio(StepAudio);
        Agent.isStopped = false;

    }

    private IEnumerator DestinationDefine()
    {
        while (true) 
        {
            
            for (var i = 0; i < EnemyList.Count; i++)
            {
                if (Vector3.Distance(transform.position, EnemyList[i].transform.position) <
                    Vector3.Distance(transform.position, destination)
                    || Vector3.Distance(destination, transform.position) > FindEnemyDistance
                    || enemy.activeSelf == false)
                {

                    enemy = EnemyList[i];
                    destination = enemy.transform.position;
                    diference = Vector3.Distance(destination, transform.position);
                    if (diference < FindEnemyDistance)
                    {
                        break;
                    }
                }
            }
            diference = Vector3.Distance(destination, transform.position);
                if (diference > FindEnemyDistance || enemy.activeSelf == false)
                {
                enemy = TowerDestination;
                }

            yield return new WaitForSeconds(TimeGetDistance);
        }
    }

    public void PlayAudio(AudioSource step)
    {
        if (!step.isPlaying)
        {
            step.Play();
        }
    }
    public IEnumerator Death()
    {
        
        Anim.SetBool("Death", true);
        destination = transform.position;
        if (EnemyTag == "Ally" && giveXp)
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
            giveXp = false;
        }
        yield return new WaitForSeconds(2.5f);
        if (Ally)
        {
            CharList.AllyList.Remove(this.gameObject);
        }
        else
        {
            CharList.EnemyList.Remove(this.gameObject);
        }
        this.gameObject.SetActive(false);
    }
    public void Revive()
    {
        giveXp = true;
        AttackAgent.ReviveChar();
    }

}
