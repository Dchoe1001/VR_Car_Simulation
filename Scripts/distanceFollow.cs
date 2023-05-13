using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class distanceFollow : MonoBehaviour {
    public Transform[] points;      // Array of target points to move towards
    //public Transform[] targetPointA; // choose either left(A) or right(B) at a light
    //public Transform[] targetPointB;

    private int currentPointSet = 0; // Current target point set index

    //public Transform targetPointC; // choose either left(C) or right(D) at the opposing light
    //public Transform targetPointD;

    [SerializeField] public float speed = 10f;        // Speed at which to move
    [SerializeField] public float animationYellowLightSpeed = .5f;        // Speed at which to move
    [SerializeField] public float animationGreenLightSpeed = 1f;        // Speed at which to move
    [SerializeField] public float maxDistance = 10f; // Maximum distance from target object before stopping
    public GameObject targetObject; // Object to follow
    //public GameObject trafficLightObject;  // User-assigned reference to the TrafficLight game object

    private int currentPoint = 0;   // Current target point index
    private bool isMoving = true;   // Flag indicating whether the object is currently moving
    //private bool shouldChooseRandom = false; // Flag indicating whether the object should choose a random target point set

    bool isObjectTrigger = false;

    float trafficLightValue = 0f;
    private Animator animator;


    private void Start() {
        // Get the TrafficLight component from the user-assigned game object
        //trafficLight = trafficLightObject.GetComponent<TrafficLight>();
        animator = GetComponent<Animator>();
    }

     private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PressurePlate")
        {
            isObjectTrigger = true;
            //float randomValue = other.gameObject.GetComponent<random>().floatValue;
            //Debug.Log(randomValue);
            //shouldChooseRandom = true;
            //test = 1;
            //Debug.Log("passed 1st");
            StartCoroutine(DetectObjects());
        }
        else if(other.tag == "PressurePlate2")
        {
            isObjectTrigger = true;
            //float randomValue = other.gameObject.GetComponent<TriggerTrafficLight1>().floatValue;
            //Debug.Log(randomValue);
            //currentPoint = Random.Range(0, 2) == 0 ? 2 : 3; // Choose either target point A or B
            //Debug.Log("passed 1st");
            StartCoroutine(DetectObjects());
        }
    }

    private void OnTriggerExit(Collider other) {
        if (other.CompareTag("PressurePlate") || other.CompareTag("PressurePlate2")) {
            trafficLightValue = 0f;
            //Debug.Log("passed 2nd");
        }
    }

    private IEnumerator DetectObjects()
    {
        while (isObjectTrigger)
        {
            Collider[] colliders = Physics.OverlapBox(transform.position, transform.localScale / 2f);
            foreach (Collider collider in colliders)
            {
                Debug.Log(collider.tag);
                //if (collider != null && collider.CompareTag("Untagged"))
                if (collider.CompareTag("PressurePlate"))
                {
                    trafficLightValue = collider.gameObject.GetComponent<TriggerTrafficLight>().floatValue;
                    Debug.Log(trafficLightValue);
                }
                else if (collider.CompareTag("PressurePlate2"))
                {
                    trafficLightValue = collider.gameObject.GetComponent<TriggerTrafficLight1>().floatValue;
                    Debug.Log(trafficLightValue);
                }
                else 
                {
                    Debug.Log("FAILEDDDD");
                }
            }
            yield return null;
        }
    }


    private bool isAnimationEnabled = true;
    

    private void Update() 
    {
        // Check the distance to the target object
        float distance = Vector3.Distance(transform.position, targetObject.transform.position);
        animator.SetFloat("animationSpeed", speed);

        // Check the current state of the traffic light
        if (trafficLightValue == 2.0f) { // Red light value
            isMoving = false;
            isAnimationEnabled = false; // disable the animation
            Debug.Log("REDDDDDDDDD");
        } else { // Green or yellow light value
            isMoving = true;
            isAnimationEnabled = true; // enable the animation
            // Yellow light value is 3.0f
            //Debug.Log("YELLLOOOWWWWW");
            animator.speed = trafficLightValue == 3.0f ? animationYellowLightSpeed : animationGreenLightSpeed; // set the animation speed based on the traffic light value
        }

        // If the distance is greater than the maximum, stop moving and animation
        if (distance > maxDistance) {
            isMoving = false;
            isAnimationEnabled = false; // disable the animation
        }

        // Enable or disable the animation based on the isAnimationEnabled bool
        animator.enabled = isAnimationEnabled;
        //animator.SetBool("isAnimationEnabled", isAnimationEnabled); // set the isAnimationEnabled parameter in the animation controller
    }


/*    private void Update()
    {
        // Check the distance to the target object
        float distance = Vector3.Distance(transform.position, targetObject.transform.position);

        // Check the current state of the traffic light
        if (trafficLightValue == 2.0f) // Red light value
        {
            isMoving = false;
            return;
        }
        else if (trafficLightValue == 3.0f) // Yellow light value
        {
            speed = 2.5f;
        }
        else // green light value
        {
            speed = 10f;
        }

        // If the distance is greater than the maximum, stop moving
        if (distance > maxDistance)
        {
            isMoving = false;
            return;
        }
        else
        {
            // Otherwise, resume moving
            isMoving = true;
        }

        // If we're still moving, move towards the current target point
        if (isMoving)
        {
            // Move towards the current target point
            transform.position = Vector3.MoveTowards(transform.position, points[currentPoint].position, speed * Time.deltaTime);

            // Check the distance to the current target point
            float distanceToPoint = Vector3.Distance(transform.position, points[currentPoint].position);

            // If we've reached the current target point, move on to the next one
            if (distanceToPoint < 0.1f)
            {
                // Increment the current point index
                currentPoint++;

                // If we've reached the end of the points array, stop moving
                if (currentPoint >= points.Length)
                {
                    isMoving = false;
                    return;
                }
            }

            // Play the "isMovingForward" animation using a trigger
            GetComponent<Animator>().SetTrigger("isWideTurningLeft");
        }
    }*/


}

