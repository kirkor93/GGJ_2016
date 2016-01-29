using UnityEngine;
using System.Collections;

public class Egg : MonoBehaviour
{
	void Start ()
	{
	
	}

	void Update()
	{
		UpdateRotation();
	}

	void UpdateRotation()
	{
		Vector2 dir = GetComponent<Rigidbody2D>().velocity;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		Destroy(gameObject);
	}
}
