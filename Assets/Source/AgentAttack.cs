using UnityEngine;
using System.Collections;


namespace MCP_AI
{
    public class AgentAttack : MonoBehaviour
    {

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
            return (cooldown < Time.time && _state.AttackTarget != null);
        }

        // Update is called once per frame
        void Update()
        {
            if (fireEnabled())
            {
                cooldown = Time.time + 2f; //2sec. cooldown time
                //fireEnabled = false;

                RaycastHit hitInfo = new RaycastHit();
                Ray r = new Ray(transform.position, (_state.AttackTarget.transform.position-transform.position).normalized);


                

                
                bool hasHit = Physics.Raycast(r, out hitInfo, 20f);

              /*  GameObject l = GameObject.Instantiate(Resources.Load("RayLine"), transform.position, Quaternion.LookRotation(hitInfo.normal)) as GameObject;
                LineRenderer rend = l.GetComponent<LineRenderer>();
                rend.SetWidth(0.1f, 0.1f);
                rend.SetPosition(0, r.origin);
                rend.SetPosition(1, hitInfo.point);// _state.AttackTarget.transform.position);
               */

                Vector3 dest = r.origin + (r.direction * hitInfo.distance);


                Debug.DrawLine(r.origin,  dest, Color.magenta, 2f,true);
                if (hasHit)
                {
                    Debug.Log("HIT: "+hitInfo.point);
                    if (hitInfo.collider.gameObject.tag.Equals("Player"))
                    {

                        Debug.Log("Wounded: " + hitInfo.point);
                        hitInfo.collider.gameObject.GetComponent<AgentAI>()._state.Wound(3);
                    }
                }
                //GameObject.Destroy(l,1f);
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