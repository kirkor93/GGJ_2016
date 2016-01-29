using UnityEngine;
using System.Collections;
using DG.Tweening;

public class HeroTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
        transform.DOMoveX(transform.position.x+100,5);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
