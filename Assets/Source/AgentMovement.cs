using UnityEngine;
using System.Collections;
using Pathfinding;

namespace MCP_AI {
public class AgentMovement : MonoBehaviour {

    private Seeker seek;
    private AgentState agentState;
    public Transform target;

//    public Transform movementTarget=null;

    private CharacterController movementController=null;
    private int currentWaypoint=0;

    private Transform lastMovementTarget = null;

    private float minDistanceToWayPoint = 1.5f;
    // Use this for initialization
	void Start () {

        agentState=GetComponent<AgentAI>()._state;
        seek = GetComponent<Seeker>();
        movementController = GetComponent<CharacterController>();
       // StartupNode();


        /*if (agentState.MovementTarget != null){
            movementTarget = agentState.MovementTarget.transform;
       } /*else
        {
            GameObject o = new GameObject();
            Vector3 pos=Random.insideUnitSphere * 25f;
            pos.y = 0f;
            o.transform.position=pos;
            movementTarget = o.transform; 
        }*/
		
		RecalculatePath();
       
	}
	
	public void RecalculatePath(){

        if (agentState.MovementTarget != null)
        {
            target = agentState.MovementTarget;



			agentState.Moving=false;
			lastMovementTarget = agentState.MovementTarget.transform;
			agentState.CurrentPath= ABPath.Construct (gameObject.transform.position, agentState.MovementTarget.position);
        //path.CalculateStep(100);

			seek.StartPath(agentState.CurrentPath, OnPathComplete, -1);
			
		}
	}
   
	private bool MoveEnabled(){
		return (agentState.Moving && ! agentState.Healing && seek.IsDone() && agentState.MovementTarget!=null);
	}	
   
	// Update is called once per frame
	void FixedUpdate () {
        if (agentState.MovementTarget != lastMovementTarget) //ReplanPath
        {
            RecalculatePath();
        }
        if (MoveEnabled())
        {
            if (animation.IsPlaying("idle"))
            {
                animation.Play("walk");
            }

            if (currentWaypoint < agentState.CurrentPath.vectorPath.Count)
            {

                float speed = agentState.Speed;
                if (agentState.NextAttack > Time.time)
                {
                    speed /= 2.0f;
                } 
                if (agentState.Healing)
                {
                    speed = 0f;
                }


                Vector3 dir = (agentState.CurrentPath.vectorPath[currentWaypoint] - transform.position).normalized;
                dir *= speed * Time.fixedDeltaTime;

                transform.rotation = Quaternion.Slerp(
                transform.rotation,
                        Quaternion.LookRotation(new Vector3(dir.x,0f,dir.z)),
                        Time.deltaTime * 10
    );

                //

                movementController.SimpleMove(dir);

                if (Vector3.Distance(transform.position, agentState.CurrentPath.vectorPath[currentWaypoint]) < minDistanceToWayPoint)
                {
                    currentWaypoint++;
                }
            }
            else
            {
                if (animation.IsPlaying("walk"))
                {
                    animation.Play("idle");
                }

            }
        }
	}

    public void OnDisable()
    {
        seek.pathCallback -= OnPathComplete;
    }

    void OnPathComplete(Path p)
    {
        //Debug.Log("Path Length: "+p.GetTotalLength());
        //Debug.Log(agentState.CurrentPath.DebugString(PathLog.Heavy));
		agentState.Moving=true;
		
    } 

    void StartupNode()
    {
        //Get a random node to update
        NNConstraint constr = new NNConstraint();
        constr.graphMask = 1<<0;
        NNInfo node = AstarPath.active.GetNearest(gameObject.transform.position,constr);
        Debug.Log(node.clampedPosition);
        AstarPath.RegisterSafeUpdate(delegate()
        {
            //Move the node a bit
            node.clampedPosition = gameObject.transform.position;//+= (Int3)(Random.insideUnitSphere * 0.1f);

            //Recalculate the area around the node.
            //No functions for updating a single node is available, so we create
            //a bounds object which contains that node and only a small volume around it
            AstarPath.active.UpdateGraphs(new GraphUpdateObject(new Bounds((Vector3)node.node.position, new Vector3(0.1f, 0.1f, 0.1f))));

            //Make sure the above graph update is done now
            AstarPath.active.FlushGraphUpdates();

            //The update area functions of the built-in graphs generally assumes that the position of the node
            //has not changed, so some connection costs might be wrong
            node.node.RecalculateConnectionCosts(true);

        }, true);

        //If you want to make sure the update is called directly instead of
        //at the end of this frame or after a few frames.
        //AstarPath.active.FlushThreadSafeCallbacks ();
    }
}
}
