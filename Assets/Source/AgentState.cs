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

        internal void Heal(int val)
        {
            _hits = System.Math.Min(_hits + val, _maxHits);
        }

        internal void Wound(int val)
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

        private bool _canHeal = false;

        public bool CanHeal
        {
            get { return _canHeal; }
            
        }
        private float _healRange = 5f;

        public float HealRange
        {
            get { return _healRange; }
        }
        private float _healCooldown = 8f;

        public float HealCooldown
        {
            get { return _healCooldown; }
        }

        private bool _healing = false;

        public bool Healing
        {
            get { return _healing; }
            set { _healing = value; }
        }

        private int _healPoints=2;

        public int HealPoints
        {
            get { return _healPoints; }
            
        }


        public AttackType[] attackTypes=new AttackType[3];

        private int _currentAttackType = 0;

        public int CurrentAttackType
        {
            get { return _currentAttackType; }
            set { _currentAttackType = value; }
        }
        

        public const int SNIPER  = 0;
        public const int EXPLORER = 1;
        public const int SOLDIER  = 2;
        public const int SUPPORT  = 3;

        public void SetRole(int agentType)
        {
            switch (agentType)
            {
                case SNIPER:
                    _maxHits = 6;
                    _speed = 75;
                    attackTypes[AgentAttack.LONG_RANGE] = new AttackType(50f, 0.2f, 5f,10);
                    attackTypes[AgentAttack.MEDIUM_RANGE] = new AttackType(20f, 0.8f,4f,3);
                    attackTypes[AgentAttack.SHORT_RANGE] = new AttackType(10f, 1.0f, 3f, 3);
                    break;
                case EXPLORER:
                    _maxHits = 10;
                    _speed = 125;
                    attackTypes[AgentAttack.LONG_RANGE] = new AttackType(40f, 2.2f, 4f, 3);
                    attackTypes[AgentAttack.MEDIUM_RANGE] = new AttackType(20f, 1.2f, 3f, 3);
                    attackTypes[AgentAttack.SHORT_RANGE] = new AttackType(15f, 0.5f, 2f, 5);
                    break;
                case SOLDIER:
                    _maxHits = 12;
                    _speed = 100;
                    attackTypes[AgentAttack.LONG_RANGE] = new AttackType(50f, 1.2f, 4f, 3);
                    attackTypes[AgentAttack.MEDIUM_RANGE] = new AttackType(25f, 0.5f, 3f, 5);
                    attackTypes[AgentAttack.SHORT_RANGE] = new AttackType(10f, 1.0f, 3f, 5);
                    break;
                case SUPPORT:
                    _maxHits = 6;
                    _speed = 80;
                    attackTypes[AgentAttack.LONG_RANGE] = new AttackType(40f, 2.0f, 5f, 3);
                    attackTypes[AgentAttack.MEDIUM_RANGE] = new AttackType(20f, 1.0f, 3f, 3);
                    attackTypes[AgentAttack.SHORT_RANGE] = new AttackType(10f, 1.0f, 3f, 3);
                    _canHeal = true;
                    break;
            }
            _hits = _maxHits;
        }


        

    }
}