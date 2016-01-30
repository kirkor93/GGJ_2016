using UnityEngine;
using System.Collections;
using DG.Tweening;

public class Water : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("hero"))
        {
           // if ( col.gameObject.name.Contains("Goat") )
                //col.gameObject.GetComponent<Animation>().cl

            if (col.transform.localScale.x > 0)
                col.transform.DOMoveX(col.transform.position.x + col.gameObject.GetComponent<SpriteRenderer>().bounds.size.x * 3, 2);
            if (col.transform.localScale.x < 0)
                col.transform.DOMoveX(col.transform.position.x - col.gameObject.GetComponent<SpriteRenderer>().bounds.size.x * 3, 2);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("hero"))
        {
            col.transform.DOKill();
        }
    }
}
