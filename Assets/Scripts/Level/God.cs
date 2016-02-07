using UnityEngine;
using System.Collections;
using DG.Tweening;

public class God : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("hero") && col.gameObject.GetComponent<Player>().Attack)
        {
			GetComponent<AudioSource>().Play();
            StartCoroutine("FuckIT");
            transform.DOScale(Vector3.zero, 0.25f);
        }
    }

    IEnumerator FuckIT()
    {
        yield return new WaitForSeconds(0.25f);

        gameObject.SetActive(false);
    }
}
