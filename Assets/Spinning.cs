using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinning : MonoBehaviour
{
    public float speed = 50f; // degrees per second

    void Update()
    {
        // Rotate around Y-axis
        transform.Rotate(0, speed * Time.deltaTime, 0);
    }
}

