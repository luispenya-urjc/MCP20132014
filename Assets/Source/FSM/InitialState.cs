using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FSM
{
	class InitialState: State
	{
        private static InitialState instance = new InitialState();
        public override void Enter(StateMachine obj)
        {
            Debug.Log("Enter " + this.GetType().Name);
            obj.agent.animation.CrossFade("idle");
          
        }

        public override void Execute(StateMachine obj)
        {
            Debug.Log("Execute: " + this.GetType().Name + "  Time: " + Time.time);
            if (obj.environment.Show)
            {
                obj.ChangeState(ChaseState.GetInstance());
            }
            if (obj.environment.Sound)
            {
                obj.ChangeState(RoamState.GetInstance());
            }
        }

        public override void Exit(StateMachine obj)
        {
            Debug.Log("Exit " + this.GetType().Name);
        }

        public static InitialState GetInstance()
        {
            return instance ;
        }
    }
}
