using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public GameObject canvasPointer;

    private void Awake()
    {
        canvasPointer = GameObject.Find("UpgradeMenucanvaspointer");
    }
    void Update()
    {
        transform.LookAt(canvasPointer.transform);
    }
}
