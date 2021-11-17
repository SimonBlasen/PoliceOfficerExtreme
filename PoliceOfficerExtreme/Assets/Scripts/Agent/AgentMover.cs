using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentMover : MonoBehaviour
{
    [SerializeField]
    private bool isRobber = false;
    [SerializeField]
    private Animator animator = null;
    [SerializeField]
    private Transform[] possibleDestinations = null;
    [SerializeField]
    private Transform[] spawnPositions = null;
    [SerializeField]
    private float targetReachedThresh = 1f;
    [SerializeField]
    private AudioClip[] passantHitClips;
    [SerializeField]
    private AudioSource passantHitAudioSource;

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
        else
        {
            if  (reviveIn > 0f)
            {
                reviveIn -= Time.deltaTime;
                if (reviveIn <= 0f)
                {
                    rigAgentManager.MakeNavigationAgent();
                    RandomlySpawn();
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

    public void SpawnAt(Vector3 point)
    {
        transform.position = point;
    }

    public bool IsRobber
    {
        get
        {
            return isRobber;
        }
    }

    public void PlayPassantClip()
    {
        if (passantHitAudioSource.isPlaying == false)
        {
            passantHitAudioSource.clip = passantHitClips[Random.Range(0, passantHitClips.Length)];
            passantHitAudioSource.Play();
        }
    }


    private float reviveIn = 0f;
    public void ReviveIn(float seconds)
    {
        reviveIn = seconds;
    }
}
