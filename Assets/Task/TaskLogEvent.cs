using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;


[System.Serializable]
public enum EventType
{
    NumberCreate,
    NumberDestroy,
    TypeCorrect,
    TypeWrong,
    TimePause,
}


[System.Serializable]
public class TaskLogEvent
{
    [SerializeField]
    private int ID;
    [SerializeField]
    private string EventAt;
    [SerializeField]
    private string eventTypeString;
    [SerializeField]
    private float DestroyPosition;
    private EventType eventType;

    public TaskLogEvent( int _ID, EventType _eventType)
    {
        ID = _ID;
        eventType = _eventType;
        eventTypeString = _eventType.ToString();
        EventAt = DateTime.Now.ToString("O");
    }

    public void AddDestroyPoint(float _position)
    {
        DestroyPosition = _position;
    }
}