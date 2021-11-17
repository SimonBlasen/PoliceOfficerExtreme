using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarColliderTrigger : MonoBehaviour
{
    [SerializeField]
    private RigAgentManager rigAgentManager = null;

    private Manager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        CarController cc = other.transform.GetComponentInParent<CarController>();
        if (cc != null)
        {
            Debug.Log("Robber was hit by car");

            cc.HitRobber();
            rigAgentManager.MakeRigid(cc.Velocity);
            manager.HitRobber();
        }
    }
}
