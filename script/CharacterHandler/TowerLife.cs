using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;

public class TowerLife : Charactere
{
    #region Variables
    public float Health = 100;
    public float Mana = 100;
    public float Damage = 10;
    public int Gold = 30;
    public int Xp = 50;
    public Rigidbody childRigidBody;
    public bool Ally;
    private CharactereList CharList;
    
    public Stats stats;
    private UiHealthBar HealthBar;
    private float countToGetHit = 0.3f;
    private float timeToGetHit = 0.3f;
    private Rigidbody rigidBody;
    private Collider Collider;


    #endregion

    void Start()
    {
        CharList = FindObjectOfType<CharactereList>();
        if (Ally)
        {
            CharList.AllyList.Add(this.gameObject);
        }
        else
        {
            CharList.EnemyList.Add(this.gameObject);
        }
        stats = new Stats(Health, Mana, Damage, Gold, Xp);
        HealthBar = GetComponentInChildren<UiHealthBar>();
        HealthBar.SetMaxHealth((int)stats.GetHealthPoints());
        HealthBar.SetHealth(stats.GetHealthPoints());
        rigidBody = GetComponent<Rigidbody>();
        Collider = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        countToGetHit += Time.deltaTime;
        if (stats.GetHealthPoints() <= 0)
        {
            StartCoroutine(Death());
        }
    }
    public override float TakeDamage(float damage)
    {
        if (countToGetHit > timeToGetHit)
        {
            stats.GetHit(damage);
            HealthBar.SetHealth(stats.GetHealthPoints());
            countToGetHit = 0;
        }
        if (stats.GetHealthPoints() <= 0)
        {
            return stats.Gold;
        }
        return 0;
    }
    public IEnumerator Death()
    {
        if(stats.GetHealthPoints() <= 0)
        {
            Collider.enabled = false;
            rigidBody.useGravity = true;
            rigidBody.isKinematic = false;
            childRigidBody.useGravity = true;
            childRigidBody.isKinematic = false;
            if (Ally)
            {
                CharList.AllyList.Remove(this.gameObject);
            }
            else
            {
                CharList.EnemyList.Remove(this.gameObject);
            }
            yield return new WaitForSeconds(3);
            gameObject.SetActive(false);
            
        }
    }

    public override float GetDamage()
    {
        return stats.Damage;
    }

    public override void ReceiveGold(float gold)
    {
        
    }
}
