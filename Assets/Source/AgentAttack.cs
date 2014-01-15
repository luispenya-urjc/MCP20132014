using UnityEngine;
using System.Collections;


namespace MCP_AI
{
    
    public struct AttackType
    {
        public float range, aim,cooldown;
        public int damage;
        

        public AttackType(float ran, float a, float cool, int dam)
        {
            range = ran;
            aim = a;
            cooldown = cool;
            damage = dam;
        }
    }

    public class AgentAttack : MonoBehaviour
    {

        public const int LONG_RANGE = 0;
        public const int MEDIUM_RANGE = 1;
        public const int SHORT_RANGE = 2;
        


        private AgentState _state;
        
        

        // Use this for initialization
        void Start()
        {
            _state=GetComponent<AgentAI>()._state;
        }


        private bool fireEnabled()
        {
            return (_state.NextAttack <= Time.time && _state.AttackTarget != null && _state.CurrentAttackType>=0 && ! _state.Healing);
        }

        // Update is called once per frame
        void Update()
        {
            if (fireEnabled())
            {
                if (!animation.IsPlaying("run"))
                {
                    animation.Blend("run");
                }

                RaycastHit hitInfo = new RaycastHit();
                Vector3 randomized = UnityEngine.Random.insideUnitSphere * _state.attackTypes[_state.CurrentAttackType].aim;

                Vector3 dir = (_state.AttackTarget.transform.position - transform.position + randomized).normalized;
                Ray r = new Ray(transform.position, dir);

                int mask=1<<LayerMask.NameToLayer("Ignore Raycast");
                mask=~mask;
               
                bool hasHit = Physics.Raycast(r, out hitInfo, _state.attackTypes[_state.CurrentAttackType].range,mask);
                Vector3 dest = r.origin + (r.direction * hitInfo.distance);


                GameObject l = GameObject.Instantiate(Resources.Load("RayLine"), transform.position, Quaternion.LookRotation(hitInfo.normal)) as GameObject;
                LineRenderer rend = l.GetComponent<LineRenderer>();
                rend.SetWidth(0.1f, 0.1f);
                rend.SetPosition(0, r.origin);
                rend.SetPosition(1, dest);// _state.AttackTarget.transform.position);

                Debug.DrawLine(r.origin, dest, Color.magenta, 2f, true);
                if (hasHit)
                {

                    AgentAI hittedGuy = hitInfo.collider.gameObject.GetComponent<AgentAI>();
                    if (hittedGuy != null)
                    {

                        Debug.Log("Wounded: " + hitInfo.point);
                        hittedGuy._state.Wound(_state.attackTypes[_state.CurrentAttackType].damage);
                    }
                }
                GameObject.Destroy(l, 0.5f);
                _state.CurrentAttackType = -1;//.NextAttack = Time.time + _state.attackTypes[_state.CurrentAttackType].cooldown;
            }
            else
            {
                if (animation.IsPlaying("run"))
                {
                    animation.Blend("idle");
                }
            }
            
        }
    }
}