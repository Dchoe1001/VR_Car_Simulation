using UnityEngine;
using System.Collections.Generic;

public class test : MonoBehaviour
{
    public Animator animator; // reference to the animator component
    [SerializeField] public float delayTime = 1.0f; // delay time in seconds
    private bool isAnimationEnabled = true; // assume animation should play by default

    public float rayLength = 10.0f;
    public Vector3[] rayDirections = { Vector3.forward, Vector3.right, Vector3.left };
    public Color rayColor = Color.yellow;

    public LayerMask layerMask;

    public float forwardOffset = 0.0f;
    public float upwardOffset = 0.5f;
    [SerializeField] public float speed = 1f;        // Speed at which to move
    public float distanceFromObjSlow = 10f;

    public float distanceFromObjStop = 5f;
    
    void Start()
    {
        Invoke(nameof(FixedUpdate), delayTime);
    }
    
    void FixedUpdate()
    {
        bool objectInPath = false; // assume object is not in path by default

        foreach (Vector3 direction in rayDirections)
        {
            Vector3 rayOrigin = transform.position + Vector3.up * upwardOffset + transform.forward * forwardOffset;
            if (Physics.Raycast(rayOrigin, transform.TransformDirection(direction), out RaycastHit hit, rayLength, layerMask))
            {
                Debug.DrawRay(rayOrigin, transform.TransformDirection(direction) * hit.distance, rayColor);
                Debug.Log("Hit " + hit.collider.gameObject.name + " at distance " + hit.distance.ToString("F2"));
                if (hit.distance < distanceFromObjSlow && hit.distance > distanceFromObjStop)
                {
                    animator.SetFloat("animationSpeed", speed);
                }
                else if (hit.distance <= distanceFromObjStop)
                {
                    isAnimationEnabled = false;
                    animator.SetBool("isPlayAnimationBool", false);
                    
                }

                objectInPath = true; // object is in path
            }
            else
            {
                Debug.DrawRay(rayOrigin, transform.TransformDirection(direction) * rayLength, Color.white);
            }
        }

        // enable or disable the animator component based on isAnimationEnabled
        animator.enabled = isAnimationEnabled;

        // set isAnimationEnabled to true if object is not in path
        if (!objectInPath)
        {
            isAnimationEnabled = true;
            animator.SetBool("isPlayAnimationBool", true);
        }
    }

}
