using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellController : MonoBehaviour
{

    SpellManager spellManager;
    Camera mainCamera;
    

    

    private void Start()
    {
        spellManager = SpellManager.instance;
        mainCamera = Camera.main;
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            CastSpell();
        }
    }

    public void CastSpell()
    {
        if (spellManager.GetSpellToLaunch() == null)
        {
            Debug.Log("No spell is set");
            return;
        }
        // Cast a ray from the mouse position on the screen to determine the targerposition
        Ray castPoint = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(castPoint, out hit, Mathf.Infinity))
        {
            LaunchSpell(hit.point); // Launch the spell towards the hit point from the raycast
        }
    }

    public void LaunchSpell(Vector3 targetPosition)
    {
        GameObject spellToCast = spellManager.GetSpellToLaunch();
       
        Vector3 launchPosition = mainCamera.transform.position + Vector3.up; // Position above the camera
        Quaternion launchRotation = Quaternion.LookRotation(targetPosition - launchPosition); // Calculate the rotation to face the target position
        // Instantiate the spell at the launch position
        GameObject castedSpell = Instantiate(spellToCast, launchPosition, launchRotation);
        Spell castedSpellComponent = castedSpell.GetComponent<Spell>();
        float castedSpellSpeed = castedSpellComponent.speed;

        castedSpellComponent.Launch(targetPosition); //Call the launch method of the spell to start the spell movement

        spellManager.SetSpellToLaunch(null);

    }
  
}

