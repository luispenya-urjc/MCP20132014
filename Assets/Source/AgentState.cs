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

        private int _maxHits = 10;
        private int _hits = 10;

        public int Hits
        {
            get { return _hits; }
            
        }

        public void Heal(int val)
        {
            _hits=System.Math.Min(_hits +val,_maxHits);
        }

        public void Wound(int val)
        {
            _hits -= val;
        }

        private float _speed=100;

        public float Speed
        {
            get { return _speed; }
            
        }

        private bool _moving = false;

        public bool Moving
        {
            get { return _moving; }
            set { _moving = value; }
        }

        private Transform _movementTarget;

        public Transform MovementTarget
        {
            get { return _movementTarget; }
            set { _movementTarget = value; }
        }

        private GameObject _attackTarget;

        public GameObject AttackTarget
        {
            get { return _attackTarget; }
            set { _attackTarget = value; }
        }
    }
}