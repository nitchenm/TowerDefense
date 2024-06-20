using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiShotTurret : MonoBehaviour, ITurret
{
    private EnemyAI target;
    public GameObject menuCanvas;

    public Transform turretTransform => transform;

    public float range { get; set; }
    public float fireRate { get; set; }
    public int turretDamage { get; set; }
    public int currentTurretLevel { get; set; }

    private float lastShotTime = 0f;

    [Header("Unity Setup Fields")]

    public string enemyTag = "Enemy";

    public Transform partToRotate;
    public float turnSpeed = 10f;

    public GameObject bulletPrefab;
    public Transform firePoint;

    public int maxTargets= 3;




    void UpdateTarget()
    {

        target = EnemyManager.instance.GetNearestEnemy(transform.position, range);
    }
    void Update()
    {
        UpdateTarget();

        if (target == null)
            return;

        Vector3 dir = target.transform.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (Time.time > lastShotTime + 1f / fireRate)
        {
            Shoot();
            lastShotTime = Time.time;
        }

    }
    public void Shoot()
    {
        //List to store enemies within range
        List<EnemyAI> targetsInRange = new List<EnemyAI>();

        //Find all enemies within turrets range
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        foreach(Collider collider in colliders)
        {
            EnemyAI enemy = collider.GetComponent<EnemyAI>();
            if(enemy != null)
            {
                targetsInRange.Add(enemy); //Add enemy to the list if it is on the turrets range
            }
        }

        //Calculate the number of targets to shoot based on the minimum between maxTargets
        //and the current targets in the list
        int targetsToShoot = Mathf.Min(maxTargets, targetsInRange.Count);

        for (int i = 0; i < targetsToShoot; i++)
        {
            Vector3 direction = (targetsInRange[i].transform.position - firePoint.position).normalized;
            Quaternion rotation = Quaternion.LookRotation(direction);

            GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, rotation);
            MultiShotBullet bullet = bulletGO.GetComponent<MultiShotBullet>();
            bullet.SetTurretParent(this);
            bullet.Seek(targetsInRange[i].transform);
        }
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget.GetComponent<EnemyAI>();
    }

    public void ClearTarget()
    {
        target = null;
    }



    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, range);
    }

    public void DestroyTurret()
    {
        Destroy(gameObject);
    }
}

