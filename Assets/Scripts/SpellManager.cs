using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellManager : MonoBehaviour
{

    public static SpellManager instance;

    public GameObject firstSpell;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one SpellController in scene");
            Destroy(gameObject);
            return;
        }
            instance = this;
    }
    private GameObject spellToLaunch;
   public GameObject GetSpellToLaunch()
   {
            Debug.Log("ugestruges");
            return spellToLaunch;
   }


    public void SetSpellToLaunch(GameObject spell)
    {
        spellToLaunch = spell;
    }
   
}
