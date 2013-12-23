using UnityEditor;
using UnityEngine;
using System.Collections;
using System;
[CustomEditor(typeof(MCP_AI.Environment))]

public class ControllerEditor : Editor {


    
    private MonoScript aS = null;
    private MonoScript dS = null;

    

    private string GetClassName(MCP_AI.AgentAI.OPTIONS option,MonoScript ms)
    {
        switch (option)
        {
            case MCP_AI.AgentAI.OPTIONS.FSMAI:
                return typeof(FSM.FSMAI).Name;
                
            case MCP_AI.AgentAI.OPTIONS.BTAI:
                return typeof(BT.BTAI).Name;
                
            case MCP_AI.AgentAI.OPTIONS.RandomAI:
                return typeof(RandomAI).Name;
            case MCP_AI.AgentAI.OPTIONS.CustomAI:
                return ms.GetClass().Name;


        }
        return null;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();


        if (Application.isPlaying)
            return;

        MCP_AI.Environment myTarget = (MCP_AI.Environment)target;
        myTarget.AttackerControllerType =(MCP_AI.AgentAI.OPTIONS)EditorGUILayout.EnumPopup("Attacker Controller Type",myTarget.AttackerControllerType);
        if (myTarget.AttackerControllerType == MCP_AI.AgentAI.OPTIONS.CustomAI)
        {
            aS = EditorGUILayout.ObjectField("Controller Class", aS, typeof(MonoScript), false) as MonoScript;
            if (aS != null && aS.GetClass().BaseType.Equals(typeof(MCP_AI.AIController)))
            {
                myTarget.AttackController = aS.GetClass().Name;
            }
        }
        else
        {
            myTarget.AttackController = GetClassName(myTarget.AttackerControllerType, null);
        }

        myTarget.DefenderControllerType = (MCP_AI.AgentAI.OPTIONS)EditorGUILayout.EnumPopup("Defender Controller Type", myTarget.DefenderControllerType);
        if (myTarget.DefenderControllerType == MCP_AI.AgentAI.OPTIONS.CustomAI)
        {
            dS = EditorGUILayout.ObjectField("Controller Class", dS, typeof(MonoScript), false) as MonoScript;
            if (dS != null && dS.GetClass().BaseType.Equals(typeof(MCP_AI.AIController)))
            {

                myTarget.DefendController = dS.GetClass().Name;
            }
        }
        else
        {
            myTarget.DefendController = GetClassName(myTarget.DefenderControllerType, null);
        }
    }
}
