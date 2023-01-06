using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;

public class PlayerStatus : Charactere
{
    public int Str  = 100;
    public int Agi = 100;
    public int Int = 100;
    public PrimaryAttribute atribute;

    [HideInInspector]
    public UiHealthBar HealthBar;

    [HideInInspector]
    public Stats stats;
    private void Start()
    {
        stats = new Stats(Str, Agi, Int, atribute);
        HealthBar = GetComponentInChildren<UiHealthBar>();
        HealthBar.SetMaxHealth((int)stats.GetHealthPoints());
        HealthBar.SetHealth(stats.GetHealthPoints());
    }
    public void Update()
    {
        Str = stats.Str;
        Agi = stats.Agi;
        Int = stats.Int;
    }
    public override float GetDamage()
    {
        return stats.Damage;
    }
    public override void ReceiveGold(float gold)
    {
        stats.Gold += (int)gold;
    }
    public override float TakeDamage(float damage)
    {
        stats.GetHit(damage);
        HealthBar.SetHealth(stats.GetHealthPoints());
        return 0;
    }
    public void GetXp(int xp)
    {

    }
}
