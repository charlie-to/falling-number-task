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
    DecreaseLife,
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
    private EventType EventType;
    private DateTime dateTime;

    public TaskLogEvent( int _ID, EventType _eventType)
    {
        ID = _ID;
        EventType = _eventType;
        eventTypeString = _eventType.ToString();
        dateTime = DateTime.Now;
        EventAt = dateTime.ToString("O");
    }

    public void AddDestroyPoint(float _position)
    {
        DestroyPosition = _position;
    }

    public EventType GetEventType()
    {
        return EventType;
    }
    public float GetDestroyPosition()
    {
        return DestroyPosition;
    }
    public DateTime GetDateTime()
    {
        return dateTime;
    }

}