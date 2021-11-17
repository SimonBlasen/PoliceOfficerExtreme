using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MissionEventType
{
    DIALOGUE, ENTER_CAR, RETURN_POLICE_STATION, KILL_ROBBER
}


[CreateAssetMenu(fileName = "Mission", menuName = "ScriptableObjects/Mission", order = 1)]
public class Mission : ScriptableObject
{
    public Vector3 startRunningPosition;
    public DialogueText[] missionEvents;
}



[Serializable]
public class DialogueText
{
    public MissionEventType eventType;


    public string dialogueText;
    public AudioClip clip;
}