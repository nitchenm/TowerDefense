using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementZoom : MonoBehaviour
{
    // Movement based Scroll Wheel Zoom.
    public Transform parentObject;
    public float zoomLevel;
    public float sensitivity = 1;
    public float speed = 30;
    public float maxZoom = 30;
    float zoomPosition;
    public GameObject spellObject;
    void Update()
    {
        zoomLevel += Input.mouseScrollDelta.y * sensitivity;
        zoomLevel = Mathf.Clamp(zoomLevel, 0, maxZoom);
        ClipCheck();
        zoomPosition = Mathf.MoveTowards(zoomPosition, zoomLevel, speed * Time.deltaTime);
        transform.position = parentObject.position + (transform.forward * zoomPosition);
    }

    void ClipCheck()
    {
        Ray ray = new Ray(parentObject.position, transform.forward);
        RaycastHit[] hits = Physics.SphereCastAll(ray, 3, maxZoom);
        float closestDistance = maxZoom;

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.gameObject != spellObject) // Exclude the spell's collider by checking the gameObject reference
            {
                if (hit.distance < closestDistance)
                {
                    closestDistance = hit.distance;
                  //  Debug.Log("Collider hit: " + hit.collider.name); // Print collider name for debugging
                }
            }
        }

        if (closestDistance < zoomLevel + 3)
        {
            zoomLevel = closestDistance - 3;
        }
    }
}
