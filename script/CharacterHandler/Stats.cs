using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets
{
    public class Stats
    {
        #region Variables
        public float HealthPointsMax;
        public float ManaPointsMax;
        public float AttackSpeed;
        public float Damage;
        public bool Alive;
        public int Str;
        public int Agi;
        public int Int;
        public int Xp;
        public int Level;
        public PrimaryAttribute Atribute;
        public int Gold { set; get; }

        private float CurrentHealthPoints;
        private float CurrentManaPoints;



        private float _timeRegeneration;
        #endregion


        public Stats(int str, int agi, int _int, PrimaryAttribute atribute)
        {
            Atribute = atribute;
            DefineStatus(str, agi, _int);
            Xp = 0;
        }

        private void DefineStatus(int str, int agi, int _int)
        {
            Str += str;
            Agi += agi;
            Int += _int;
            HealthPointsMax = 100 + (Str * 25);
            ManaPointsMax = 100 + (Int * 25);
            AttackSpeed = 1 * (agi / 100);
            Alive = true;
            Level = 1;
            _timeRegeneration = 1;
            if (Atribute == PrimaryAttribute.StrHero)
            {
                Damage = Str * 1.3f;
            }
            if (Atribute == PrimaryAttribute.AgiHero)
            {
                Damage = Agi * 1.3f;
            }
            if (Atribute == PrimaryAttribute.IntHero)
            {
                Damage = Int * 1.3f;
            }
            CurrentHealthPoints = HealthPointsMax;
            CurrentManaPoints = ManaPointsMax;
        }

        public Stats(float healthPoints, float manaPoints, float damage, int gold, int xp)
        {
            HealthPointsMax = healthPoints;
            CurrentHealthPoints = healthPoints;
            Str = (int)(healthPoints / 100);
            ManaPointsMax = manaPoints;
            Damage = damage;
            _timeRegeneration = 1;
            Gold = gold;
            Xp = xp;
        }

        public void IsAlive()
        {
            if(HealthPointsMax <= 0)
            {
                Alive = false;
            }
        }
        public void GainXP(int xp)
        {
            Xp += xp;
            if (Xp >= 100 * Level)
            {
                Xp = 0;
                Level++;
                LevelUp();
            }
        }
        public void LevelUp()
        {
            DefineStatus(10, 10, 10);
        }
        public int GiveXp()
        {
            return Xp;
        }
        public void GetHit(float damage)
        {
            CurrentHealthPoints -= damage;
        }
        public void Regenaration()
        {
            _timeRegeneration += Time.deltaTime;
            if (_timeRegeneration >= 1)
            {
                CurrentHealthPoints += Str / 5;
                _timeRegeneration = 0;
            }
        }
        public float GetHealthPoints()
        {
            return CurrentHealthPoints;
        }
        public float GetManaPoints()
        {
            return CurrentManaPoints;
        }
        
    }
    public enum PrimaryAttribute
    {
        StrHero,
        AgiHero,
        IntHero
    }

}
