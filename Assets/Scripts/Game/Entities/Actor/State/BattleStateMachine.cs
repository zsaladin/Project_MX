using UnityEngine;
using System.Collections.Generic;
using System.Linq;


namespace MX
{
    public class BattleStateMachine : ITickable
    {
        private BattleActor _actor;
        private BattleActorTypeProfile _typeProfile;

        public BattleState[] States { get; private set; }
        public BattleState CurrentState { get; private set; }
        public BattleState DefaultState { get; private set; }

        public BattleStateMachine(BattleActor actor, BattleActorTypeProfile typeProfile)
        {
            _actor = actor;
            _typeProfile = typeProfile;

            States = new BattleState[typeProfile.States.Count];
            for (int i = 0; i < typeProfile.States.Count; ++i)
            {
                BattleStateProfile stateProfile = typeProfile.States[i];
                BattleState state = new BattleState();
                state.Init(stateProfile, _actor, this);
                States[i] = state;

                if (stateProfile.IsDefault)
                    DefaultState = state;
            }

            for (int i = 0; i < States.Length; ++i)
            {
                States[i].PostInit();
            }
            CurrentState = DefaultState;
            CurrentState.OnBegin();
        }

        public void OnTick()
        {
            BattleState nextState = CurrentState.OnConditionConfirm();
            if (nextState == null)
                CurrentState.OnTick();
            else
            {
                CurrentState.OnEnd();
                CurrentState = nextState;
                CurrentState.OnBegin();
            }
        }
    }
}
