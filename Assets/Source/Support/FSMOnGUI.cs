using UnityEngine;
using System.Collections;


public class FSMOnGUI : MonoBehaviour {

    public GameObject environment_obj;
    private MCP_AI.Environment environment;
    void Start()
    {
        if (environment_obj != null)
            environment = environment_obj.GetComponent<MCP_AI.Environment>();
    }

    /*void OnGUI()
    {
        // Make a background box
        GUI.Box(new Rect(10, 10, 180, 170), "Simulated Events");

        
        if (GUI.Button(new Rect(20, 40, 120, 20), "Simulate Wound"))
        {
            environment.Wounds++;
        }

        if (GUI.Button(new Rect(20, 70, 120, 20), "Simulate Sound"))
        {
            environment.Sound=!environment.Sound;
        }

        if (GUI.Button(new Rect(20, 100, 120, 20), "Simulate View"))
        {
            environment.Show = !environment.Show;
        }

        if (GUI.Button(new Rect(20, 130, 120, 20), "Simulate Dead"))
        {
            environment.Alive = !environment.Alive;
        }

        Rect placeHolder=new Rect(10, Screen.height - 150, 150, 140);
        GUI.TextArea(placeHolder,environment.ToString());
        if (GUI.changed)
        {
            
            Debug.Log(environment.ToString()+" ... "+ placeHolder);
           
        }
    }*/
}
