using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TurretLevelUI : MonoBehaviour
{
    private ITurret turret;
    public TextMeshProUGUI turretLevel;
    public TextMeshProUGUI currentDamage;
    public TextMeshProUGUI currentRange;
    public TextMeshProUGUI currentFireRate;

    private void Start()
    {
        turret = GetComponentInParent<ITurret>();
    }

    void Update()
    {
        if(turret != null && turret.turretTransform.gameObject.activeInHierarchy)
        {
            turretLevel.text = turret.currentTurretLevel.ToString();
            currentDamage.text = turret.turretDamage.ToString();
            currentRange.text = turret.range.ToString();
            currentFireRate.text = turret.fireRate.ToString();
           
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
