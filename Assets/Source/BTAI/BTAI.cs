using UnityEngine;
using System.Collections;
using MCP_AI;
using RAIN.Minds;
using RAIN.BehaviorTrees;

namespace BT
{
    public class BTAI : AIController
    {


        private BasicMind mind;

        private RAIN.Memory.BasicMemory memory;

        private RAIN.Core.AI rainAI = new RAIN.Core.AI();

        public static BTAsset tree = Resources.Load<BTAsset>("BehaviorTrees/test");

        private Environment environment;
        public override void Init(GameObject agent)
        {
            base.Init(agent);

            memory=new RAIN.Memory.BasicMemory();
            memory.AIInit(rainAI);
            memory.SetItem<AIController>("controller",this);

            environment = GetEnvironment();

            mind = new BasicMind();
            mind.AIInit(rainAI);

            mind.AI.Body = agent;
            Debug.Log("SettingUP BasicMind: " + tree.name);

            mind.SetBehavior(tree, null);

            rainAI.WorkingMemory = memory;
            
        }

        public override void RefreshAI()
        {
            mind.Think();
        }
    }
}