using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using MCP_AI;

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
            GameObject t = obj.FindClosestEnemy();
            float d = Vector3.Distance(obj.agent.transform.position, t.transform.position);
            if (obj.controller.GetState().AttackTarget == null || obj.controller.GetState().CurrentAttackType == -1)
            {
                obj.controller.SetAttack(AgentAttack.MEDIUM_RANGE);
                obj.controller.SetAttackTarget(t);
            }
            if (d < obj.controller.GetState().attackTypes[obj.controller.GetState().CurrentAttackType].range/2.0f)
            {
                obj.controller.GetState().Moving = false;   
            }
            else if (d>30)
            {
                obj.controller.SetAttackTarget(null);
                obj.controller.SetAttack(-1);
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
