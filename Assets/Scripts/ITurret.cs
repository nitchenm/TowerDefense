using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ITurret 
{
    int turretDamage { get; set; }
    float range { get; set; }
    float fireRate { get; set; }

    void Shoot();
    void SetTarget(Transform newTarget);

    void ClearTarget();

    Transform turretTransform { get; }
    int currentTurretLevel { get; set; }

    void DestroyTurret();
}
