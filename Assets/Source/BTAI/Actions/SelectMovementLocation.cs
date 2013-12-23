using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using RAIN.Core;
using RAIN.Action;
using MCP_AI;

    
namespace BT
{
[RAINAction]
public class SelectMovementLocation : RAINAction {
    private AIController control;

        public SelectMovementLocation()
        {
            actionName = "SelectMovementLocation";
        }

        public override void Start(AI ai)
        {
            
            base.Start(ai);
            control = ai.WorkingMemory.GetItem<AIController>("controller");
        }

        public override ActionResult Execute(AI ai)
        {
            Environment.FACTIONS f=control.faction;
            Debug.Log("Execute Movement Action: "+f);
            ai.WorkingMemory.SetItem<Transform>("movementTarget",SelectKeyLoc());
            return ActionResult.SUCCESS;
        }

        public override void Stop(AI ai)
        {
            base.Stop(ai);
        }

        private Transform SelectKeyLoc()
        {
            KeyLocProperty[] locs = control.GetEnvironment().GetComponentsInChildren<KeyLocProperty>();
            int l;

            do
            {
                l = Random.Range(0, locs.Length);
            } while (locs[l].taken);


            return locs[l].transform;
        }
    }
}