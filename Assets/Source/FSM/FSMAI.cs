using UnityEngine;
using System.Collections;
using MCP_AI;

namespace FSM
{
    public class FSMAI : AIController
    {

        private StateMachine state_machine = new StateMachine();
        public Object blood;

       

        // Use this for initialization
        public FSMAI(GameObject agent)
        {
            
            Init(agent);
            
            //    .StartCoroutine(RefreshFSM(2.0F));

        }

         

       /* IEnumerator RefreshFSM(float waitTime)
        {
            while (true)
            {
                yield return new WaitForSeconds(waitTime);
                Debug.Log("Refreshing FSM " + Time.time);
                RefreshAI();
            }
        }

       */

        void OnDestroy()
        {
            Object o = GameObject.Instantiate(blood, _agent.transform.position, _agent.transform.rotation);
            float time = ((GameObject)o).particleSystem.duration;
            MonoBehaviour.Destroy(o, 1.0f);
        }

        public override void Init(GameObject agent)
        {
            base.Init(agent);
            MCP_AI.Environment environment = GetEnvironment();
            state_machine.Init(_agent, environment);
        }

        public override void RefreshAI()
        {
            state_machine.UpdateFSM();
        }
    }
}