using System.Collections;
using UnityEngine;

namespace FSM
{
    public class StateMachine
    {

        public State currentState;
        public State previousState = null;

        public Environment environment;
        public GameObject agent;

        // Use this for initialization
        internal void Init(GameObject obj, Environment env)
        {
            agent = obj;
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