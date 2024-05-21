
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class carcontroller : MonoBehaviour
{
    
    private float horizontalInput, verticalInput;
    private float currentSteerAngle, currentbreakForce;
    private bool isBreaking;

     public bool isBoosting; 
    public bool isDrifting; 
    // Settings
    [SerializeField] private float motorForce, breakForce, maxSteerAngle;
     [SerializeField] private float boostPower = 1000f;
    [SerializeField] private float driftTorque = 4000f;
    [SerializeField] private float driftSidewaysFriction = 0.5f;

    [SerializeField] private float boostDuration = 5f; // Duration of the boost in seconds
[SerializeField] private float boostCooldown = 10f; // Cooldown period for the boost in seconds
private bool isBoostAvailable = true; // Flag to track if the boost is available
private float boostTimer = 0f; // Timer to track the duration of the boost
private float cooldownTimer = 0f; // Timer to track the cooldown period

public Sprite boostOffSprite;
public Sprite boostOnSprite;
public Image boostIndicator;





    // Wheel Colliders
    [SerializeField] private WheelCollider frontLeftWheelCollider, frontRightWheelCollider;
    [SerializeField] private WheelCollider rearLeftWheelCollider, rearRightWheelCollider;

    // Wheels
    [SerializeField] private Transform frontLeftWheelTransform, frontRightWheelTransform;
    [SerializeField] private Transform rearLeftWheelTransform, rearRightWheelTransform;

     public int speed;


    private void Update() {
        GetInput();
        HandleMotor();
        HandleSteering();
        UpdateWheels();
        UpdateSpeed();
        Boost(); 
        Drift();
        UpdateBoostIndicator();
    }

    private void GetInput() {
        // Steering Input
        horizontalInput = Input.GetAxis("Horizontal");

        // Acceleration Input
        verticalInput = Input.GetAxis("Vertical");

        // Breaking Input
        isBreaking = Input.GetKey(KeyCode.Space);

        // Boosting Input
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            isBoosting = true;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            isBoosting = false;
        }

       
        if (Input.GetKeyDown(KeyCode.Z))
        {
            isDrifting = true;
        }
        else if (Input.GetKeyUp(KeyCode.Z))
        {
            isDrifting = false;
        }
    }

    private void HandleMotor() {
        float motor = verticalInput * motorForce;

        // Apply boost if boosting
        if (isBoosting)
        {
            motor += boostPower;
        }

        frontLeftWheelCollider.motorTorque = motor;
        frontRightWheelCollider.motorTorque = motor;
        currentbreakForce = isBreaking ? breakForce : 0f;
        ApplyBreaking();
        
    }

    private void ApplyBreaking() {
        frontRightWheelCollider.brakeTorque = currentbreakForce;
        frontLeftWheelCollider.brakeTorque = currentbreakForce;
        rearLeftWheelCollider.brakeTorque = currentbreakForce;
        rearRightWheelCollider.brakeTorque = currentbreakForce;
    }

    private void HandleSteering() {
        currentSteerAngle = maxSteerAngle * horizontalInput;
        frontLeftWheelCollider.steerAngle = currentSteerAngle;
        frontRightWheelCollider.steerAngle = currentSteerAngle;
    }

    private void UpdateWheels() {
        UpdateSingleWheel(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateSingleWheel(frontRightWheelCollider, frontRightWheelTransform);
        UpdateSingleWheel(rearRightWheelCollider, rearRightWheelTransform);
        UpdateSingleWheel(rearLeftWheelCollider, rearLeftWheelTransform);
    }

    private void UpdateSingleWheel(WheelCollider wheelCollider, Transform wheelTransform) {
        Vector3 pos;
        Quaternion rot; 
        wheelCollider.GetWorldPose(out pos, out rot);
        wheelTransform.rotation = rot;
        wheelTransform.position = pos;
    }

     private void UpdateSpeed() {
        // Calculate speed and round it to the nearest integer
        speed = Mathf.RoundToInt(GetComponent<Rigidbody>().velocity.magnitude * 3.6f); // Convert m/s to km/h
    }

    private void Boost()
{
    if (isBoosting && isBoostAvailable)
    {
        // Activate boost
        GetComponent<Rigidbody>().AddForce(-Vector3.forward * boostPower);
        boostTimer += Time.deltaTime;

        // Check if boost duration has been reached
        if (boostTimer >= boostDuration)
        {
            // Deactivate boost and start cooldown
            isBoosting = false;
            isBoostAvailable = false;
            boostTimer = 0f;
        }
    }
    else if (!isBoostAvailable)
    {
        // Start cooldown
        cooldownTimer += Time.deltaTime;

        // Check if cooldown period has been reached
        if (cooldownTimer >= boostCooldown)
        {
            // Reset cooldown
            isBoostAvailable = true;
            cooldownTimer = 0f;
        }
    }
}



 private void UpdateBoostIndicator()
{
    if (isBoostAvailable)
    {
        // Booster available, set boostOn image
        boostIndicator.sprite = boostOnSprite;
    }
    else
    {
        // Booster on cooldown, set boostOff image
        boostIndicator.sprite = boostOffSprite;
    }
}



   private void Drift()
    {
        WheelFrictionCurve sidewaysFriction = rearLeftWheelCollider.sidewaysFriction;

        if (isDrifting)
        {
            sidewaysFriction.stiffness = driftSidewaysFriction;
        }
        else
        {
            // Reset sideways friction to default value
            sidewaysFriction.stiffness = 1f;
        }

        // Apply the updated sideways friction to both rear wheel colliders
        rearLeftWheelCollider.sidewaysFriction = sidewaysFriction;
        rearRightWheelCollider.sidewaysFriction = sidewaysFriction;
    }

public void SetControlsEnabled(bool enabled)
    {
        this.enabled = enabled;
    }

}