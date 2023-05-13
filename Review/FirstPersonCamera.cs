using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonCamera : MonoBehaviour
{

    // Variables
    public Transform objectCamera;

    bool lockedCursor = true;

    public string inputAxisName = "Steering";  // The name of the input axis to read


    void Start()
    {
        // Lock and Hide the Cursor
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

    }

    
    void Update()
    {
        // Collect steering input

        float inputX = Input.GetAxis(inputAxisName);

        // Rotate the Camera around its local X axis

        /*cameraVerticalRotation -= inputY;
        cameraVerticalRotation = Mathf.Clamp(cameraVerticalRotation, -90f, 90f);
        transform.localEulerAngles = Vector3.right * cameraVerticalRotation;*/


        // Rotate the Player Object and the Camera around its Y axis

        objectCamera.Rotate(Vector3.up * inputX);
       
    }
}