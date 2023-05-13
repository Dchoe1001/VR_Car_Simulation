using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{
    private float horizontalInput;
    private float verticalInput;
    private float currentSteerAngle;
    private float currentbreakForce; // clutch pedal brake
    //private float currentbreakForce2; // brake pedal brake **doesnt work
    private float isBreaking; // clutch pedal
    //private float isBreaking2; //brake pedal

    private Rigidbody carRigidBody;

    [SerializeField] private float motorForce = 1000f; // acceleration speed
    [SerializeField] private float breakForce = 75f;
    [SerializeField] private float maxSteerAngle = 150f;
    [SerializeField] private float minSteerAngle = 0f;
    [SerializeField] public float deadZone = 10f;  // The range of values around zero to ignore
    [SerializeField] public float steeringSensitivity = 5f;
    [SerializeField] public float accelerationFactor = 100f;
    [SerializeField] public float breakDuration = 7500f;
    [SerializeField] public float accelDuration = 10f;
    [SerializeField] public float velocityCap = 10f;

    [SerializeField] public float minrotationspd = 0f;
    [SerializeField] public float maxrotationspd = 120f;

    [SerializeField] private WheelCollider frontLeftWheelCollider;
    [SerializeField] private WheelCollider frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider;
    [SerializeField] private WheelCollider rearRightWheelCollider;

    [SerializeField] private Transform frontLeftWheelTransform;
    [SerializeField] private Transform frontRightWheeTransform;
    [SerializeField] private Transform rearLeftWheelTransform;
    [SerializeField] private Transform rearRightWheelTransform;

    
    void Start ()
    {
        carRigidBody = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        GetInput();
        HandleMotor();
        if (carRigidBody.velocity.magnitude > velocityCap) {
            carRigidBody.velocity = carRigidBody.velocity.normalized * velocityCap;
         }

        HandleSteering();
        UpdateWheels();
    }


    private void GetInput()
{
    if (Input.GetAxis("Steering") < 1f)
    {
        horizontalInput = 0f;
    }
    else
    {
        horizontalInput = Input.GetAxis("Steering");
    }
    //Debug.Log(verticalInput);

    verticalInput = Input.GetAxisRaw("GasPedal");
    isBreaking = Input.GetAxisRaw("BrakePedal");
    //isBreaking2 = Input.GetAxisRaw("BrakePedal2");
    //Debug.Log(verticalInput);

}


    private void HandleMotor()
    {
        float isTerTrue = 1;
        float isTerFalse = 0;
        if (verticalInput < 0f)
        {
            frontLeftWheelCollider.motorTorque = -verticalInput * motorForce;
            frontRightWheelCollider.motorTorque = -verticalInput * motorForce;
        }
        else
        {
            frontLeftWheelCollider.motorTorque = 0;
            frontRightWheelCollider.motorTorque = 0;
        }
        currentbreakForce = isBreaking != 1f ? breakForce : 0f; // Apply breaking only if isBreaking is not equal to 1
        currentbreakForce = isBreaking != 1f ? isTerTrue : isTerFalse;
        //Debug.Log("ANS: " + currentbreakForce);
        //currentbreakForce2 = isBreaking2 != 1f ? isTerTrue : isTerFalse;
        //Debug.Log("Clutch pedal: " + currentbreakForce);
        //Debug.Log("Brake pedal: " + currentbreakForce2);
        ApplyBreaking();
    }

    private void ApplyBreaking()
    {
        if (isBreaking != 1f)
        {
            // Gradually increase the brake force up to the maximum over a certain duration
            currentbreakForce = Mathf.MoveTowards(currentbreakForce, breakForce, Time.fixedDeltaTime * (breakForce / breakDuration));

            // Gradually decrease the motor force down to zero over a certain duration
            motorForce = Mathf.MoveTowards(motorForce, 0f, Time.fixedDeltaTime * (motorForce / accelDuration));
        }
        else
        {
            // Gradually decrease the brake force down to zero over a certain duration
            currentbreakForce = Mathf.MoveTowards(currentbreakForce, 0f, Time.fixedDeltaTime * (breakForce / breakDuration));

            // Gradually increase the motor force up to the maximum over a certain duration
            motorForce = Mathf.MoveTowards(motorForce, 1000f, Time.fixedDeltaTime * (1000f / accelDuration));
        }

        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }



    private void HandleSteering()
    {
        float rawInput = Input.GetAxisRaw("Steering"); // Get the raw input value from the G920 steering wheel
        float input = Mathf.Clamp(rawInput, -1f, 1f); // Clamp the input value between -1 and 1


        /*currentSteerAngle = Mathf.Lerp(minSteerAngle, maxSteerAngle, Mathf.Abs(input)) * Mathf.Sign(input);
        //Debug.Log("currentSteerAngle" + currentSteerAngle);

        if (Mathf.Abs(currentSteerAngle) < deadZone)
        {
            input = 0f;
        }

        frontLeftWheelCollider.steerAngle = currentSteerAngle / steeringSensitivity;
        frontRightWheelCollider.steerAngle = currentSteerAngle / steeringSensitivity;*/

        if (Mathf.Abs(verticalInput) < .1f)
        {
            currentSteerAngle = 0f;
        }
        else
        {
            float angle = 0;

            // check if the input is above a certain threshold value
            if (Mathf.Abs(input) > 0.1f) {
                angle = input / steeringSensitivity;

                // gradually rotate the car toward the desired angle
                float rotationSpeed = Mathf.Clamp(GetComponent<Rigidbody>().velocity.magnitude / 2, minrotationspd, maxrotationspd);
                Quaternion targetRotation = Quaternion.Euler(0, angle, 0);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

                // update current steer angle based on input
                currentSteerAngle = Mathf.Lerp(minSteerAngle, maxSteerAngle, Mathf.Abs(input)) * Mathf.Sign(input);
            } 
            else {
                // if input is below the threshold value, set current steer angle to 0
                currentSteerAngle = 0;
            }

        }
        //Debug.Log("Steering " + currentSteerAngle);
        if (Mathf.Abs(currentSteerAngle) < deadZone)
            {
                currentSteerAngle = 0f;
                frontLeftWheelCollider.steerAngle = 0f;
                frontRightWheelCollider.steerAngle = 0f;
                //Debug.Log("*****DEADZONE*****-");
            }
            else
            {
                frontLeftWheelCollider.steerAngle = currentSteerAngle / steeringSensitivity;
                frontRightWheelCollider.steerAngle = currentSteerAngle / steeringSensitivity;
            }

           
    }



    private void UpdateWheels()
    {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheeTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }
}