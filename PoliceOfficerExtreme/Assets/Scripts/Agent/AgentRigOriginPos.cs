using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentRigOriginPos : MonoBehaviour
{
    private Vector3 originalLocalPos;
    private Quaternion originalLocalRot;

    // Start is called before the first frame update
    void Start()
    {
        originalLocalPos = transform.localPosition;
        originalLocalRot = transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ResetPose()
    {
        transform.localPosition = originalLocalPos;
        transform.localRotation = originalLocalRot;
    }
}
