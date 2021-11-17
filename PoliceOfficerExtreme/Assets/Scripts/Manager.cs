using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    [SerializeField]
    private Mission[] missions;

    [Space]
    [SerializeField]
    private Dialogue dialogue = null;
    [SerializeField]
    private AudioSource dialogueClipSource = null;
    [SerializeField]
    private GameObject firstPersonPlayer = null;
    [SerializeField]
    private Camera camCar = null;
    [SerializeField]
    private float enterCarDistance = 1f;
    [SerializeField]
    private float carPoliceStationDistance = 1f;
    [SerializeField]
    private Transform policeStationPos = null;
    [SerializeField]
    private RigAgentManager robberRigAgentManager = null;
    [SerializeField]
    private AgentMover robberAgentMover = null;

    private int missionEventIndex = -1;
    private int lastMissionIndex = -1;
    private Mission currentMission = null;

    private float blockSpaceKeyFor = 0f;
    private CarController cc;

    private GameObject instRobberWaypoints = null;
    private GameObject missionAccomplishedScreen = null;

    private List<int> missionsDone = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        missionsDone.Add(-1);

        cc = FindObjectOfType<CarController>();
        Vector3 spawnPos = firstPersonPlayer.transform.position;
        IsFirstPerson = true;
        firstPersonPlayer.transform.position = spawnPos;
        StartMission(Random.Range(0, missions.Length));

        robberRigAgentManager.IsVisible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (blockSpaceKeyFor > 0f)
        {
            blockSpaceKeyFor -= Time.deltaTime;
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (missionAccomplishedScreen != null)
                {
                    Destroy(missionAccomplishedScreen);
                    missionAccomplishedScreen = null;
                    missionsDone.Add(lastMissionIndex);
                    StartMission(-1);
                }

                if (currentMission != null && currentMission.missionEvents[missionEventIndex].eventType == MissionEventType.DIALOGUE)
                {
                    blockSpaceKeyFor = 0.1f;
                    dialogue.StopShowingText();
                    Invoke("nextMissionEvent", blockSpaceKeyFor);
                }
            }
        }


        if (Input.GetKeyDown(KeyCode.E))
        {
            if (IsFirstPerson)
            {
                if (Vector3.Distance(cc.transform.position, firstPersonPlayer.transform.position) <= enterCarDistance)
                {
                    IsFirstPerson = false;

                    EnterCar();
                }
            }
            else
            {
                IsFirstPerson = true;

                if (Vector3.Distance(cc.transform.position, policeStationPos.position) <= carPoliceStationDistance)
                {
                    ReturnPoliceStation();
                }
            }
        }
    }




    private void nextMissionEvent()
    {
        missionEventIndex++;

        if (missionEventIndex >= currentMission.missionEvents.Length)
        {
            Debug.Log("Mission finished");


            missionAccomplishedScreen = Instantiate(currentMission.missionAccomplishedScreen);
            currentMission = null;

            return;
        }

        DialogueText curEvent = currentMission.missionEvents[missionEventIndex];

        if (curEvent.eventType == MissionEventType.DIALOGUE)
        {
            dialogue.ShowDialogueText(curEvent.dialogueText);

            dialogueClipSource.clip = curEvent.clip;
            dialogueClipSource.loop = false;
            dialogueClipSource.Play();
        }
    }

    public void HitRobber()
    {
        if (currentMission != null && currentMission.missionEvents[missionEventIndex].eventType == MissionEventType.DIALOGUE && (missionEventIndex + 1) < currentMission.missionEvents.Length && currentMission.missionEvents[missionEventIndex + 1].eventType == MissionEventType.KILL_ROBBER)
        {
            dialogue.StopShowingText();
            nextMissionEvent();
        }

        if (currentMission != null && currentMission.missionEvents[missionEventIndex].eventType == MissionEventType.KILL_ROBBER)
        {
            nextMissionEvent();
        }
    }

    public void EnterCar()
    {
        if (currentMission != null && currentMission.missionEvents[missionEventIndex].eventType == MissionEventType.DIALOGUE && (missionEventIndex + 1) < currentMission.missionEvents.Length && currentMission.missionEvents[missionEventIndex + 1].eventType == MissionEventType.ENTER_CAR)
        {
            dialogue.StopShowingText();
            nextMissionEvent();
        }

        if (currentMission != null && currentMission.missionEvents[missionEventIndex].eventType == MissionEventType.ENTER_CAR)
        {
            robberRigAgentManager.IsVisible = true;
            robberRigAgentManager.MakeNavigationAgent();
            robberAgentMover.SpawnAt(currentMission.startRunningPosition);
            robberAgentMover.RunToRandomTarget();
            nextMissionEvent();
        }
    }

    public void ReturnPoliceStation()
    {
        if (currentMission != null && currentMission.missionEvents[missionEventIndex].eventType == MissionEventType.DIALOGUE && (missionEventIndex + 1) < currentMission.missionEvents.Length && currentMission.missionEvents[missionEventIndex + 1].eventType == MissionEventType.RETURN_POLICE_STATION)
        {
            dialogue.StopShowingText();
            nextMissionEvent();
        }

        if (currentMission != null && currentMission.missionEvents[missionEventIndex].eventType == MissionEventType.RETURN_POLICE_STATION)
        {
            nextMissionEvent();
        }
    }

    public void StartMission(int index)
    {
        missionEventIndex = -1;


        // All missions done
        if (missionsDone.Count > missions.Length)
        {
            Debug.Log("All missions done");
            return;
        }



        if (index == -1)
        {
            int counter = 0;
            while (missionsDone.Contains(index) && counter <= 400)
            {
                index = Random.Range(0, missions.Length);
                counter++;
            }
        }


        currentMission = missions[index];
        robberRigAgentManager.IsVisible = false;

        if (instRobberWaypoints != null)
        {
            Destroy(instRobberWaypoints);
            instRobberWaypoints = null;
        }

        instRobberWaypoints = Instantiate(currentMission.waypointsPrefab);
        instRobberWaypoints.transform.position = Vector3.zero;
        instRobberWaypoints.transform.rotation = Quaternion.identity;

        Transform[] waypoints = new Transform[instRobberWaypoints.transform.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = instRobberWaypoints.transform.GetChild(i);
        }

        robberAgentMover.PossibleWaypoints = waypoints;

        //rigAgentManager.MakeRigid(Vector3.zero);

        lastMissionIndex = index;

        nextMissionEvent();
    }

    public bool IsFirstPerson
    {
        get
        {
            return isFirstPerson;
        }
        set
        {
            bool wasFirstPerson = isFirstPerson;
            isFirstPerson = value;

            if (isFirstPerson != wasFirstPerson)
            {
                cc.IsActive = !isFirstPerson;
                camCar.enabled = !isFirstPerson;


                if (isFirstPerson)
                {
                    RaycastHit hit;
                    firstPersonPlayer.transform.position = cc.FirstPersonSpawnPos.position;
                    if (Physics.Raycast(new Ray(cc.FirstPersonSpawnPos.position + Vector3.up * 200f, Vector3.down), out hit, 400f))
                    {
                        firstPersonPlayer.transform.position = hit.point + new Vector3(0f, 2f, 0f);
                    }
                }
                firstPersonPlayer.SetActive(isFirstPerson);
            }
        }
    }

    private bool isFirstPerson = false;
}
