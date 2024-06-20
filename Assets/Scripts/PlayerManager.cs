using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IDamageable
{

    public string enemyTag = "Enemy";

    private Spawner spawner;

    public float maxHp;
    public float currentHp;


    void Start()
    {
        currentHp = maxHp;
        spawner = FindObjectOfType<Spawner>();
    }

   public void TakeDamage(float damage)
    {
        currentHp -= damage;
    }
}
