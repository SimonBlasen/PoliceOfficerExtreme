using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentMover : MonoBehaviour
{
    [SerializeField]
    private Animator animator = null;
    [SerializeField]
    private Transform[] possibleDestinations = null;
    [SerializeField]
    private Transform[] spawnPositions = null;
    [SerializeField]
    private float targetReachedThresh = 1f;

    private NavMeshAgent agent;
    private RigAgentManager rigAgentManager;

    private Vector3 nextTarget = Vector3.zero;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        rigAgentManager = GetComponent<RigAgentManager>();

        RandomlySpawn();
        RunToRandomTarget();
    }

    // Update is called once per frame
    void Update()
    {
        if (rigAgentManager.IsRigidPerson == false)
        {
            if (nextTarget != Vector3.zero)
            {
                if (Vector3.Distance(nextTarget, transform.position) <= targetReachedThresh)
                {
                    Debug.Log("Agent reached target");

                    RunToRandomTarget();
                }
            }
        }
    }

    public void RunToRandomTarget()
    {
        Vector3 oldTarget = nextTarget;
        int cntr = 0;
        while (oldTarget == nextTarget && cntr <= 100)
        {
            nextTarget = possibleDestinations[Random.Range(0, possibleDestinations.Length)].position;
            cntr++;
        }

        agent.SetDestination(nextTarget);
    }

    public void RandomlySpawn()
    {
        transform.position = spawnPositions[Random.Range(0, spawnPositions.Length)].position;
    }
}
