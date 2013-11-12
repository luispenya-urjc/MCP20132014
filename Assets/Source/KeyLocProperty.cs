using UnityEngine;
using System.Collections;

public class KeyLocProperty : MonoBehaviour {

    public bool taken = false;



    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) {
            taken = true;
            gameObject.renderer.materials[0].color = new Color(0f,1f,0f,0.5f);
            

        }
    }
}
