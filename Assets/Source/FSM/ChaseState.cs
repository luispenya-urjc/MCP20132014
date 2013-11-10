using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FSM
{
	class ChaseState : State
	{
        private static ChaseState instance = new ChaseState();
        public override void Enter(StateMachine obj)
        {
            Debug.Log("Enter " + this.GetType().Name);
            obj.agent.animation.CrossFade("run");

        }

        public override void Execute(StateMachine obj)
        {
            Debug.Log("Execute: " + this.GetType().Name + "  Time: " + Time.time);
            if (!obj.environment.Show)
            {
                obj.ChangeState(InitialState.GetInstance());
            }
        }

        public override void Exit(StateMachine obj)
        {
            Debug.Log("Exit " + this.GetType().Name);
        }

        public static ChaseState GetInstance()
        {
            return instance;
        }
	}
}
