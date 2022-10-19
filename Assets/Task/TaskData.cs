using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TaskData
{
    [SerializeField]
    private string SubjectNumber;
    [SerializeField]
    private float FallingSpeed;
    [SerializeField]
    private float NumberSpawnDelayTime;
    [SerializeField]
    private String TaskStartAt;
    [SerializeField]
    private List<TaskLogEvent> taskEvents = new List<TaskLogEvent>();
    [SerializeField]
    private int LifeNumber;
    [SerializeField]
    private int NumberOfDeleteOnDecreaseLife;
    

    public TaskData (string _SubjectNumber, float _FallingSpeed, float _NumberSpawnDelayTime, int lifeNumber, int numberOfDeleteOnDecreaseLife)
    {
        SubjectNumber = _SubjectNumber;
        FallingSpeed = _FallingSpeed;
        NumberSpawnDelayTime = _NumberSpawnDelayTime;
        TaskStartAt = DateTime.Now.ToString("u");
        LifeNumber = lifeNumber;
        NumberOfDeleteOnDecreaseLife = numberOfDeleteOnDecreaseLife;
        // AreaOfDeleteOnDecreaseLife = areaOfDeleteOnDecreaseLife;
    }

    public void AddTaskEvent(EventType _eventType)
    {
        var taskLogEvent = new TaskLogEvent(taskEvents.Count + 1, _eventType);
        taskEvents.Add(taskLogEvent);
    }

    public void AddTaskEvent(EventType _eventType,float DestroyPosition)
    {
        var taskLogEvent = new TaskLogEvent(taskEvents.Count + 1, _eventType);
        taskLogEvent.AddDestroyPoint(DestroyPosition);
        taskEvents.Add(taskLogEvent);
    }
}


