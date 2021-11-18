using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MotorSoundManager : MonoBehaviour
{

    private float pitch;
    public AudioSource motorAudio;
    public Rigidbody carRig;
    public float maxPitch, minPitch;
    public float pitchMult;

    // Start is called before the first frame update
    void Start()
    {
        motorAudio.pitch = minPitch;
    }

    // Update is called once per frame
    void Update()
    {
       

        pitch = minPitch + (pitchMult * (Mathf.Abs(carRig.velocity.x) + Mathf.Abs(carRig.velocity.z)));

        if (motorAudio.pitch <= minPitch)
        {
            pitch = minPitch;
        }
        if(motorAudio.pitch >= maxPitch)
        {
            pitch = maxPitch;
        }

        Debug.Log(pitch);
        Debug.Log(Mathf.Abs(carRig.velocity.x));
        motorAudio.pitch = pitch;
    }
}
