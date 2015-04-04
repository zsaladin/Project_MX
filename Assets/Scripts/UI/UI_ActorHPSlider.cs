using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class UI_ActorHPSlider : UIMonoBehaviour
    {
        private BattleActor _actor;
        private Transform _actorUITransform;

        private Slider _slider;
        private RectTransform _rectTransform;

        private float _totalDuration;
        private float _currentDuration;

        public void Init(BattleActor actor)
        {
            _actor = actor;
            _slider = GetComponent<Slider>();
            _rectTransform = _slider.transform as RectTransform;

            _actorUITransform = _actor.transform.FindChildRecursively(Manager.Constant.UI_HP_SLIDER_NAME);
            _totalDuration = Manager.Constant.UI_HP_SLIDER_DURATION;

            ColorBlock color = _slider.colors;
            color.normalColor = actor.OwnerShip == Ownership.OurForce ? Color.green : Color.red;
            _slider.colors = color;

            actor.AddUIController(this);
            _slider.gameObject.SetActive(false);
        }

        public override void Redraw(bool enable = true)
        {
            EnableUI(enable);
            UpdateUI();
            Update();
        }

        public override void EnableUI(bool enable)
        {
            if (enable)
            {
                _currentDuration = _totalDuration;
            }
            else
            {
                _currentDuration = 0;
            }
            _slider.gameObject.SetActive(enable);
        }

        public override void UpdateUI()
        {
            _slider.value = _actor.HitPoint / _actor.HitPointMax;
        }

        void Update()
        {
            UpdateDuration();
            UpdatePosition();
        }

        void UpdateDuration()
        {
            _currentDuration -= Time.deltaTime;
            _slider.gameObject.SetActive(_currentDuration > 0);
        }

        void UpdatePosition()
        {
            if (_currentDuration > 0)
            {
                _rectTransform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, _actorUITransform.position);
            }

        }
    }
}