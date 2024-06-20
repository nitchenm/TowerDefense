using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Transform target;

    public float speed = 70f;
    public int turretDamage;
    public float explosionRadius = 0f;

    public GameObject impactEffect;

    private Turret parentTurret;


    private void Awake()
    {
        
    }

    public void Seek(Transform _target) // gives position of target
    {
      target = _target; 
    }

    void Update()
    {
        if( target == null){
            Destroy(gameObject);
            return;
        }
        
        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed* Time.deltaTime;

        if(dir.magnitude <= distanceThisFrame){

         HitTarget();
         return;
        }
        transform.Translate (dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    public void SetTurretParent(Turret turret)
    {
        parentTurret = turret;
    }

    void HitTarget(){ //bullet hits target and damage/destroy it with effects
       GameObject effectIns =  (GameObject)Instantiate(impactEffect,transform.position, transform.rotation);
      // Destroy(target.gameObject);
       Destroy(effectIns,5f);

       if(explosionRadius > 0f)
       {
        Explode();
       }else
       {
        Damage(target);
       }

       Destroy(gameObject);
    
    }

    void Explode() // destroy/damage target with effects
   
    {
      Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);  
      foreach(Collider collider in colliders)
      {
        if(collider.tag == "Enemy")
        {
          Damage(collider.transform);
        }
      }
    }


    void Damage (Transform enemy)
    {
      
      EnemyAI e = enemy.GetComponent<EnemyAI>();

      if(e!= null){
          e.TakeDamage(parentTurret.turretDamage);
      }

    }
    

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
