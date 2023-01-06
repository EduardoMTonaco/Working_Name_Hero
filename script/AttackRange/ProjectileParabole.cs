using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileParabole : MonoBehaviour
{
    #region Variables
    public float timeToGetThere = 5f;
	public float CastTime;
    
	public bool Loop;
    public Vector3 StartPosition;
    public Vector3 EndPosition;


    public string AllyTag = "Ally";
    public string EnemyTag = "Enemy";
    public string TowerTag = "TowerEnemy";
    public string ProjectileTag = "Projectile";
    public float Damage;

    public ParticleSystem Trail;
    public ParticleSystem DestructionEffect;

    private Collider colider;
    private bool stop;
    private float time;
    private float height;
    #endregion

    private void Start()
    {
        colider = GetComponent<Collider>();

        if (timeToGetThere <= 0)
		{
			timeToGetThere = 1f;
		}
        stop = false;
        height = Vector3.Distance(StartPosition, EndPosition) / 10;

    }
    void Update()
	{
		time += Time.deltaTime;
		if(Loop)
        {
			time = time % CastTime;
		}
		if(time > timeToGetThere)
        {
			Destroy(gameObject);
        }
        if(!stop)
        {
            MoveArrow();
        }
	}
	void MoveArrow()
	{
		transform.position = MathParabola.Parabola(StartPosition, EndPosition, height, time/ timeToGetThere);
	}
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(AllyTag) || other.gameObject.CompareTag(ProjectileTag))
        {
            Physics.IgnoreCollision(other, colider);
        }
        else
        {
            if (other.gameObject.CompareTag(EnemyTag) || other.gameObject.CompareTag(TowerTag))
            {
                other.gameObject.GetComponent<ICharactere>().TakeDamage(Damage);
            }
            if (Trail != null)
            {
                Trail.gameObject.SetActive(false);
            }
            if (DestructionEffect != null)
            {
                DestructionEffect.gameObject.SetActive(true);
            }
            stop = true;
            Destroy(gameObject, 0.3f);
        }
    }
}
