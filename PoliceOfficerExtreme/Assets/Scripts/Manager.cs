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

    private int missionEventIndex = -1;
    private Mission currentMission = null;

    private float blockSpaceKeyFor = 0f;

    // Start is called before the first frame update
    void Start()
    {
        StartMission(Random.Range(0, missions.Length));
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
                if (currentMission != null && currentMission.missionEvents[missionEventIndex].eventType == MissionEventType.DIALOGUE)
                {
                    blockSpaceKeyFor = 1f;
                    dialogue.StopShowingText();
                    Invoke("nextMissionEvent", blockSpaceKeyFor);
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
        if (currentMission != null && currentMission.missionEvents[missionEventIndex].eventType == MissionEventType.KILL_ROBBER)
        {
            nextMissionEvent();
        }
    }

    public void EnterCar()
    {
        if (currentMission != null && currentMission.missionEvents[missionEventIndex].eventType == MissionEventType.ENTER_CAR)
        {
            nextMissionEvent();
        }
    }

    public void ReturnPoliceStation()
    {
        if (currentMission != null && currentMission.missionEvents[missionEventIndex].eventType == MissionEventType.RETURN_POLICE_STATION)
        {
            nextMissionEvent();
        }
    }

    public void StartMission(int index)
    {
        missionEventIndex = -1;
        currentMission = missions[index];

        nextMissionEvent();
    }
}
