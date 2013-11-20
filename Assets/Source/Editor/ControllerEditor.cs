using UnityEditor;
using UnityEngine;
using System.Collections;
[CustomEditor(typeof(MCP_AI.Environment))]

public class ControllerEditor : Editor {



    MonoScript aS = null;
    public override void OnInspectorGUI()
    {
        MCP_AI.Environment myTarget = (MCP_AI.Environment)target;
        myTarget.attackerControllerType =(MCP_AI.AgentAI.OPTIONS)EditorGUILayout.EnumPopup("Attacker Controller Type",myTarget.attackerControllerType);
        if (myTarget.attackerControllerType == MCP_AI.AgentAI.OPTIONS.CustomAI)
        {
            aS=EditorGUILayout.ObjectField("Controller Class",aS, typeof(MonoScript),false) as MonoScript;
            if (aS!=null && aS.GetClass().BaseType.Equals(typeof(MCP_AI.AIController)))
            {
                
                myTarget.attackController = aS.GetClass();
            }
        }
        myTarget.defenderControllerType = (MCP_AI.AgentAI.OPTIONS)EditorGUILayout.EnumPopup("Defender Controller Type", myTarget.defenderControllerType);
    }
}
