using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using UnityEngine;

namespace MCP_AI
{
	public abstract class AIController
	{
        protected GameObject _agent;

        public abstract void Init();
        public abstract void RefreshAI();

        public AgentState GetState()
        {
            return _agent.GetComponent<AgentAI>()._state;
        }

        public void Destroy()
        {
            GameObject.Destroy(_agent);
        }
	}
}
