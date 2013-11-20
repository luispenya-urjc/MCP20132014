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
            yield return new WaitForSeconds(1);
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
                GameObject.Destroy(gameObject);
            }
        }

    }
}