using UnityEngine;
using System.Collections;
using UnityEditor;
using FSM;

namespace MCP_AI
{
    public class AgentAI : MonoBehaviour
    {


        public enum OPTIONS
        {
            FSMAI = 0,
            BTAI = 1,
            RandomAI = 2,
			CustomAI = 3,
        }

        public AgentState _state;

        public OPTIONS controllerType=OPTIONS.CustomAI;
        public AIController _controller=null;
        void Awake()
        {
            //_state = new AgentState(AgentState.EXPLORER);
            switch (controllerType){
                case OPTIONS.FSMAI:
                    _controller=AIController.CreateInstance<FSMAI>();//gameObject);
                    break;
                case OPTIONS.RandomAI:
                    _controller = AIController.CreateInstance<RandomAI>();
                    break;
                case OPTIONS.BTAI:
                    _controller = AIController.CreateInstance<BT.BTAI>();
                    break;

            }
            //_controller.Init();
        }

        
        IEnumerator UpdateAIController(float waitTime)
        {
            while (true)
            {
                yield return new WaitForSeconds(waitTime);
               // print("Refreshing FSM " + Time.time);
                _controller.RefreshAI();
            }
        }


        // Use this for initialization
        IEnumerator Start()
        {
            yield return new WaitForSeconds(0.5f);
            if (_controller != null)
            {

                _controller.Init(gameObject);
                StartCoroutine(UpdateAIController(0.5f));
            }
            else
            {
                Debug.Log("ERROR no controller set!");
            }
        }


        void Update()
        {
            if (_state.Hits <= 0)
            {
                Object o = GameObject.Instantiate(Resources.Load<GameObject>("Blood"), transform.position, transform.rotation);
                float time = ((GameObject)o).particleSystem.duration;
                
                GameObject.Destroy(o, 1.0f);
                StopAllCoroutines();

                SendMessageUpwards("AgentDie", gameObject);
                GameObject.Destroy(gameObject);
                
                
            }
        }

        

    }
}