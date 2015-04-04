using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class BattleEvent_MouseUp : BattleEvent
    {
        public override EventType Type
        {
            get { return EventType.MouseUp; }
        }
        public Vector3 _mousePosition;
    }
}