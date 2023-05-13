using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : MonoBehaviour
{
    public Transform[] points;  // An array of points to move towards
    public float speed = 5f;    // The speed at which the object moves

    private Transform target;   // The current target point

    void Start()
    {
        // Choose a random point from the array as the initial target
        target = points[Random.Range(0, points.Length)];
    }

    void Update()
    {
        // Move the object towards the target point
        transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        // Make the object look at the target point
        transform.LookAt(target);

        // If the object has reached the target point, choose a new target randomly
        if (transform.position == target.position)
        {
            target = points[Random.Range(0, points.Length)];
        }
    }
}

