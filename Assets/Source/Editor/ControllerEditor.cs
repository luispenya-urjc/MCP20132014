using UnityEditor;
using System.Collections;
[CustomEditor(typeof(MCP_AI.AgentAI))]

public class ControllerEditor : Editor {

    
	

    public override void OnInspectorGUI()
    {
        MCP_AI.AgentAI myTarget = (MCP_AI.AgentAI)target;
        myTarget.controllerType =(MCP_AI.AgentAI.OPTIONS)EditorGUILayout.EnumPopup("Controller Type",myTarget.controllerType);
            
    }
}
