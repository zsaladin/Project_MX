using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class UIManager : MonoBehaviour
    {
        public CanvasRenderer _rootOfBattleInfo;
        public UI_ActorHPSlider _actorHPSliderPrefab;

        public void Init()
        {

        }

        public void InitActorSlider(BattleActor actor)
        {
            UI_ActorHPSlider slider = Instantiate<UI_ActorHPSlider>(_actorHPSliderPrefab);
            slider.transform.SetParent(_rootOfBattleInfo.transform, false);
            slider.Init(actor);
        }

    }
}