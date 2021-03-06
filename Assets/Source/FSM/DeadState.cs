﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using MCP_AI;

namespace FSM
{
	class DeadState: State
	{
        private static DeadState instance = new DeadState();
        public override void Enter(StateMachine obj)
        {
            Debug.Log("Enter " + this.GetType().Name);

            GameObject.Destroy(obj.agent);

        }

        public override void Execute(StateMachine obj)
        {
            Debug.Log("Execute: " + this.GetType().Name + "  Time: " + Time.time);
            
        }

        public override void Exit(StateMachine obj)
        {
            Debug.Log("Exit " + this.GetType().Name);
        }

        public static DeadState GetInstance()
        {
            return instance;
        }
	}
}
