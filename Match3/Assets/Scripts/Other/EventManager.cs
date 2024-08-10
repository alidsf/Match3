using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager
{
    private Dictionary<EventType, UnityEvent> _eventDictionary = new Dictionary<EventType, UnityEvent>();

    public void SubscribeEvent(EventType eventType, UnityAction listener)
    {
        UnityEvent currentEvent;

        if (!_eventDictionary.TryGetValue(eventType, out currentEvent))
        {
            currentEvent = new UnityEvent();
            _eventDictionary.Add(eventType, currentEvent);
        }

        currentEvent.AddListener(listener);
    }

    public void Invoke(EventType eventType)
    {
        UnityEvent currentEvent;

        if (_eventDictionary.TryGetValue(eventType, out currentEvent))
            currentEvent.Invoke();
    }
}

public enum EventType
{
    LoadLevel,
    LevelWin,
    GameOver,
    GoHome
}