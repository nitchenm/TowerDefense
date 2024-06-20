using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LivingEntity : MonoBehaviour   , IDamageable
{
   
    [SerializeField]
    private int maxHealth;
 
    [SerializeField]
    private float resistance;
    public float health = 100;
    public int EnemyCurrency = 20;

    public UnityEvent onDeath;
    EnemyManager enemyManager;
    
    

    private Spawner spawner;

    public RectTransform healthBar;

    void Start()
    {
        health = maxHealth;
     
        spawner = FindObjectOfType<Spawner>();
        enemyManager = EnemyManager.instance;
    }
    
    
    public void TakeDamage(float damage) //Apply damage after the resistance
    {
        float damageAfterResistance = damage * (1 - resistance);        
        health -= damageAfterResistance;
        if(health <= 0f)
        {
            Die();
        }
        healthBar.sizeDelta = new Vector2(health/2, healthBar.sizeDelta.y);
        
    }

   void Die(){
      
      
      PlayerStats.Currency += EnemyCurrency;
      Debug.Log("Currency Gained: "+ PlayerStats.Currency);
      EnemyAI enemyAI = GetComponent<EnemyAI>();
      enemyManager.UnregisterEnemy(enemyAI);
       enemyAI.onDestroyed.Invoke();
      ObjectPooler.ReturnToPool(gameObject);
      health = maxHealth;
      
        
   }
}
