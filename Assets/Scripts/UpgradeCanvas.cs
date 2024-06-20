using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeCanvas : MonoBehaviour
{

   // public Turret selectedTurret;
    private Turret upgradedTurret;

    private ITurret selectedTurret;

    public GameObject freezingTurretPrefab;
    public GameObject multiShotTurretPrefab;

    private bool isFreezingTurret;
    private bool isMultiShotTurret;

    private FreezingTurret upgradedFreezingTurret;
    private MultiShotTurret upgradedMultiShotTurret;


    public Vector3 offset;
    public Vector3 rotation;

    private Transform turretTransform;

    public int upgradeRangeCost;
    public int upgradeDamageCost;
    public int upgradeFireRateCost;

    public int upgradeFreezingCost;
    public int upgradeMultiShotCost;

    public UpgradeCanvas upgradeTurretCanvas;

    public Node node; 

    

    private void Start()
    {
        selectedTurret = GetComponentInParent<Transform>().GetComponentInParent<ITurret>();
        upgradedTurret = GetComponentInParent<Turret>();
        turretTransform = selectedTurret.turretTransform;
     //   transform.SetParent(null); // Unparent the canvas from the turret
        transform.position = turretTransform.position + offset; // Set the initial position of the canvas
        isFreezingTurret = false;
        node = GetComponentInParent<Node>();

    }
    private void LateUpdate()
    {
        // Update the position of the canvas to follow the turret's position
        transform.position = turretTransform.position + offset;
        transform.rotation = Quaternion.Euler(rotation);
    }
    public void CloseUpgradeMenu()
    {
        gameObject.SetActive(false);
    }

  

    public void UpgradeRange()
    {
        if (PlayerStats.Currency >= upgradeRangeCost)
        {
            PlayerStats.Currency -= upgradeRangeCost;
            selectedTurret.currentTurretLevel++;
            selectedTurret.range += 10;
            Debug.Log("Upgrade purchased. Currency Left: " + PlayerStats.Currency);
        }
        if (selectedTurret.currentTurretLevel == 5)
        {
            CloseUpgradeMenu();
            upgradedTurret.OpenUpgradeTurretCanvas();
        }
    }
    public void UpgradeDamage()
    {
        if (PlayerStats.Currency >= upgradeDamageCost)
        {
            PlayerStats.Currency -= upgradeDamageCost;
            selectedTurret.currentTurretLevel++;
            selectedTurret.turretDamage += 5;
            Debug.Log("Upgrade purchased. Currency Left: " + PlayerStats.Currency);

            if(selectedTurret.currentTurretLevel == 5)
            {
                CloseUpgradeMenu();
                upgradedTurret.OpenUpgradeTurretCanvas();
            }
        }
    }

    public void UpgradeFireRate()
    {
        if (PlayerStats.Currency >= upgradeFireRateCost)
        {
            PlayerStats.Currency -= upgradeFireRateCost;
            selectedTurret.currentTurretLevel++;
            selectedTurret.fireRate += (float)0.1;
            Debug.Log("Upgrade purchased. Currency Left: " + PlayerStats.Currency);
        }
        if (selectedTurret.currentTurretLevel == 5)
        {
            CloseUpgradeMenu();
            upgradedTurret.OpenUpgradeTurretCanvas();
        }
    }

    public void UpgradeToFreezingTurret()
    {
        if (PlayerStats.Currency >= upgradeFreezingCost)
        {
            PlayerStats.Currency -= upgradeFreezingCost;
            selectedTurret.currentTurretLevel++;

            GameObject newTurret = Instantiate(freezingTurretPrefab, selectedTurret.turretTransform.position, selectedTurret.turretTransform.rotation);

            FreezingTurret newFreezingTurret = newTurret.GetComponent<FreezingTurret>();

            // Transfer the common attributes between Turret and FreezingTurret
            newFreezingTurret.turretDamage = ((Turret)selectedTurret).turretDamage;
            newFreezingTurret.range = ((Turret)selectedTurret).range;
            newFreezingTurret.fireRate = ((Turret)selectedTurret).fireRate;
            newFreezingTurret.currentTurretLevel = ((Turret)selectedTurret).currentTurretLevel;

            node.SetTurret(newTurret);

            // Check if the selected turret is a FreezingTurret, and if so, transfer its freezing attributes
            if (isFreezingTurret)
            {
                newFreezingTurret.freezingAmount = upgradedFreezingTurret.freezingAmount;
                newFreezingTurret.freezingDuration = upgradedFreezingTurret.freezingDuration;
            }

            selectedTurret.DestroyTurret();

            selectedTurret = (ITurret)newFreezingTurret;
            isFreezingTurret = true;
            upgradedFreezingTurret = newFreezingTurret;

            

            Destroy(gameObject);
            selectedTurret.currentTurretLevel++;
            
        }
    }

    public void sell(){
        
    }
    public void UpgradeToMultiShotTurret()
    {
        if(PlayerStats.Currency >= upgradeMultiShotCost)
        {
            PlayerStats.Currency -= upgradeMultiShotCost;
            selectedTurret.currentTurretLevel++;

            GameObject newTurret = Instantiate(multiShotTurretPrefab, selectedTurret.turretTransform.position, selectedTurret.turretTransform.rotation);
            MultiShotTurret newMultiShotTurret = newTurret.GetComponent<MultiShotTurret>();

            newMultiShotTurret.turretDamage = ((Turret)selectedTurret).turretDamage;
            newMultiShotTurret.range = ((Turret)selectedTurret).range;
            newMultiShotTurret.fireRate = ((Turret)selectedTurret).fireRate;
            newMultiShotTurret.currentTurretLevel = ((Turret)selectedTurret).currentTurretLevel;

            node.SetTurret(newTurret);

            if (isMultiShotTurret)
            {
                newMultiShotTurret.maxTargets = upgradedMultiShotTurret.maxTargets;

            }

            selectedTurret.DestroyTurret();

            selectedTurret = (ITurret)newMultiShotTurret;
            isMultiShotTurret = true;
            upgradedMultiShotTurret = newMultiShotTurret;

            Destroy(gameObject);
            selectedTurret.currentTurretLevel++;
            
        }
    }
}
