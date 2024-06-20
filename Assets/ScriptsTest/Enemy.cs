using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   public float speed = 10f;
   public int health = 100;
   public int EnemyCurrency = 20;

   private Transform target;
   private int wavepointIndex = 0;
   
   // enemy look for the waypoints to advance
   void Start (){
    target = Waypoints.points[0];

   }
   
   public void TakeDamage(int amount){
      health-= amount;
      if(health <=0){
         Die();
      }
   }

   void Die(){
      Destroy(gameObject);
      PlayerStats.Currency += EnemyCurrency;
      Debug.Log("Currency Gained: "+ PlayerStats.Currency);
   }

   void Update ()
   {
      int i=0;
    Vector3 dir = target.position - transform.position;
		transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);
    
		if (Vector3.Distance(transform.position, target.position) <= 0.4f)// if distance from a waypoint its near 0 it look for the next waypoint
		{
			GetNextWaypoint();
		}
          i++;
    void GetNextWaypoint(){

      if(wavepointIndex >= Waypoints.points.Length-1)
      {
         EndPath();
         return;
      }
      wavepointIndex++;
      target = Waypoints.points[wavepointIndex];
   }

   void EndPath(){
      PlayerStats.Lives--;
      Destroy(gameObject);
   }
    }
    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }
}
