using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using MCP_AI;

namespace FSM
{
	class RoamState: State
	{
        private static RoamState instance = new RoamState();
       
        public override void Enter(StateMachine obj)
        {
            
            AgentState s = obj.controller.GetState();
            GameObject t = obj.FindClosestEnemy();
           
            obj.controller.SetMovementTarget(t.transform);

            
            Debug.Log("Enter " + this.GetType().Name + " : " + obj.agent.name + " --> " + obj.controller.GetState().Moving);
            //obj.agent.animation.CrossFade("walk");


          
        }

        public override void Execute(StateMachine obj)
        {
            AgentState s = obj.controller.GetState();

            if (s.CurrentPath != null && s.CurrentPath.IsDone())
            {
                obj.controller.GetState().Moving = true;
            }

           
           // s.AttackTarget = GameObject.Find("AttTarget");
            
            GameObject t = obj.FindClosestEnemy();
            obj.controller.SetMovementTarget(t.transform);

            float d = Vector3.Distance(obj.agent.transform.position, t.transform.position);

            Debug.Log(obj.agent.name + ":: " + d+" to "+t.transform.position);  
            if (d <= 20)
            {
                obj.ChangeState(ChaseState.GetInstance());
            }
        }

        public override void Exit(StateMachine obj)
        {
            obj.agent.GetComponent<AgentAI>()._state.Moving = false;
        }

        public static RoamState GetInstance()
        {
            return instance ;
        }
    }
}
