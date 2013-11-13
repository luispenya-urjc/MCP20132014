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
        }

        public AgentState _state;

        public OPTIONS controllerType=OPTIONS.FSMAI;
        public AIController _controller;
        void Awake()
        {
            //_state = new AgentState(AgentState.EXPLORER);
            switch (controllerType){
                case OPTIONS.FSMAI:
                    _controller=new FSMAI(gameObject);
                    break;
                
                default:
                    _controller=new FSMAI(gameObject);
                    break;
            }
            //_controller.Init();
        }

        
        IEnumerator UpdateAIController(float waitTime)
        {
            while (true)
            {
                yield return new WaitForSeconds(waitTime);
                print("Refreshing FSM " + Time.time);
                _controller.RefreshAI();
            }
        }

        // Use this for initialization
        void Start()
        {
            _controller.Init(gameObject);
            StartCoroutine(UpdateAIController(0.5f));
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