using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;

    private Node selectedNode;

    void Awake()
    {
        if (instance != null)

            Debug.LogError("More than one buildManager in scene");
        instance = this;
    }

    public GameObject standardTurretPrefab;
    public GameObject missileLauncherPrefab;

    private TurretBlueprint turretToBuild;


    public bool CanBuild { get { return turretToBuild != null; } }
    public bool HasCurrency { get { return PlayerStats.Currency >= turretToBuild.cost; } }

    public void ResetTurretSelection()
    {
        turretToBuild = null;
    }

    //method that checks if it is possible to build a turret on a node
    public void BuildTurretOn(Node node)
    {

        if (node.turret != null && node.multishotTurret != null && node.freezingTurret != null)
        {
            Debug.Log("Turret already placed on this node");
            return;
        }

        if (PlayerStats.Currency < turretToBuild.cost)
        { // check if currency is enough to build a turret
                Debug.Log("Not Enough Currency");
                return;
        }
        PlayerStats.Currency -= turretToBuild.cost;

        GameObject turret = (GameObject)Instantiate(turretToBuild.prefab, node.GetBuildPosition(), Quaternion.identity); // Instantiate turret to build
        node.turret = turret;

        turret.transform.SetParent(node.transform);

        Debug.Log("Turret Build. Currency Left: " + PlayerStats.Currency);
        
    }

    public void SelectTurretToBuild(TurretBlueprint turret)
    { // get the prefab of the turret to build
        turretToBuild = turret;
    }


    public void SetSelectedNode(Node node)
    {
        selectedNode = node;
    }


}
