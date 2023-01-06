using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacking : MonoBehaviour
{
    #region Variables
    public Charactere Charactere;
    public Collider Collider;
    public Transform corpo;
    public string EnemyTag = "Ally";
    public string TowerTag = "Tower";
    public float WeaponDistanceToAttack = 1f;
    public float WeaponDistanceToAttackMax = 5f;
    public float AttackInterval = 0.5f;
    public float AttackDistance;
    public AudioSource AttackSound;
    public ParticleSystem AttackSystem;
    private float timeAttack = 0f;
    
    #endregion

    private void Start()
    {
        Collider = Collider.GetComponent<Collider>();
        corpo = corpo.GetComponent<Transform>();
    }
    private void FixedUpdate()
    {
        AttackDistance = Vector3.Distance(transform.position, corpo.position);
        timeAttack += Time.deltaTime;
        if (AttackDistance > WeaponDistanceToAttack  &&  AttackDistance < WeaponDistanceToAttackMax && timeAttack > AttackInterval)
        {
            Collider.enabled = true;
            
        }
        else
        {
            Collider.enabled = false;
        }
    }
    public void AttackingEnemy(Collider other)
    {
        if (other.gameObject.CompareTag(EnemyTag))
        {
            Charactere.ReceiveGold(other.gameObject.GetComponent<ICharactere>().TakeDamage(Charactere.GetDamage()));
            Collider.enabled = false;
            timeAttack = 0;
            if (AttackSystem != null)
            {
                ParticleSystem p = Instantiate(AttackSystem);
                p.transform.position = transform.position;
                Destroy(p, 0.3f);
            }
            
            return;
        }
        if (other.gameObject.CompareTag(TowerTag))
        {
            Charactere.ReceiveGold(other.gameObject.GetComponent<ICharactere>().TakeDamage(Charactere.GetDamage()));

            if (AttackSystem != null)
            {
                ParticleSystem p = Instantiate(AttackSystem);
                p.transform.position = transform.position;
                Destroy(p, 0.3f);
            }
            Collider.enabled = false;
            timeAttack = 0;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        AttackingEnemy(other);
        PlayAudio(AttackSound);
    }
    public void PlayAudio(AudioSource audio)
    {
        if(audio != null)
        {
            if (!audio.isPlaying)
                audio.Play();
        }
        
    }
}
