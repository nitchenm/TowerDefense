using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public Transform enemyPrefab;
    public Transform spawnPoint;

    public float timeBetweenWaves = 5f;
    private float countdown = 2f;

    private int waveNumber =0;

    void Update(){

          if(countdown<=0f){
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
          }
       countdown -= Time.deltaTime;
    }

    // basic spawn of enemies(for every iteration it spawn and additional enemy to the previous wave)
    IEnumerator SpawnWave(){

        waveNumber++;

        for(int i=0 ; i<waveNumber;i++){
         SpawnEnemy();
         yield return new WaitForSeconds(0.5f);
        }          
        
    }
    //for every iteration, creates a enemy at the spawn point
    void SpawnEnemy(){
        Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation);

    }
}
