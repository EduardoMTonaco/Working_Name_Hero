using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

    public class ProjectileMove : FindEnemyList
    {
        public float speed = 10;
        public float fireRate = 1;
        public string EnemyTag = "Ally";
        public string AllyTag = "Enemy";
        public string ProjectileTag = "Projectile";
        public float Damage = 50;
        public Vector3 posicao;
        private Collider colider;
        private void Start()
        {
            colider = GetComponent<Collider>();
        }
        private void Update()
        {
            if(speed != 0)
            {
                transform.position += transform.forward * (speed * Time.deltaTime);
                posicao = transform.position;
            }
            else
            {
                Debug.Log("No speed");
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(AllyTag) || other.gameObject.CompareTag(ProjectileTag))
            {
                Physics.IgnoreCollision(other, colider);
            }
            else
            {
                if (other.gameObject.CompareTag(EnemyTag))
                {

                    other.GetComponent<ICharactere>().TakeDamage(Damage);
                }
                Destroy(gameObject);
            }
            
        }
}
