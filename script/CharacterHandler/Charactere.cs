using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Charactere : MonoBehaviour, ICharactere
{
    public abstract float GetDamage();

    public abstract void ReceiveGold(float gold);

    public abstract float TakeDamage(float damage);
}
