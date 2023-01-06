using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharactere
{
        float TakeDamage(float damage);
        float GetDamage();
        void ReceiveGold(float gold);

}
