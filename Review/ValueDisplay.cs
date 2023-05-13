using UnityEngine;

public class VelocityDisplay : MonoBehaviour
{
    public GameObject carObject; // Public variable for the car object
    Rigidbody rb;
    WheelCollider wheelCollider;

    void Start()
    {
        // Get the Rigidbody component of the car object
        rb = GetComponent<Rigidbody>();
        // Get the WheelCollider component of the car object specified by the user
        wheelCollider = carObject.GetComponent<WheelCollider>();
    }

    void Update()
    {
        // Output the velocity to the console
        Debug.Log("Velocity: " + rb.velocity.magnitude);
        // Output the RPM of the wheel collider to the console
        Debug.Log("RPM: " + wheelCollider.rpm);
    }
}
