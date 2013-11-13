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

        protected AgentState _state;

        public virtual void Init(GameObject agent)
        {
            _agent = agent;
        }
        public abstract void RefreshAI();

        public AgentState GetState()
        {
            if (_state==null)
                _state=_agent.GetComponent<AgentAI>()._state;

            return _state;
        }

        public void SetAttack(int attackType)
        {
            if (attackType > AgentAttack.SHORT_RANGE)
            {
                Debug.LogError("Not suitable attack: " + attackType);
                return;
            }
            GetState().CurrentAttackType=attackType;
        }

        public void SetAttackTarget(GameObject target)
        {
            GetState().AttackTarget = target;
        }

        public void SetMovementTarget(Transform target)
        {
            GetState().MovementTarget = target;
        }

        public Environment GetEnvironment()
        {
            return GameObject.Find("Environment").GetComponent<Environment>();
        }

        public void HealNow()
        {
            if (GetState().CanHeal)
            {
                GetState().MovementTarget = null;
                GetState().Moving = false;
                GetState().CurrentAttackType = -1;
                GetState().Healing = true;
            }
        }

        public void Destroy()
        {
            GameObject.Destroy(_agent);
        }
	}
}
