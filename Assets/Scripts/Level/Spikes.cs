using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class Spikes : MonoBehaviour {

    public Collider2D col1, col2;

	// Use this for initialization
	void Start () {
        transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D col)
    {
         if (col.gameObject.layer == LayerMask.NameToLayer("hero") )
         {
             if (col1)
             {
				if(!transform.GetChild(0).GetComponent<SpriteRenderer>().enabled)
					GetComponent<AudioSource>().Play();
                 transform.GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
                 col1.enabled = false;
             }
             else
             {
                 col.gameObject.GetComponent<Player>().HitPoints = 0;
             }
         }
    }
}
