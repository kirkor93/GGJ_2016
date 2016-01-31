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
             if (!transform.GetChild(0).GetComponent<SpriteRenderer>().enabled)
             {
				if(!transform.GetChild(0).GetComponent<SpriteRenderer>().enabled)
					GetComponent<AudioSource>().Play();

                foreach (Transform spike in transform)
                {
                    spike.GetComponent<SpriteRenderer>().enabled = true;
                }
                
                 col1.enabled = false;
             }
             else
             {
                 if (col.gameObject.name.Contains("Goat"))
                 {
                     col.gameObject.GetComponent<Player>().HitPoints = 0;
                 }
             }
         }
    }
}
