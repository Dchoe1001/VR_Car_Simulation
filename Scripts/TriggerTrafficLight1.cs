using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerTrafficLight1 : MonoBehaviour
{
    private TrafficLightLogic trafficLight; // Reference to the TrafficLight script
    public GameObject trafficLightObject;  // User-assigned reference to the TrafficLight game object

    bool isObjectTrigger = false;

    public float floatValue = 1.0f;

    void Start()
    {
        // Debug.Log("test scripted launched");
        // Get the TrafficLight component from the user-assigned game object
        trafficLight = trafficLightObject.GetComponent<TrafficLightLogic>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Car")
        {
            isObjectTrigger = true;
            StartCoroutine(DetectObjects());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Car"))
        {
            isObjectTrigger = false;
        }
    }

     private IEnumerator DetectObjects()
    {
        while (isObjectTrigger)
        {
            Collider[] colliders = Physics.OverlapBox(transform.position, transform.localScale / 2f);
            foreach (Collider collider in colliders)
            {
                if (collider.CompareTag("Car"))
                {
                    //Debug.Log("Car detected");
                    if (trafficLight.currentState == TrafficLightLogic.LightState.Red) {
                        //Debug.Log("Red");
                        floatValue = 2.0f;
                    } else if (trafficLight.currentState == TrafficLightLogic.LightState.Yellow) {
                        floatValue = 3.0f;
                    } else {
                        floatValue = 4.0f;
                    }
                }
            }
            yield return null;
        }
    }

    void Update()
    {
    }

}