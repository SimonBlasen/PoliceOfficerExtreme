using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCamFollow : MonoBehaviour
{
    [SerializeField]
    private Transform carLookat = null;
    [SerializeField]
    private Transform carPosFollow = null;
    [SerializeField]
    private float lerpSpeedLookat = 1f;
    [SerializeField]
    private float lerpSpeedMove = 1f;


    private Vector3 lerpLookat = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        lerpLookat = Vector3.Lerp(lerpLookat, carLookat.position, lerpSpeedLookat * Time.fixedDeltaTime);
        transform.LookAt(lerpLookat);

        transform.position = Vector3.Lerp(transform.position, carPosFollow.position, lerpSpeedMove * Time.fixedDeltaTime);
    }
}
