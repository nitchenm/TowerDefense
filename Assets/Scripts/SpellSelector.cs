using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellSelector : MonoBehaviour
{
    SpellManager spellManager;

     void Start()
    {
        spellManager = SpellManager.instance;
       
    }



    public void SelectFirstSpell()
    {
        if (spellManager.firstSpell == null)
        {
            Debug.Log("se paso ");

        }
            Debug.Log("You choose the first spell");
            spellManager.SetSpellToLaunch(spellManager.firstSpell);

  
    }
}
