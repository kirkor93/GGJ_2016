using UnityEngine;

public class Egg : MonoBehaviour
{
	float destroyTimer = 0.25f;
	bool destroyed = false;

	void Start ()
	{
		GetComponent<Animator>().enabled = false;
	}

	void Update()
	{
		UpdateRotation();
		DestroyEgg();
	}

	void UpdateRotation()
	{
		Vector2 dir = GetComponent<Rigidbody2D>().velocity;
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
	}

	void OnCollisionEnter2D(Collision2D coll)
	{
		if (!destroyed)
		{
			GetComponent<AudioSource>().Play();
			GetComponent<Animator>().enabled = true;
			GetComponent<Animator>().Play("egg_destroy", 0, 0);
			destroyed = true;
			GetComponent<Rigidbody2D>().isKinematic = true;
		}
	}

	void DestroyEgg()
	{
		if(destroyed)
		{
			if(destroyTimer > 0)
			{
				destroyTimer -= Time.deltaTime;
			}
			else
			{
				destroyTimer = 0.25f;
				destroyed = false;
				Destroy(gameObject);				
			}
		}
	}
}
