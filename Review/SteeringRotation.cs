using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringRotation : MonoBehaviour
{
    [SerializeField] public float steeringRotationSpeed = 50f;  // The speed at which the object should rotate
    public string inputAxisName = "Steering";  // The name of the input axis to read

    [SerializeField] public float deadZoneSteeringRotation = 1f;  // The range of values around zero to ignore

    private float currentRotation = 0f;  // The current rotation of the object
    private Quaternion initialRotation;  // The initial rotation of the object

    void Start()
    {
       //transform.localEulerAngles = new Vector3(0f, 0f, 0f);
       initialRotation = transform.rotation;
    }

    void Update()
    {
        float inputAxisValue = Input.GetAxis(inputAxisName);  // Read the input value
      
        if (Mathf.Abs(inputAxisValue) < deadZoneSteeringRotation) {
            inputAxisValue = 0f;  // Apply dead zone to the input
            //currentRotation = initialRotation.z;  // Reset the current rotation
        }

        // Calculate the new rotation based on the input value
        float newRotation = currentRotation - inputAxisValue * steeringRotationSpeed * Time.deltaTime;
        //make the rotation relative to the initial rotation
        float relativeRotation = currentRotation - initialRotation.eulerAngles.z;

        // Apply the new rotation to the object
        Vector3 eulerRotation = transform.rotation.eulerAngles;
        eulerRotation.z = relativeRotation;
        transform.rotation = Quaternion.Euler(eulerRotation);
       // transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, -inputAxisValue * 30f));
        //Debug.Log(transform.rotation);
        currentRotation = newRotation;  // Update the current rotation
    }
}
