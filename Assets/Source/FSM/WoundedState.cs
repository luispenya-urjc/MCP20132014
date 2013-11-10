using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FSM
{
	class WoundedState: State
	{
        private static WoundedState instance = new WoundedState();
        public override void Enter(StateMachine obj)
        {
            Debug.Log("Enter " + this.GetType().Name);

            obj.agent.animation.Play("jump_pose");

        }

        public override void Execute(StateMachine obj)
        {
            Debug.Log("Execute: " + this.GetType().Name + "  Time: " + Time.time);
            if (obj.environment.Wounds < 3)
            {
                obj.ChangeState(obj.previousState);
            }
            else
            {
                obj.ChangeState(DeadState.GetInstance());
            }
        }

        public override void Exit(StateMachine obj)
        {
            Debug.Log("Exit " + this.GetType().Name);
        }

        public static WoundedState GetInstance()
        {
            return instance;
        }
	}
}
