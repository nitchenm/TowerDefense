using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezingBullet : MonoBehaviour
{
    public int freezingAmmount;
    public int freezingRadius;
    public int freezingDuration;

    private Transform target;

    public float speed = 70f;
    public int turretDamage;
    public float explosionRadius = 0f;

    public GameObject impactEffect;

    private FreezingTurret parentTurret;

    private void Awake()
    {

    }

    public void Seek(Transform _target) // gives position of target
    {
        target = _target;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;

        if (dir.magnitude <= distanceThisFrame)
        {

            HitTarget();
            return;
        }
        transform.Translate(dir.normalized * distanceThisFrame, Space.World);
        transform.LookAt(target);
    }

    public void SetTurretParent(FreezingTurret turret)
    {
        parentTurret = turret;
    }

    void HitTarget()
    { //bullet hits target and damage/destroy it with effects
        GameObject effectIns = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        // Destroy(target.gameObject);
        Destroy(effectIns, 5f);

        if (explosionRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }

        Destroy(gameObject);

    }

    void Explode() // destroy/damage target with effects

    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Enemy")
            {
                Damage(collider.transform);
            }
        }
    }


    void Damage(Transform enemy)
    {
        EnemyAI e = enemy.GetComponent<EnemyAI>();

      //  Debug.Log(e);

        if (e != null)
        {
            SlowMovement(e.transform);
            e.TakeDamage(parentTurret.turretDamage);
        }

    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }


    void SlowMovement(Transform enemyTransform)
    {
        EnemyAI enemy = enemyTransform.GetComponent<EnemyAI>();
        Debug.Log(enemy);

        if (enemy.isFrozen)
        {
            return;
        }

        else if (enemy != null)
        {
            // Apply the freeze effect by reducing the enemy's speed
            float originalSpeed = enemy.speed;
            float slowedSpeed = originalSpeed * (1f - (parentTurret.freezingAmount / 100f));
            enemy.SetSpeed(slowedSpeed);
            enemy.isFrozen = true;
          
          //  Debug.Log(originalSpeed + "y" + slowedSpeed);

            Debug.Log("enemigo ralentizado");
            // Start a coroutine to revert the enemy's speed after the freeze duration
            StartCoroutine(RevertSpeedAfterDuration(enemy, originalSpeed, parentTurret.freezingDuration));
        }
    }

    IEnumerator RevertSpeedAfterDuration(EnemyAI enemy, float originalSpeed, float duration)
    {
        // Wait for the specified duration
        yield return new WaitForSeconds(duration);

        // Revert the enemy's speed back to its original value
        enemy.SetSpeed(originalSpeed);
        enemy.isFrozen = false;
       
    }
}

