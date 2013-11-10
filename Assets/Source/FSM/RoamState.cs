using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FSM
{
	class RoamState: State
	{
        private static RoamState instance = new RoamState();
        private static int lastWound=0;
        public override void Enter(StateMachine obj)
        {
            Debug.Log("Enter " + this.GetType().Name);
            obj.agent.animation.CrossFade("walk");
          
        }

        public override void Execute(StateMachine obj)
        {
            Debug.Log("Execute: " + this.GetType().Name + "  Time: " + Time.time);
            if (obj.environment.Show)
            {
                obj.ChangeState(ChaseState.GetInstance());
            }
            if (!obj.environment.Sound)
            {
                obj.ChangeState(InitialState.GetInstance());
            }

            if (obj.environment.Wounds > lastWound)
            {
                lastWound = obj.environment.Wounds;
                obj.ChangeState(WoundedState.GetInstance());
            }
        }

        public override void Exit(StateMachine obj)
        {
            Debug.Log("Exit " + this.GetType().Name);
        }

        public static RoamState GetInstance()
        {
            return instance ;
        }
    }
}
