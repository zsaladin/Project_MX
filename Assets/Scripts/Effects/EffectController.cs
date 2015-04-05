using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class EffectController : MonoBehaviour
    {
        private bool _isInit = false;

        private float _duration;
        private bool _isLooping;

        public void Init(BattleEffectProfile profile, BattleActor targetActor)
        {
            Init(profile.Delay, profile.Duration, profile.IsLooping, profile.IsGlobal, profile.NodeType, targetActor);
        }

        public void Init(BattleEffectProfile profile, Vector3 targetSpot)
        {
            Init(profile.Delay, profile.Duration, profile.IsLooping, targetSpot);
        }

        public void Init(float delay, float duration, bool isLooping, bool isGlobal, EffectNodeType type, BattleActor targetActor)
        {
            Init(delay, duration, isLooping);

            Transform targetTrans = targetActor.transform.FindChildRecursively(type.ToString());
            transform.SetParent(targetTrans);
            transform.localPosition = Vector3.zero;

            if (isGlobal)
                transform.SetParent(null);
        }

        public void Init(float delay, float duration, bool isLooping, Vector3 targetSpot)
        {
            Init(delay, duration, isLooping);

            transform.position = targetSpot;
        }

        public void Init(float delay, float duration, bool isLooping)
        {
            _isInit = true;

            _duration = duration;
            _isLooping = isLooping;

            if (delay > 0)
            {
                gameObject.SetActive(false);
                Invoke("StartAfterDelay", delay);
            }
        }

        void StartAfterDelay()
        {
            gameObject.SetActive(true);
        }

        public void Destroy()
        {
            _isInit = true;
            _duration = 0f;
        }

        void Update()
        {
            if (_isInit == false)
                return;

            _duration -= Time.deltaTime;
            if (_duration > 0f)
                return;

            Destroy(gameObject);
        }
    }

    public enum EffectNodeType
    {
        Invalid,
        Origin,
        OverHead,
        Head,
        Body,
        Weapon,
    }
}