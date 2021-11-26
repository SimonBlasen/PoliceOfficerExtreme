using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfViewManip : MonoBehaviour
{
    [SerializeField]
    private Camera cam;
    [SerializeField]
    private float camShakeMultiplicator;

    private bool isZoomingIn;

    // Start is called before the first frame update
    void Start()
    {
        cam.fieldOfView = 60f;
        isZoomingIn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(isZoomingIn)
        {
            cam.fieldOfView -= camShakeMultiplicator * Time.deltaTime;
            if(cam.fieldOfView < 50)
            {
                isZoomingIn = false;
            }
        }
        if (!isZoomingIn)
        {
            cam.fieldOfView += camShakeMultiplicator * Time.deltaTime;
            if (cam.fieldOfView >= 60)
            {
                isZoomingIn = true;
            }
        }
    }
}
