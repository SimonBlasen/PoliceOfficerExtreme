using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField]
    private WheelCollider[] wheelsSteer = null;
    [SerializeField]
    private WheelCollider[] wheelsThrottle = null;
    [SerializeField]
    private float brakeTorque = 12f;
    [SerializeField]
    private float throttleTorque = 12f;
    [SerializeField]
    private float rpmThreshNotBrake = 12f;
    [SerializeField]
    private float velocityThresh = 12f;
    [SerializeField]
    private float steerSpeed = 12f;
    [SerializeField]
    private float steerAngle = 12f;
    [SerializeField]
    private AudioSource audioSourceHitDude;
    [SerializeField]
    private Transform firstPersonSpawnPos;

    private Rigidbody rig;

    private float curSteerAngle = 0f;

    // Start is called before the first frame update
    void Start()
    {
        rig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsActive)
        {
            Vector2 inputs = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));


            curSteerAngle = inputs.x * steerAngle;


            //Debug.Log(wheelsThrottle[0].rpm.ToString("n2"));

            bool drivesForward = Vector3.Angle(rig.velocity, transform.forward) <= 90f;

            for (int i = 0; i < wheelsThrottle.Length; i++)
            {
                // Give power
                if ((drivesForward == (inputs.y > 0f)) || rig.velocity.magnitude <= velocityThresh)
                {
                    wheelsThrottle[i].motorTorque = throttleTorque * inputs.y;
                    wheelsThrottle[i].brakeTorque = 0f;
                }

                // Brake
                else
                {
                    wheelsThrottle[i].motorTorque = 0f;
                    wheelsThrottle[i].brakeTorque = brakeTorque * Mathf.Abs(inputs.y);
                }
            }


            for (int i = 0; i < wheelsSteer.Length; i++)
            {
                wheelsSteer[i].steerAngle = Mathf.MoveTowards(wheelsSteer[i].steerAngle, curSteerAngle, steerSpeed * Time.deltaTime);


                // Give power
                if ((drivesForward == (inputs.y > 0f)) || rig.velocity.magnitude <= velocityThresh)
                {
                    wheelsSteer[i].motorTorque = 0f;
                    wheelsSteer[i].brakeTorque = 0f;
                }

                // Brake
                else
                {
                    wheelsSteer[i].motorTorque = 0f;
                    wheelsSteer[i].brakeTorque = brakeTorque * Mathf.Abs(inputs.y);
                }
            }
        }
        else
        {
            for (int i = 0; i < wheelsThrottle.Length; i++)
            {
                wheelsThrottle[i].motorTorque = 0f;
                wheelsThrottle[i].brakeTorque = brakeTorque;
            }
            for (int i = 0; i < wheelsSteer.Length; i++)
            {
                wheelsSteer[i].motorTorque = 0f;
                wheelsSteer[i].brakeTorque = brakeTorque;
            }
        }
    }

    public Vector3 Velocity
    {
        get
        {
            return rig.velocity;
        }
    }

    public void HitRobber()
    {
        audioSourceHitDude.Play();
    }

    public bool IsActive
    {
        get; set;
    } = true;


    public Transform FirstPersonSpawnPos
    {
        get
        {
            return firstPersonSpawnPos;
        }
    }
}
