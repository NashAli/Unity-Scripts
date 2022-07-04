using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    [SerializeField] WheelCollider frontRight;
    [SerializeField] WheelCollider frontLeft;
    [SerializeField] WheelCollider rearRight;
    [SerializeField] WheelCollider rearLeft;

    [SerializeField] Transform frontRightTransform;
    [SerializeField] Transform frontLeftTransform;
    [SerializeField] Transform rearRightTransform;
    [SerializeField] Transform rearLeftTransform;
    public float acceleration = 500f;
    public float breakingforce = 300f;
    public float maxTurnAngle = 30f;
    private float currentAcceleration = 0f;
    private float currentBreakforce = 0f;
    private float currentTurnAngle = 0f;

    // Update is called once per frame
    void Update()
    {
        currentAcceleration = acceleration * Input.GetAxis("Vertical");
        //get acceleration values from keys.
        if(Input.GetKey(KeyCode.Space))
            currentBreakforce = breakingforce;
            else
            currentBreakforce=0f;
            //Apply acceleration for front wheel drive.
            frontRight.motorTorque  = currentAcceleration;
            frontLeft.motorTorque  = currentAcceleration;

            frontRight.brakeTorque = currentBreakforce;
            frontLeft.brakeTorque = currentBreakforce;
            rearRight.brakeTorque = currentBreakforce;
            rearLeft.brakeTorque = currentBreakforce;
            //Take care of steering
            currentTurnAngle = maxTurnAngle * Input.GetAxis("Horizontal");
            frontLeft.steerAngle = currentTurnAngle;
            frontRight.steerAngle = currentTurnAngle;

            //Take care of wheel meshes
            UpdateWheel(frontLeft, frontLeftTransform);
            UpdateWheel(frontRight, frontRightTransform);
            UpdateWheel(rearLeft, rearLeftTransform);
            UpdateWheel(rearRight, rearRightTransform);
    }

    void UpdateWheel(WheelCollider col, Transform trans)
    {
        //Get wheel collider state
        Vector3 position;
        Quaternion rotation;
        col.GetWorldPose(out position, out rotation);
        //Set wheel transform state.
        trans.position = position;
        trans.rotation = rotation;

    }


}
