using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spell : MonoBehaviour
{
    public float speed;
    public int damage;

    private Vector3 targetPosition;
    private bool isMoving = false;
   
    void Update()
    {
        if (isMoving)
        {
            MoveTowardsTarget();
        }
    }

    private void MoveTowardsTarget()
    {
        //Move the spell wowards the target
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        if (transform.position == targetPosition)
        {
            isMoving = false;
            ActivateCollisionDetection();
            Destroy(gameObject, (float).5);
        }
    }
    private void ActivateCollisionDetection()
    {
        Collider collider = GetComponent<Collider>();
        if(collider != null)
        {
            collider.enabled = true;
        }
    }
    public void Launch(Vector3 target)
    {
        targetPosition = target;
        isMoving = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            EnemyAI enemy = other.GetComponent<EnemyAI>();
            enemy.TakeDamage(damage);
            Debug.Log("Lo da�e");
        }
    }
}
