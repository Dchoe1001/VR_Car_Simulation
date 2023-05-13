using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; //extension for textmeshpro

public class TextDisplay : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI velocityText;
    [SerializeField] TextMeshProUGUI RPMText;

    Rigidbody rb;
    WheelCollider wheelCollider;
    public GameObject wheelColliderRPM; // Public variable for the car object

    [SerializeField] float wheelRadius = 0.5f; // Set the radius of the wheel in meters

    void Start()
    {
        // Get the Rigidbody and WheelCollider components of the car object
        rb = GetComponent<Rigidbody>();
        wheelCollider = wheelColliderRPM.GetComponent<WheelCollider>();
    }

    void Update()
    {
        // Display the velocity of the car in the velocityText object
        velocityText.text = "Velocity: " + rb.velocity.magnitude.ToString("0.00");

        // Display the RPM of the wheel collider in the RPMText object
        // RPM = (Vehicle Speed * 60) / (2 * pi * Wheel Radius)
        float rpm = (rb.velocity.magnitude * 60f) / (2f * Mathf.PI * wheelRadius);

        //RPMText.text = "RPM: " + wheelCollider.rpm.ToString("0.00");
        RPMText.text = "RPM: " + rpm.ToString("0.00");

    }
}

