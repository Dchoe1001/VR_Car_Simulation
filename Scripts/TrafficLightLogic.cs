
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrafficLightLogic : MonoBehaviour
{
    [SerializeField] public float redDuration = 5.0f; // Duration of red light in seconds
    [SerializeField] public float yellowDuration = 3.0f; // Duration of yellow light in seconds
    [SerializeField] public float greenDuration = 2.0f; // Duration of green light in seconds
    public Light redLight;      // Reference to the red light object
    public Light yellowLight;   // Reference to the yellow light object
    public Light greenLight;    // Reference to the green light object

    public enum LightState { Red, Yellow, Green };
    public LightState currentState; // Make the currentState variable public

    public TrafficLight otherTrafficLight; // Reference to the other traffic light script
    private float timer = 0.0f;  // Timer to keep track of how long each light has been on

    string otherCurrentLightState;

    void Start()
    {
        currentState = LightState.Red;  // Start with the Red light on
        redLight.enabled = true;
        yellowLight.enabled = false;
        greenLight.enabled = false;
    }

    void Update()
    {
        // Update the timer
        timer += Time.deltaTime;

        //Debug.Log(otherTrafficLight.currentState);
        otherCurrentLightState = otherTrafficLight.currentState.ToString();

        // Check if it's time to switch to the next light
        if (otherCurrentLightState == "Red" && currentState != LightState.Yellow)
        {
            // Turn on the green light and turn off the others
            currentState = LightState.Green;
            redLight.enabled = false;
            greenLight.enabled = true;
            //Debug.Log(timer);
            if (timer > greenDuration)
            {
                // Switch to Yellow light
                currentState = LightState.Yellow;
                greenLight.enabled = false;
                yellowLight.enabled = true;
                
                timer = 0.0f;
            }
            
        }
        else if (otherCurrentLightState == "Yellow")
        {
            // Turn on the yellow light and turn off the others
            currentState = LightState.Red;
            yellowLight.enabled = false;
            redLight.enabled = true;
            timer = 0.0f;
        }
        else if (otherCurrentLightState == "Green")
        {
            // Turn on the green light and turn off the others
            currentState = LightState.Red;
            redLight.enabled = true;
            yellowLight.enabled = false;
            greenLight.enabled = false;
            timer = 0.0f;
        }
        

        /*
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
        }*/
    }
}
