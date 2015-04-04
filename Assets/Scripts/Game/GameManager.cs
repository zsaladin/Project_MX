using UnityEngine;
using System.Collections.Generic;
using System.Linq;

namespace MX
{
    public class GameManager : MonoBehaviour
    {
        public static int _tickCount;
        protected float _currentTickCounter;

        public int _ourForceID = 1;
        public int _enemyForceID = 2;

        private UserRecord _ourRecord;
        private UserRecord _enemyRecord;

        public bool _autoStart = false;

        public void Init()
        {
            _tickCount = 0;
            _currentTickCounter = 0;

            _ourRecord = Manager.Data.UserRecordSave.Get(_ourForceID);
            _enemyRecord = Manager.Data.UserRecordSave.Get(_enemyForceID);

            if (_autoStart)
                Manager.Entity.CreateUser(Ownership.OurForce, _ourRecord);
            Manager.Entity.CreateUser(Ownership.EnemyForce, _enemyRecord);
        }

        void Update()
        {
            _currentTickCounter += Time.deltaTime;
            if (_currentTickCounter >= Manager.Constant.GAME_TICK)
            {
                _currentTickCounter -= Manager.Constant.GAME_TICK;

                Manager.Coordinate.OnTick();

                Manager.Event.OnTick();
                Manager.Entity.OnTick();

                Manager.Event.OnEventTick();
                Manager.Event.ReleaseEvents();

                Manager.Entity.OnPostTick();
                _tickCount++;
            }
        }
    }
}