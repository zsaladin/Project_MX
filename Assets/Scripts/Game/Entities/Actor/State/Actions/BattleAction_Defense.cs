using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class BattleAction_Defense : BattleAction
    {
        public override ActionType Type
        {
            get
            {
                return ActionType.Defense;
            }
        }

        public BattleAction_Defense(BattleActor actor)
            : base(actor)
        {

        }


        public void Defense(float damage)
        {
            if (Actor.debug)
            {
                Debug.Log("A");
            }
            Actor.Property.HitPoint -= damage;
            Actor.RedrawUIs();
        }
    }

    public enum DefenseType
    {
        Invalid = 0x00,
        Light = 0x01,
        Medium = 0x02,
        Heavy = 0x04,
    }
}