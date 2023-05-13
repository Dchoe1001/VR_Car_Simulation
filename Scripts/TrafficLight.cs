using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLight : MonoBehaviour
{
    [SerializeField] public float redDuration = 5.0f;     // Duration of red light in seconds
    [SerializeField] public float yellowDuration = 3.0f;  // Duration of yellow light in seconds
    [SerializeField] public float greenDuration = 2.0f;   // Duration of green light in seconds

    public Light redLight;      // Reference to the red light object
    public Light yellowLight;   // Reference to the yellow light object
    public Light greenLight;    // Reference to the green light object

    private float timer = 0.0f;  // Timer to keep track of how long each light has been on

    public enum LightState { Red, Yellow, Green };
    public LightState currentState; // Make the currentState variable public

    //public GameObject logicTestLight;

    //public string test;

    /*private enum LightState
    {
        Red,
        Yellow,
        Green
    }*/

    //private LightState currentState;   // Current state of the traffic light

    void Start()
    {
        currentState = LightState.Green;  // Start with the Green light on
        redLight.enabled = false;
        yellowLight.enabled = false;
        greenLight.enabled = true;

        //test = logicTestLight.GetComponent<TrafficLightLogic>().logicTest;
        //Debug.Log("start " + test);
    }

    void Update()
    {
        // Update the timer
        timer += Time.deltaTime;

        // Check if it's time to switch to the next light
        if (currentState == LightState.Red && timer >= redDuration)
        {
            // Switch to Green light
            currentState = LightState.Green;
            redLight.enabled = false;
            greenLight.enabled = true;
            timer = 0.0f;
        }
        else if (currentState == LightState.Green && timer >= greenDuration)
        {
            // Switch to Yellow light
            currentState = LightState.Yellow;
            greenLight.enabled = false;
            yellowLight.enabled = true;
            
            timer = 0.0f;
        }
        else if (currentState == LightState.Yellow && timer >= yellowDuration)
        {
            // Switch to red light
            currentState = LightState.Red;
            yellowLight.enabled = false;
            redLight.enabled = true;
            timer = 0.0f;
        }
    }

    


}
