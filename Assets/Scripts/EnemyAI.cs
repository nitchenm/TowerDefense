using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class EnemyAI :  LivingEntity
{

    private NavMeshAgent enemyNavMesh;
    private WaypointController waypointController;
    private List<Transform> waypoints;
    private int index = 0;
    private bool moving = true;
    public float waitInWaypoint;
    public PlayerManager playerManager;

    public float enemyDamage;

    public float speed => enemyNavMesh.speed;

    public UnityEvent onDestroyed;

    //  public EnemyManager enemyManager;

    public bool isFrozen = false;

    private void Awake()
    {
        enemyNavMesh = GetComponent<NavMeshAgent>();
        waypointController = FindObjectOfType<WaypointController>();
        waypoints = waypointController.wayPoints;
        playerManager = FindObjectOfType<PlayerManager>();
       // EnemyManager.instance.RegisterEnemy(this);
        //enemyManager = EnemyManager.instance;
        //enemyManager.RegisterEnemy(this);
        //onDeath.AddListener(OnDeath);
        
       // EnemyManager.instance.RegisterEnemy(this);
       // onDeath.AddListener(OnDeath);
        
    }

    private void OnEnable()
    {
        if (waypoints != null && waypoints.Count > 0)
        {
            //enemyManager = EnemyManager.instance;
            //enemyManager.RegisterEnemy(this);
            enemyNavMesh.SetDestination(waypoints[index].position);     //Set the initial destination to the first waypoint
        }
    }

    private void Update()
    {
        if (waypoints == null || waypoints.Count == 0)
            return;

        if (Vector3.Distance(transform.position, waypoints[index].position) <= 1f && moving) //Checks distance from a waypoint and if its moving to set the move 
        {                                                                                    //to the next Waypoint   
            moving = false;
            StartCoroutine(MoveToNextWaypoint());
        }

    }

    private IEnumerator MoveToNextWaypoint()
    {
        index++; //Move to the next waypoint

        if (index < waypoints.Count) //checks if theres still waypoints in the index
        {
            enemyNavMesh.SetDestination(waypoints[index].position);
        }
        else //if there is no more waypoints, return the enemy and do damage mechanic
        {
            ObjectPooler.ReturnToPool(gameObject);
            playerManager.TakeDamage(enemyDamage);
            EnemyManager.instance.UnregisterEnemy(this);
            EndPath();
            yield break;
        }
        yield return new WaitForSeconds(waitInWaypoint); 
        moving = true;
    }
    public void SetSpeed(float speed)
    {
        enemyNavMesh.speed = speed;
    }

    void EndPath(){
      PlayerStats.Lives--;

   }
}
