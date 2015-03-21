using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public abstract class BattleEvent 
{
    public abstract EventType Type { get;}

    protected BattleEvent() { }

    static Dictionary<EventType, Queue<BattleEvent>> _eventDic = new Dictionary<EventType, Queue<BattleEvent>>();
    public static BattleEvent Create(EventType type)
    {
        Queue<BattleEvent> events = null;
        if (_eventDic.TryGetValue(type, out events) == false)
        {
            events = new Queue<BattleEvent>();
            _eventDic.Add(type, events);
        }

        BattleEvent newEvent = null;

        if (events.Count > 0)
        {
            newEvent = events.Dequeue();
        }
        else
        {
            switch (type)
            {
                case EventType.MouseDown:
                    newEvent = new BattleEvent_MouseDown(); break;

                case EventType.MouseUp:
                    newEvent = new BattleEvent_MouseUp(); break;
            }
        }

        if (newEvent != null)
            Manager.Event.AddEvent(newEvent);

        return newEvent;
    }

    public static void Release(BattleEvent e)
    {
        _eventDic[e.Type].Enqueue(e);
    }
}

public enum EventType
{
    MouseDown,
    MouseUp
}
