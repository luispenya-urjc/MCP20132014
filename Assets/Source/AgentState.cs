using UnityEngine;
using System.Collections;

namespace MCP_AI
{
    public class AgentState: ScriptableObject
    {

        private GameObject _target;

        public GameObject Target
        {
            get { return _target; }
            set { _target = value; }
        }

        private Pathfinding.Path _currentPath;

        public Pathfinding.Path CurrentPath
        {
            get { return _currentPath; }
            set { _currentPath = value; }
        }


        private int _hits = 10;

        public int Hits
        {
            get { return _hits; }
            set { _hits = value; }
        }

        private float _speed=100;

        public float Speed
        {
            get { return _speed; }
            
        }

    
    }
}