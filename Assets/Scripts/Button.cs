using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnetr2D(Collider2D col)
    {
        if ( col.gameObject.layer == LayerMask.NameToLayer("hero") )
        {
            Debug.Log("Jestem w środku");
        }
    }
}
