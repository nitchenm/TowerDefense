using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager instance;

    public List<EnemyAI> activeEnemies = new List<EnemyAI>();
    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("More than one EnemyManager instance in the scene");
            Destroy(gameObject);
            return;
        }
        instance = this;
    }

    public void RegisterEnemy (EnemyAI enemy)
    {
        if (!activeEnemies.Contains(enemy))
        {
            activeEnemies.Add(enemy);
        }
    }

    public void UnregisterEnemy(EnemyAI enemy)
    {
        activeEnemies.Remove(enemy);
    }

    public EnemyAI GetNearestEnemy(Vector3 position, float maxRange)
    {
        float shortestDistance = Mathf.Infinity;
        EnemyAI nearestEnemy = null;

        foreach(EnemyAI enemy in activeEnemies)
        {
            float distanceToEnemy = Vector3.Distance(position, enemy.transform.position);
            if(distanceToEnemy < shortestDistance && distanceToEnemy <= maxRange)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }
        return nearestEnemy;
    }

    public void OnEnemyDestroyed()
    {
        Debug.Log("Enemy destroyed by: " + gameObject.name);
    }
}
