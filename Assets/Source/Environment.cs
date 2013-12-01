using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace MCP_AI
{
	public class Environment: MonoBehaviour
	{

        public enum FACTIONS
        {
            ATTACKER=1,
            DEFENDER=2,
        }

		public float timer;
        public float currentTime=0f;

        private AgentAI.OPTIONS _attackerControllerType;

        public AgentAI.OPTIONS AttackerControllerType
        {
            get { return _attackerControllerType; }
            set { _attackerControllerType = value; }
        }

        public string _attackController;

        public string AttackController
        {
            get { return _attackController; }
            set { _attackController = value; }
        }
        private AgentAI.OPTIONS _defenderControllerType;

        public AgentAI.OPTIONS DefenderControllerType
        {
            get { return _defenderControllerType; }
            set { _defenderControllerType = value; }
        }
        public string _defendController;

        public string DefendController
        {
            get { return _defendController; }
            set { _defendController = value; }
        }

        private GameObject GenerateCombatant(string name, int role, string areas, FACTIONS faction)
        {
            Terrain t = GameObject.Find("Terrain").GetComponent<Terrain>();
            
            
            GameObject[] s = GameObject.FindGameObjectsWithTag(areas);
            int i = UnityEngine.Random.Range(0, s.Length);
            Vector3 pos = s[i].transform.position + (UnityEngine.Random.insideUnitSphere * (s[i].transform.localScale.x / 2.0f));
   
            pos.y = 1f + t.SampleHeight(pos);
            GameObject o = Instantiate(Resources.Load("AIActor"), pos, Quaternion.identity) as GameObject;

            o.name = name;
            
            switch(faction ){
                case FACTIONS.ATTACKER:
                    Debug.Log("Setting up controller (" + _attackController+")");
                    o.GetComponent<MCP_AI.AgentAI>()._controller = ScriptableObject.CreateInstance(_attackController) as AIController;
                    o.GetComponent<MCP_AI.AgentAI>()._controller.faction = FACTIONS.ATTACKER;
                    break;
                case FACTIONS.DEFENDER:
                    Debug.Log("Setting up controller " + _defendController);
                    o.GetComponent<MCP_AI.AgentAI>()._controller = ScriptableObject.CreateInstance(_defendController) as AIController;
                    o.GetComponent<MCP_AI.AgentAI>()._controller.faction = FACTIONS.DEFENDER;
                    break;
                default:
                    throw new Exception("ERROR controller initialization failed");
            } 

            o.GetComponent<MCP_AI.AgentAI>()._state = ScriptableObject.CreateInstance<AgentState>();
            o.GetComponent<MCP_AI.AgentAI>()._state.SetRole(role);// new MCP_AI.AgentState(role);
            

            return o;
        }

        public List<GameObject> attackers;
        public List<GameObject> defenders;


        private void GenerateTeam(string team,List<GameObject> list,string areas, FACTIONS faction) 
        {

            GameObject t = new GameObject(team);
            t.transform.parent = gameObject.transform;

            GameObject o;
            o = GenerateCombatant(team+"-Explorer", MCP_AI.AgentState.EXPLORER, areas,faction);
            o.transform.parent = t.transform;

            list.Add(o);
            o = GenerateCombatant(team + "-Sniper", MCP_AI.AgentState.SNIPER, areas,faction);
            o.transform.parent = t.transform;
            list.Add(o);
            o = GenerateCombatant(team + "-Soldier-1", MCP_AI.AgentState.SOLDIER, areas,faction);
            o.transform.parent = t.transform;
            list.Add(o);
            o = GenerateCombatant(team + "-Soldier-2", MCP_AI.AgentState.SOLDIER, areas,faction);
            o.transform.parent = t.transform;
            list.Add(o);
            o = GenerateCombatant(team + "-Support", MCP_AI.AgentState.SUPPORT, areas,faction);
            o.transform.parent = t.transform;
            list.Add(o);
        
        }

        void Start()
        {
            attackers=new List<GameObject>();
            defenders=new List<GameObject>();
            GenerateTeam("Attackers",attackers,"spawnarea-attack",FACTIONS.ATTACKER);
            GenerateTeam("Defenders",defenders,"spawnarea-defend",FACTIONS.DEFENDER);
                    
            GameObject[] defendareas=GameObject.FindGameObjectsWithTag("spawnarea-defend");
            GameObject p=new GameObject("KeyLocs");
            p.transform.parent=gameObject.transform;
            for (int x=0; x<defendareas.Length; x++){
                Vector3 pos = defendareas[x].transform.position + (UnityEngine.Random.insideUnitSphere * (defendareas[x].transform.localScale.x / 4.0f));
                pos.y = 1f;
                GameObject o = Instantiate(Resources.Load("KeyLoc"), pos, Quaternion.identity) as GameObject;
                o.name = "KeyLoc-" + x;
                o.transform.parent = p.transform;
            }
			timer=Time.time+240f; //4minutos
			
        }
		
		private bool hasFinalLog=false;
		private string finalLog;
		void OnGUI() {
			if (hasFinalLog){
				 GUI.TextArea(new Rect(50, 50, 400, 200), finalLog);
			}
		}
		
		void Update() {
            currentTime = Time.time;
                
			if (defenders.Count <= 0 || attackers.Count <=0 || currentTime > timer){
				
				Time.timeScale=0;
                hasFinalLog = true;
                finalLog = "TIME: " + currentTime + "\n";
			}
		}

        void AgentDie(GameObject o)
        {
            attackers.Remove(o);
            defenders.Remove(o);
        }
	}
}
