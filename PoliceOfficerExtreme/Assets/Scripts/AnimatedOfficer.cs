using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedOfficer : MonoBehaviour
{
    private Camera[] cameras = null;
    public Vector3 localOffset;
    public float scale;


    // Start is called before the first frame update
    void Start()
    {
        cameras = FindObjectsOfType<Camera>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i].enabled && cameras[i].gameObject.activeInHierarchy)
            {
                transform.position = cameras[i].transform.TransformPoint(localOffset);
                transform.rotation = Quaternion.LookRotation(cameras[i].transform.forward, cameras[i].transform.up);
                transform.localScale = new Vector3(scale, scale, scale);
            }
        }
    }
}
