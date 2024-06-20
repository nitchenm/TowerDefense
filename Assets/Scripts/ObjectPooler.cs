using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int poolSize = 10;
    private List<GameObject> pool;
    private GameObject poolContainer;
    private void Awake()
    {
        pool = new List<GameObject>();
        poolContainer = new GameObject($"Pool - {enemyPrefab.name}");  //Creates a container object for the pooled objects
        CreatePooler();                                                
    }

    private void CreatePooler()
    {
        for (int i = 0; i < poolSize; i++)
        {
            pool.Add(CreateInstance());                 //Instantiate and add objects to the pool
        }
    }

    private GameObject CreateInstance()
    {
        GameObject newInstance = Instantiate(enemyPrefab);              // New object from the prefab
        newInstance.transform.SetParent(poolContainer.transform);       // Set poolcontainer as parent
        newInstance.transform.SetPositionAndRotation(transform.position, transform.rotation);
        newInstance.SetActive(false);                                   // set the prefabs as inactive
        return (newInstance);
    }


    
    public GameObject GetInstanceFromPool()  //This method searches for an inactive object in the pool and returns if it is available, if there is no active it creates a new one
    {
        for (int i = 0; i< pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                pool[i].transform.position = this.transform.position;
                return pool[i];
            }
        }
        return CreateInstance();
    }

    public static void ReturnToPool(GameObject instance)   //Return the gameobject to the pool and deactivates the instance
    { 
        
        instance.SetActive(false);

    }

    public static IEnumerator ReturnToPoolWithDelay(GameObject instance, float delay)
    {
        yield return new WaitForSeconds(delay);
        instance.SetActive(false);
    }

   
}
