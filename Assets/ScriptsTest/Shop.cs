using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour
{
    public TurretBlueprint standardTurret;
    public TurretBlueprint missileLauncher;
    BuildManager buildManager;
    private bool isTurretSelected = false;

    void Start() {
        buildManager = BuildManager.instance;
        
    }

    public void SelectStandardTurret(){
        Debug.Log("Standard Turret Purchased");
        buildManager.SelectTurretToBuild(standardTurret); //get blueprint prefab
     //   buildManager.SetTurretToBuild(buildManager.standardTurretPrefab);
      //  buildManager.SetTurretToBuild(buildManager.standardTurretPrefab);
        isTurretSelected = true;
    }

    public void SelectMissileLauncher(){
        Debug.Log("Missile Launcher Purchased");
        buildManager.SelectTurretToBuild(missileLauncher); //get blueprint prefab
   //     buildManager.SetTurretToBuild(buildManager.missileLauncherPrefab);
     //   buildManager.SetTurretToBuild(buildManager.missileLauncherPrefab);
        isTurretSelected = true;
    }

    public bool IsTurretSelected()
    {
        return isTurretSelected;
    }

    public void ResetTurretSelection()
    {
        isTurretSelected = false;
    }
}
