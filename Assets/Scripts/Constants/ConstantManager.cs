using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class ConstantManager : MonoBehaviour
    {
        public readonly float GAME_TICK = 0.1f;

        public readonly string UI_HP_SLIDER_NAME = "HPSlider";
        public readonly string ACTOR_LAUNCHER_POINT_NAME = "LauncherPoint";
        public readonly string ACTOR_PROJECTILE_HIT_POINT_NAME = "ProjectileHitPoint";

        public readonly float UI_HP_SLIDER_DURATION = 2f;


        public readonly Constant_Effect Effect = new Constant_Effect();

        public void Init()
        {

        }
    }
}