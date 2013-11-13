using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace FSM
{
    public abstract class State
    {
        public abstract void Enter(StateMachine obj);

        public abstract void Execute(StateMachine obj);

        public abstract void Exit(StateMachine obj);

    }
}
