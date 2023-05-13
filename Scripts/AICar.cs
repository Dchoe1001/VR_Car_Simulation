using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICar : MonoBehaviour
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

    bool isObjectTrigger = false;

    float trafficLightValue = 0f;

    [SerializeField] public float animationYellowLightSpeed = .5f;        // Speed at which to move
    [SerializeField] public float animationGreenLightSpeed = 1f;        // Speed at which to move
    
    void Start()
    {
        Invoke(nameof(FixedUpdate), delayTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        
        
        if(other.tag == "PressurePlate")
        {
            isObjectTrigger = true;

            StartCoroutine(DetectObjects1());
        }
        else if(other.tag == "PressurePlate2")
        {
            isObjectTrigger = true;

            StartCoroutine(DetectObjects1());
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("PressurePlate") || other.CompareTag("PressurePlate2")) {
            trafficLightValue = 0f;
        }
    }

    private IEnumerator DetectObjects1()
    {
        while (isObjectTrigger)
        {
            Collider[] colliders = Physics.OverlapBox(transform.position, transform.localScale / 2f);
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("PressurePlate"))
                {
                    trafficLightValue = collider.gameObject.GetComponent<TriggerTrafficLight>().floatValue;
                    Debug.Log(trafficLightValue);
                }
                else if (collider.CompareTag("PressurePlate2"))
                {
                    trafficLightValue = collider.gameObject.GetComponent<TriggerTrafficLight1>().floatValue;
                    //Debug.Log(trafficLightValue);
                }
            }
            yield return null;
        }
    }
    
   void FixedUpdate()
    {
        bool objectInPath = false; // assume object is not in path by default
        bool stopObjectDetected = false; // assume stop object is not detected by default

        if (trafficLightValue == 2.0f) { // Red light value
        isAnimationEnabled = false;
        Debug.Log("red");
        animator.SetBool("isPlayAnimationBool", false); // disable the animation
    } else { // Green or yellow light value
        // Yellow light value is 3.0f
        if (!objectInPath) {
            animator.speed = trafficLightValue == 3.0f ? animationYellowLightSpeed : animationGreenLightSpeed; // set the animation speed based on the traffic light value
        } else {
            animator.speed = 0; // stop the animation if there is an object in the path
        }
    }

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
                    stopObjectDetected = true;
                    isAnimationEnabled = false;
                    animator.SetBool("isPlayAnimationBool", false);
                }

                objectInPath = true; // object is in path
            }
            else
            {
                //Debug.DrawRay(rayOrigin, transform.TransformDirection(direction) * rayLength, Color.white);
            }
        }

        

        // enable or disable the animator component based on isAnimationEnabled and whether a stop object is detected or not
        animator.enabled = isAnimationEnabled && !stopObjectDetected;

        // set isAnimationEnabled to true if object is not in path and stop object is not detected
        /*if (!objectInPath && stopObjectDetected)
        {
           // Debug.Log("asfihasfioasfsaf");
            isAnimationEnabled = false;
            animator.SetBool("isPlayAnimationBool", false);
        }
        else
        {
            isAnimationEnabled = true;
            animator.SetBool("isPlayAnimationBool", true);
        }*/
        if (objectInPath && !stopObjectDetected)
        {
            isAnimationEnabled = true;
            animator.SetBool("isPlayAnimationBool", true);
        }
    }




}
