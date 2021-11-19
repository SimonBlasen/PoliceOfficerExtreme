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
       

        // Das .magnitude kann man auf jedem Vector3 aufrufen, und gibt einfach die Länge des Vektors aus. In dem Fall ist das smoother, als Abs(x)+Abs(z) zu nehmen, was aber auch in etwa funktionieren würde :D

        //pitch = minPitch + (pitchMult * (Mathf.Abs(carRig.velocity.x) + Mathf.Abs(carRig.velocity.z)));
        pitch = minPitch + (pitchMult * carRig.velocity.magnitude);
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);


        // Hier hast du in der if motorAudio.pitch geprüft, aber du müsstest pitch prüfen, ob es größer oder kleiner als min-max pitch ist

        /*if (motorAudio.pitch <= minPitch)
        {
            pitch = minPitch;
        }
        if(motorAudio.pitch >= maxPitch)
        {
            pitch = maxPitch;
        }*/

        //Debug.Log(pitch);
        //Debug.Log(Mathf.Abs(carRig.velocity.x));
        motorAudio.pitch = pitch;
    }
}
