using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;

public class CharactereStatus : Charactere
{

    public float HealthPoints = 100;
    public float ManaPoints = 50;
    public float Damage = 10;
    public float DistanceAttack = 3;
    public int xp = 25;
    public int gold = 25;


    private UiHealthBar HealthBar;

    [HideInInspector]
    public Stats stats;
    private void Start()
    {
        ReviveChar();
    }

    public void ReviveChar()
    {
        stats = new Stats(HealthPoints, ManaPoints, Damage, gold, xp);
        HealthBar = GetComponentInChildren<UiHealthBar>();
        HealthBar.SetMaxHealth((int)stats.GetHealthPoints());
        HealthBar.SetHealth(stats.GetHealthPoints());
    }

    public override void ReceiveGold(float gold)
    {

    }
    public override float TakeDamage(float damage)
    {
        stats.GetHit(damage);
        HealthBar.SetHealth(stats.GetHealthPoints());
        if (stats.GetHealthPoints() <= 0)
        {
            return stats.Gold;
        }
        return 0;
    }
    public override float GetDamage()
    {
        return stats.Damage;
    }

}
