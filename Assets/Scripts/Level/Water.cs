using UnityEngine;
using System.Collections;
using DG.Tweening;
using Assets.Scripts;

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
            col.gameObject.GetComponent<Player>().Water = true;

            if (col.transform.localScale.x >= 0)
                col.transform.DOMoveX(col.transform.position.x + GetComponent<SpriteRenderer>().bounds.size.x ,  1f).SetEase(Ease.OutQuad);
            else if (col.transform.localScale.x < 0 )
                col.transform.DOMoveX(col.transform.position.x - GetComponent<SpriteRenderer>().bounds.size.x, 1f).SetEase(Ease.OutQuad);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("hero"))
        {
            col.transform.DOKill();

            col.gameObject.GetComponent<Player>().Water = false;
        }
    }
}
