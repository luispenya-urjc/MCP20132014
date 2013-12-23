using UnityEngine;
using System.Collections;
using MCP_AI;
using System.Collections.Generic;

public class RandomAI : AIController
{

    public static float PROB_CHANGE = 0.05f;
    public static float ATT_RADIUS = 20f;

    public KeyLocProperty movementTarget;
    public GameObject attackTarget;
    public int attackSelected=-1;
    


    private MCP_AI.Environment environment;
    public override void Init(GameObject agent)
    {
        base.Init(agent);
         environment = GetEnvironment();
    
    }  

    public override void RefreshAI()
    {
        if (movementTarget == null || movementTarget.taken)
        {
            SelectMovementLocation();
        }
        if (Vector3.Distance(FindClosestEnemy().transform.position, _agent.transform.position) < ATT_RADIUS)
        {
            if (attackSelected == -1 || Random.value < PROB_CHANGE)
            {
                attackSelected = Random.Range(0, 3);
                SetAttack(attackSelected);
            }
            if (attackTarget == null || Random.value < PROB_CHANGE)
            {
                attackTarget = FindClosestEnemy();
                SetAttackTarget(attackTarget);
            }
        }
        else
        {
            attackSelected = -1;
            SetAttack(attackSelected);
        }
    }

    private void SelectMovementLocation()
    {
        KeyLocProperty[] locs= environment.GetComponentsInChildren<KeyLocProperty>();
        int l;
        
        do {
            l=Random.Range(0, locs.Length);
        } while (locs[l].taken);
        
        movementTarget = locs[l];
        this.SetMovementTarget(movementTarget.transform);
    }


    internal List<GameObject> GetFriends()
    {
        if (this.faction == Environment.FACTIONS.ATTACKER)
            return environment.attackers;
        else
            return environment.defenders;
    }

    internal List<GameObject> GetFoes()
    {
        if (this.faction != Environment.FACTIONS.ATTACKER)
            return environment.attackers;
        else
            return environment.defenders;
    }

    internal GameObject FindClosestEnemy()
    {
        Vector3 orig = _agent.transform.position;
        if (GetFoes().Count > 0)
        {
            GameObject res = GetFoes()[0];


            float dist = Vector3.Distance(orig, res.transform.position);
            float aux;
            foreach (GameObject o in GetFoes())
            {
                aux = Vector3.Distance(orig, o.transform.position);
                if (dist > aux)
                {
                    dist = aux;
                    res = o;
                }
            }
            return res;
        }
        else
        {
            return null;
        }
    }
}
