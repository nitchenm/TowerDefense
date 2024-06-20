using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor;
    public Color notEnoughCurrencyColor;



    public Vector3 positionOffset;

    [Header("Optional")]
    public GameObject turret;

    public FreezingTurret freezingTurret;
    public MultiShotTurret multishotTurret;

    private Renderer rend;
    private Color startColor;

    BuildManager buildManager;

    private Node selectedNode;

    public UpgradeCanvas upgradeCanvas;

    public bool isOccupied = false;
    
 
    void Start()
    {
        rend = GetComponent<Renderer>();
        startColor = rend.material.color;
        buildManager = BuildManager.instance;
        selectedNode = null;
      
        
        
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) //Checks if there is a UI object 
            return;

        if (isOccupied)
        {
               ITurret turretComponent = turret.GetComponent<ITurret>();

            if (turretComponent != null)
            {
                int currentTurretLevel = turretComponent.currentTurretLevel;
                Turret selectedTurret = turretComponent as Turret;

                if (selectedTurret != null)
                {

                    if (currentTurretLevel < 5)
                    {
                        selectedTurret.menuCanvas.SetActive(true);
                    }
                    else if (currentTurretLevel == 5)
                    {

                        selectedTurret.upgradeTurretMenuCanvas.SetActive(true);
                    }
                }
            }
         
            return;
        }

 
        if(!buildManager.CanBuild)
          return;

        if (!IsPointerOverUIObject() && !freezingTurret && !multishotTurret) // if there is no turret and no UI element, it will instantiate the selected turret 
        {
            Debug.Log("deberia instasnciar");
            buildManager.BuildTurretOn(this);
            isOccupied = true;
            return;
        }
    }
 

    public Vector3 GetBuildPosition(){
        return transform.position + positionOffset;
    }

    bool IsPointerOverUIObject()
    {
    PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
    eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    List<RaycastResult> results = new List<RaycastResult>();
    EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
    return results.Count > 0;
    }
    
    void OnMouseEnter() {
      
      
        if(EventSystem.current.IsPointerOverGameObject())
        return;


        if(!buildManager.CanBuild)
          return;
    
       
        if(buildManager.HasCurrency){
           rend.material.color = hoverColor;
        }else{
            rend.material.color = notEnoughCurrencyColor;
        }
         
    }

    void OnMouseExit()
    {
        rend.material.color = startColor;
    }



    public Node GetSelectedNode()
    {
        return selectedNode;
    }

    public void SetTurret(GameObject newTurret)
    {
        turret = newTurret;
    }

 
}
