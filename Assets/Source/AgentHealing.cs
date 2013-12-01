using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace MCP_AI
{
	class AgentHealing: MonoBehaviour
	{
        public GameObject healingSphere;
        private SphereCollider _col;

        private AgentState _state;

        private float _cooldown = 0.0f;

        void Start()
        {
            _state = GetComponent<AgentAI>()._state;
            _cooldown = Time.time;

            if (_state != null && _state.CanHeal)
            {
                healingSphere=GameObject.CreatePrimitive(PrimitiveType.Sphere);
                healingSphere.transform.parent = gameObject.transform;
                healingSphere.transform.localPosition = Vector3.zero;
                healingSphere.transform.localScale = new Vector3(_state.HealRange, _state.HealRange);
                _col=healingSphere.GetComponent<SphereCollider>();
                healingSphere.renderer.enabled = false;
                
            }
        }

        private bool healEnabled()
        {
            return (_cooldown < Time.time && _state.CanHeal && _state.Healing);
        }

       

        void Update()
        {
            if (healEnabled())
            {
                _cooldown=Time.time+_state.HealCooldown;

                Collider[] cols=Physics.OverlapSphere(transform.position, _state.HealRange,LayerMask.NameToLayer("Players"));
                foreach (Collider c in cols)
                {
                    if (c.gameObject.transform == gameObject.transform.parent) //Same team
                    {
                        c.gameObject.GetComponent<AgentAI>()._state.Heal(_state.HealPoints);

                    }
                }
            }
        }
	}
}
