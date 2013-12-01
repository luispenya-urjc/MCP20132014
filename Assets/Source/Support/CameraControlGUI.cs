using UnityEngine;
using System.Collections;

public class CameraControlGUI : MonoBehaviour {

    public Camera[] cams;
    public Camera activeCam;
    void Start()
    {
        cams = GameObject.FindObjectsOfType<Camera>();
        foreach (Camera c in cams)
        {
            if (!c.gameObject.name.Equals("Main Camera"))
            {
                c.enabled = false;
            }
            else
            {
                activeCam = c;
            }
        }
    }

	// Use this for initialization
	void LateUpdate () {
        cams = GameObject.FindObjectsOfType<Camera>();
        foreach (Camera c in cams){
            if (c != activeCam)
            {
                c.enabled = false;
            }
            else
            {
                c.enabled = true;
            }
        }
	}

    public void OnGUI()
    {
        GUI.Box(new Rect(10, 10, 160, (cams.Length*20)+40), "Cameras");
        int step = 0;
        foreach (Camera c in cams)
        {

            string name;
            if (c.gameObject.name.Equals("Main Camera"))
            {
                name = "Main Camera";
            }
            else
            {
                name=c.gameObject.transform.parent.name;
            }
            if (GUI.Button(new Rect(20, 40+step, 140, 20),name ))
            {
                activeCam = c;
                
            }
            step += 20;
        }
    }
	
}
