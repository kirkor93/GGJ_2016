using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class Hole : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("hero"))
        {
            col.gameObject.GetComponent<Player>().HitPoints = 0;
        }
    }
}
