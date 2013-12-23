using UnityEngine;
using System.Collections;
using RAIN.Core;
using RAIN.Action;
using MCP_AI;

[RAINAction]
public class MoveTo : RAINAction
{
    private AIController control;
    private Transform endPoint;
    public MoveTo()
    {
        actionName = "MoveTo";
    }

    public override void Start(AI ai)
    {
        base.Start(ai);
        control = ai.WorkingMemory.GetItem<AIController>("controller");
        endPoint = ai.WorkingMemory.GetItem<Transform>("movementTarget");
        if (!control.GetState().Moving)
        {
            Debug.Log("Move To: " + endPoint);
            control.SetMovementTarget(endPoint);
        }
    }

    public override ActionResult Execute(AI ai)
    {
        
        if (Vector3.Distance(ai.Body.transform.position, endPoint.position) < 0.3)
        {
            return ActionResult.SUCCESS;
        }
        else
        {
            return ActionResult.RUNNING;
        }
        
    }

    public override void Stop(AI ai)
    {
        base.Stop(ai);
    }
}
