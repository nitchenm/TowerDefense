using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour, ITurret
{

       private EnemyAI target;
       public GameObject menuCanvas;
       public GameObject upgradeTurretMenuCanvas;

    public int currentTurretLevel { get; set; }

       public Transform turretTransform => transform;

       
       public float range { get; set; }
       public float fireRate { get; set; }
       public int turretDamage { get; set; }

       private float lastShotTime = 0f;
    [Header("Unity Setup Fields")]

       public string enemyTag = "Enemy";
       public Transform partToRotate;
       public float turnSpeed = 10f;
       public GameObject bulletPrefab;
       public Transform firePoint;


    void Start()
    {
       

        range = 10f;
        fireRate = 1f;
        turretDamage = 10;
         //InvokeRepeating("UpdateTarget", 0f, 0.5f);
        
    }

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
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        bullet.SetTurretParent(this);
        bullet.Seek(target.transform);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget.GetComponent<EnemyAI>();
    }

    public void ClearTarget()
    {
        target = null;
    }

    public void OpenUpgradeTurretCanvas()
    {
        upgradeTurretMenuCanvas.SetActive(true);
    }

    

   void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(this.transform.position, range);
	}

    public void DestroyTurret()
    {
        Destroy(gameObject);
    }
}
