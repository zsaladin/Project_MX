using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class EventManager : MonoBehaviour 
{
    public delegate void Handler(BattleEvent e);
    private Dictionary<EventType, Handler> _eventLisener = new Dictionary<EventType,Handler>();
    private List<BattleEvent> _events = new List<BattleEvent>();

    private bool _isMouseDown;
    private bool _isMouseUp;

    private Vector3 _mouseDownPosition;

	public void Init()
    {
        _eventLisener.Add(EventType.MouseDown, OnMouseDown);
        _eventLisener.Add(EventType.MouseUp, OnMouseUp);
    }
	
	void Update () 
	{
		if (Input.GetMouseButtonDown(0))
            _isMouseDown = true;

        if (Input.GetMouseButtonUp(0))
            _isMouseUp = true;
	}

    public void OnTick()
    {
        DealMouseEvent();
    }

    public void OnEventTick()
    {
        foreach(var pair in _eventLisener)
        {
            var e = OnEvent(pair.Key);
            if (e != null) pair.Value(e);
        }
    }

    void DealMouseEvent()
    {
        if (_isMouseDown)
        {
            BattleEvent_MouseDown mouseDownEvent = BattleEvent.Create(EventType.MouseDown) as BattleEvent_MouseDown;
            mouseDownEvent._mousePosition = Input.mousePosition;
        }

        if (_isMouseUp)
        {
            BattleEvent_MouseUp mouseUpEvent = BattleEvent.Create(EventType.MouseUp) as BattleEvent_MouseUp;
            mouseUpEvent._mousePosition = Input.mousePosition;
        }

        _isMouseDown = false;
        _isMouseUp = false;
    }

    public BattleEvent OnEvent(EventType type)
    {
        return _events.Find(item => item.Type == type);
    }

    public void AddEvent(BattleEvent e)
    {
        _events.Add(e);
    }

    public void ReleaseEvents()
    {
        for(int i = 0; i < _events.Count; ++i)
        {
            BattleEvent.Release(_events[i]);
        }
        _events.Clear();
    }

    #region Global Events
    void OnMouseDown(BattleEvent e)
    {
        BattleEvent_MouseDown mouseEvent = e as BattleEvent_MouseDown;
        _mouseDownPosition = mouseEvent._mousePosition;
        
    }

    void OnMouseUp(BattleEvent e)
    {
        BattleEvent_MouseUp mouseEvent = e as BattleEvent_MouseUp;
        if ((mouseEvent._mousePosition - _mouseDownPosition).sqrMagnitude > 2) return;

        Ray ray = Camera.main.ScreenPointToRay(mouseEvent._mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, 100, LayerMask.GetMask("Ground")))
        {
            UserRecord record = Manager.Data.UserRecordSave.Get(Manager.Game._ourForceID);
            Manager.Entity.CreateActors(Ownership.OurForce, record.ActorRecords[Random.Range(0, record.ActorRecords.Count)], hit.point);
        }
    }

    #endregion
}
