using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class BattleEvent_MouseDown : BattleEvent 
{
    public override EventType Type
    {
        get { return EventType.MouseDown; }
    }
    public Vector3 _mousePosition;
}
