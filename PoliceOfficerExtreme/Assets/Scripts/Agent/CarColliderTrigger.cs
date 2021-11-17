using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarColliderTrigger : MonoBehaviour
{
    [SerializeField]
    private RigAgentManager rigAgentManager = null;

    private Manager manager;

    private AgentMover agentMover;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<Manager>();
        agentMover = GetComponentInParent<AgentMover>();
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

            if (agentMover.IsRobber)
            {
                cc.HitRobber();
                manager.HitRobber();
            }
            else
            {
                agentMover.PlayPassantClip();
                agentMover.ReviveIn(Random.Range(20f, 30f));
            }
            rigAgentManager.MakeRigid(cc.Velocity);
        }
    }
}
