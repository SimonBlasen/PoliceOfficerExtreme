using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RigAgentManager : MonoBehaviour
{
    [SerializeField]
    private float hitImpactFactor = 1f;
    [SerializeField]
    private float hitUpForce = 1f;
    [SerializeField]
    private float hitAngularStrength = 1f;

    private AgentMover agentMover;

    private NavMeshAgent navMeshAgent = null;

    [SerializeField]
    private GameObject rigidDude = null;
    [SerializeField]
    private MeshRenderer[] nonRigidMeshes = null;

    bool inited = false;

    // Start is called before the first frame update
    void Start()
    {
        agentMover = GetComponent<AgentMover>();
        navMeshAgent = GetComponent<NavMeshAgent>();

        //enDisRigids(false);

        //enDisMeshes(true);

        if (!inited)
        {
            isRigidPerson = true;
            MakeNavigationAgent();
        }
    }

    // Update is called once per frame
    void Update()
    {/*
        if (Input.GetKeyDown(KeyCode.R))
        {
            MakeNavigationAgent();
        }*/
    }

    public void MakeRigid(Vector3 velocityImpact)
    {
        if (agentMover == null)
        {
            agentMover = GetComponent<AgentMover>();
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        inited = true;
        bool wasRigidPerson = isRigidPerson;
        isRigidPerson = true;

        if (wasRigidPerson != isRigidPerson)
        {
            navMeshAgent.enabled = false;

            enDisMeshes(false);
            enDisRigids(true);

            rigidDude.transform.position = transform.position;
            rigidDude.transform.rotation = transform.rotation;

            AgentRigOriginPos[] origPoses = rigidDude.GetComponentsInChildren<AgentRigOriginPos>();
            for (int i = 0; i < origPoses.Length; i++)
            {
                origPoses[i].ResetPose();
            }

            Rigidbody[] rigids = rigidDude.GetComponentsInChildren<Rigidbody>();

            Vector3 randAngVel = new Vector3(Random.Range(-hitAngularStrength, hitAngularStrength), Random.Range(-hitAngularStrength, hitAngularStrength), Random.Range(-hitAngularStrength, hitAngularStrength));

            for (int i = 0; i < rigids.Length; i++)
            {
                Rigidbody rig = rigids[i];
                rig.velocity = velocityImpact * hitImpactFactor + Vector3.up * velocityImpact.magnitude * hitUpForce;
                rig.angularVelocity = randAngVel;
            }

        }
    }

    public void MakeNavigationAgent()
    {
        if (agentMover == null)
        {
            agentMover = GetComponent<AgentMover>();
            navMeshAgent = GetComponent<NavMeshAgent>();
        }

        inited = true;
        bool wasRigidPerson = isRigidPerson;
        isRigidPerson = false;

        if (wasRigidPerson != isRigidPerson)
        {
            navMeshAgent.enabled = true;

            enDisMeshes(true);
            enDisRigids(false);

            agentMover.RandomlySpawn();
        }

    }

    private bool isVisible = true;
    public bool IsVisible
    {
        get
        {
            return isVisible;
        }
        set
        {
            isVisible = value;

            enDisMeshes(IsVisible);
        }
    }

    private void enDisRigids(bool enable)
    {
        rigidDude.SetActive(enable);

    }
    private void enDisMeshes(bool enable)
    {
        for (int i = 0; i < nonRigidMeshes.Length; i++)
        {
            nonRigidMeshes[i].enabled = enable && isVisible;
        }
    }

    public MeshRenderer[] NonRigidMeshes
    {
        get
        {
            return nonRigidMeshes;
        }
        set
        {
            nonRigidMeshes = value;
        }
    }


    private bool isRigidPerson = false;
    public bool IsRigidPerson
    {
        get
        {
            return isRigidPerson;
        }
    }
}
