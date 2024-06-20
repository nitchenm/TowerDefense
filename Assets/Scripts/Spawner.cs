using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
public class Spawner : MonoBehaviour
{
    public int enemyCount = 10;
    public GameObject spawner;

    public float delayBtwSpawns;

    private float spawnTimer;
    private int enemiesSpawned;
    public float timeBetweenWaves = 5f;
    private float countdown = 2f;

    private int waveNumber =0;
    public float speed => enemyNavMesh.speed;

    private ObjectPooler pooler;

    public EnemyManager enemyManager;
    private NavMeshAgent enemyNavMesh;

    

    private void Start()
    {
        pooler = GetComponent<ObjectPooler>();
        enemyManager = EnemyManager.instance;
    }

    private void Update()
    {
       if(countdown<=0f && enemiesSpawned<enemyCount){
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
          }
       countdown -= Time.deltaTime;

    }
    /*
     private void Update()
    {
        spawnTimer -= Time.deltaTime;
        if (spawnTimer < 0)
        {
            spawnTimer = delayBtwSpawns;
            if(enemiesSpawned < enemyCount)
            {
                enemiesSpawned++;
                SpawnEnemy();
            }
        }

    }
    */

    IEnumerator SpawnWave(){

        waveNumber++;

        for(int i=0 ; i<waveNumber;i++){
         SpawnEnemy();
         enemiesSpawned++;
         if(i == waveNumber/2){
            
         }
         yield return new WaitForSeconds(1f);
        }          
        
    }

    private void SpawnEnemy()
    {
        GameObject newInstance = pooler.GetInstanceFromPool();
        newInstance.SetActive(true);
        EnemyAI enemyAI = newInstance.GetComponent<EnemyAI>();
        enemyManager.RegisterEnemy(enemyAI);
        
    }

}
