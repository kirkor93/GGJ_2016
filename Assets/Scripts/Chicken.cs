using UnityEngine;
using System.Collections;

public class Chicken : MonoBehaviour
{
	public GameObject egg;

	void Start ()
	{
	
	}
	
	void Update ()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			Shoot(new Vector2(1, 0.5f));
		}
	}

	void Shoot(Vector2 dir)
	{
		GameObject tempEgg = Instantiate(egg, transform.position, Quaternion.identity) as GameObject;
		tempEgg.GetComponent<Rigidbody2D>().AddForce(dir * 500, ForceMode2D.Impulse);
	}
}
