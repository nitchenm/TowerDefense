using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TurretUpgrade : Turret
{
    protected Turret turret;

    public TurretUpgrade(Turret turret)
    {
        this.turret = turret;
    }
}
