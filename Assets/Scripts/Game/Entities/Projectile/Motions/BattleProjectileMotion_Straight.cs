using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class BattleProjectileMotion_Straight : BattleProjectileMotion
    {
        public override void OnTick()
        {
            _projectitle.Position = Vector3.MoveTowards(_projectitle.Position, _projectitle.TargetPosition, _speed * Manager.Constant.GAME_TICK);
        }

        public override bool IsEnded()
        {
            return (_projectitle.Position - _projectitle.TargetPosition).sqrMagnitude <= 0.001f;
        }
    }
}