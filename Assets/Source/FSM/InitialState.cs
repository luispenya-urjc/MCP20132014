using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using MCP_AI;

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


            AgentState s = obj.controller.GetState();

           // s.AttackTarget = GameObject.Find("AttTarget");
            
            GameObject t = obj.FindClosestEnemy();
            float d = Vector3.Distance(obj.agent.transform.position, t.transform.position);

            Debug.Log(obj.agent.name + ":: " + d+" to "+t.transform.position);  
            if (d > 20)
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
