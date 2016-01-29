using UnityEngine;
using System.Collections;

public class Egg : MonoBehaviour
{
	void Start ()
	{
	
	}
	
	void Update ()
	{
		transform.eulerAngles = new Vector3(0, 0, Vector2.Angle(transform.right, GetComponent<Rigidbody2D>().velocity));
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		Destroy(gameObject);
	}
}
