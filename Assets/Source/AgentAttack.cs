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
        
        private float cooldown = 0.0f;

        // Use this for initialization
        void Start()
        {
            _state=GetComponent<AgentAI>()._state;
            cooldown = Time.time;
        }


        private bool fireEnabled()
        {
            return (cooldown < Time.time && _state.AttackTarget != null && _state.CurrentAttackType>=0);
        }

        // Update is called once per frame
        void Update()
        {
            if (fireEnabled())
            {
                cooldown = Time.time + _state.attackTypes[_state.CurrentAttackType].cooldown; //2sec. cooldown time
                //fireEnabled = false;

                RaycastHit hitInfo = new RaycastHit();
                Vector3 randomized = UnityEngine.Random.insideUnitSphere * _state.attackTypes[_state.CurrentAttackType].aim; //1.1 es el valor de puntería.
                Debug.Log(randomized);
                Vector3 dir=(_state.AttackTarget.transform.position-transform.position+randomized).normalized;
                Ray r = new Ray(transform.position, dir);





                bool hasHit = Physics.Raycast(r, out hitInfo, _state.attackTypes[_state.CurrentAttackType].range);
                Vector3 dest = r.origin + (r.direction * hitInfo.distance);


                GameObject l = GameObject.Instantiate(Resources.Load("RayLine"), transform.position, Quaternion.LookRotation(hitInfo.normal)) as GameObject;
                LineRenderer rend = l.GetComponent<LineRenderer>();
                rend.SetWidth(0.1f, 0.1f);
                rend.SetPosition(0, r.origin);
                rend.SetPosition(1, dest);// _state.AttackTarget.transform.position);
               

              


                Debug.DrawLine(r.origin,  dest, Color.magenta, 2f,true);
                if (hasHit)
                {
                    Debug.Log("HIT: "+hitInfo.point);
                    AgentAI hittedGuy=hitInfo.collider.gameObject.GetComponent<AgentAI>();
                    if (hittedGuy!=null)
                    {

                        Debug.Log("Wounded: " + hitInfo.point);
                        hittedGuy._state.Wound(_state.attackTypes[_state.CurrentAttackType].damage);
                    }
                }
                GameObject.Destroy(l,0.5f);
            }
            /*else
            {
                if (cooldown < Time.time)
                {
                    fireEnabled = true;
                }
            }*/
        }
    }
}