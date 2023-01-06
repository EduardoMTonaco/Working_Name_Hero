using System.Collections;
using System.Collections.Generic;
using Assets;
using UnityEngine;

public class CastProjectile : MonoBehaviour
{
    public ProjectileParabole projetil;


    public Transform HitPlace;
    public float AttackInterval = 0.1f;
    public float TimeToGetThere = 2f;
    public float AimSpeed = 10f;
    public float MaxDistance = 20f;

    public string AllyTag = "Ally";
    public string EnemyTag = "Enemy";
    public PlayerStatus Char;
    private float time = 0;
    private float MinDistance = 2.96f;
    
    private float distance;

    void Update()
    {
        MoveAim();
        distance = HitPlace.position.z - transform.position.z;
        time += Time.deltaTime;
        if (Input.GetButton("Fire1"))
        {
            ShotArrow();
        }
    }

    private void MoveAim()
    {
        if (AimSpeed == 0)
        {
            AimSpeed = 1;
        }
        if (Input.GetAxis("Mouse Y") > 0)
        {
            if (distance < MaxDistance)
            {
                HitPlace.position += transform.position * (Time.deltaTime * AimSpeed);

            }
        }
        if (Input.GetAxis("Mouse Y") < 0)
        {
            if (distance > MinDistance)
            {
                HitPlace.position -= transform.position * (Time.deltaTime * AimSpeed);
            }
        }
    }

    private void ShotArrow()
    {
        if (time > AttackInterval)
        {
            ProjectileParabole p = Instantiate(projetil, transform.position, transform.rotation);
            p.StartPosition = transform.position;
            p.EndPosition = HitPlace.position;
            p.AllyTag = AllyTag;
            p.EnemyTag = EnemyTag;
            p.timeToGetThere = TimeToGetThere;
            p.Damage = Char.GetDamage();
            time = 0;
        }
    }
}
