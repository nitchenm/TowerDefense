using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed;

    // Update is called once per frame
    void Update()
    {
        MoveCamera(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    void MoveCamera(float x, float y)
    {
        Vector3 movementAmount = new Vector3(x, 0, y) * speed * Time.deltaTime;
        transform.Translate(movementAmount);
    }
}
