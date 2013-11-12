using UnityEngine;
using System.Collections;

namespace MCP_AI{
public class KeyLocProperty : MonoBehaviour {

    public bool taken = false;

    private GameObject env;

    public void Awake()
    {
        env=GameObject.Find("Environment");
    }

    public void OnTriggerEnter(Collider other)
    {

        if (env.GetComponent<Environment>().attackers.Contains(other.gameObject)) {
            taken = true;
            gameObject.renderer.materials[0].color = new Color(0f,1f,0f,0.5f);
            

        }
    }
}
}