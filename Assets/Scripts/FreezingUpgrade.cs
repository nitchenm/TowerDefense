using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezingUpgrade : TurretUpgrade
{
    public FreezingUpgrade(Turret turret) : base(turret)
    {
        turretDamage = turret.turretDamage;  //Maintain original attributes
        range = turret.range;
        fireRate = turret.fireRate;
    }



}
