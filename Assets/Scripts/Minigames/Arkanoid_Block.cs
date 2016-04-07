using UnityEngine;
using System.Collections;

public class Arkanoid_Block : MonoBehaviour
{
	public bool dead;

	private float endtimer = 0.5f;

	void Start ()
	{
	
	}
	
	void Update ()
	{
		Die();
	}

	void Die()
	{
		if(dead)
		{
			if(endtimer > 0)
			{
				endtimer -= Time.deltaTime;
				transform.localScale = new Vector3(endtimer * 2, endtimer * 2, endtimer * 2);
			}
			else
			{
				Destroy(gameObject);
			}
		}
	}
}
