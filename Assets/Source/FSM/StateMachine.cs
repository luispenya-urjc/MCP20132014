using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MCP_AI;

namespace FSM
{
    public class StateMachine
    {

        public State currentState;
        public State previousState = null;

        public Environment environment;
        public GameObject agent;
        public FSMAI controller;

        internal List<GameObject> GetFriends()
        {
            if (controller.faction == Environment.FACTIONS.ATTACKER)
                return environment.attackers;
            else
                return environment.defenders;
        }

        internal List<GameObject> GetFoes()
        {
            if (controller.faction != Environment.FACTIONS.ATTACKER)
                return environment.attackers;
            else
                return environment.defenders;
        }

        internal GameObject FindClosestEnemy()
        {
            Vector3 orig=agent.transform.position;
            if (GetFoes().Count>0){
                GameObject res=GetFoes()[0];
                

                float dist = Vector3.Distance(orig, res.transform.position);
                float aux;
                foreach (GameObject o in GetFoes())
                {
                    aux=Vector3.Distance(orig, o.transform.position);
                    if (dist > aux)
                    {
                        dist = aux;
                        res = o;
                    }
                }
                return res;
            } else {
                return null;
            }
        }

        // Use this for initialization
        internal void Init(GameObject obj, Environment env)
        {
            agent = obj;
            controller = agent.GetComponent<AgentAI>()._controller as FSMAI;
            environment = env;
            currentState = InitialState.GetInstance();
            previousState = null;
            currentState.Enter(this);
        }

        // Update is called once per frame
        public void UpdateFSM()
        {
            if (currentState != null)
                currentState.Execute(this);
        }

        public void ChangeState(State newState)
        {
            currentState.Exit(this);
            previousState = currentState;
            currentState = newState;
            currentState.Enter(this);
        }

        
    }
}