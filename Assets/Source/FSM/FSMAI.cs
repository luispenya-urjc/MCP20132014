using UnityEngine;
using System.Collections;

namespace MCP_AI
{
    public class FSMAI : AIController
    {

        private FSM.StateMachine state_machine = new FSM.StateMachine();
        public Object blood;

       

        // Use this for initialization
        public FSMAI(GameObject agent)
        {
            _agent = agent;
            Init();
            
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

        public override void Init()
        {
            FSM.Environment environment = GameObject.Find("Environment").GetComponent<FSM.Environment>();
            state_machine.Init(_agent, environment);
        }

        public override void RefreshAI()
        {
            state_machine.UpdateFSM();
        }
    }
}