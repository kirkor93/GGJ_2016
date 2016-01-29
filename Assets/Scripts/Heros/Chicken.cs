using UnityEngine;
using System.Collections;
using Assets.Scripts;
using System;

public class Chicken : Player
{
	public GameObject egg;
	public Sprite cannon;
	public Sprite chickenSprite;
	public float power;
	Vector2 dir;

	float cooldown = 0.5f;
	bool shot = false;

	void Start ()
	{
	
	}
	
	void Update ()
	{
		FixRotation();
		ShotCooldown();
	}

	public override void OnActionStart()
	{
		if (!shot)
		{
			GetComponent<SpriteRenderer>().sprite = cannon;
			GameObject tempEgg = Instantiate(egg, transform.position, Quaternion.identity) as GameObject;
			tempEgg.GetComponent<Rigidbody2D>().AddForce(dir * power, ForceMode2D.Impulse);
			shot = true;
		}
	}

	public override void OnActionRelease()
	{
		GetComponent<SpriteRenderer>().sprite = chickenSprite;
		throw new NotImplementedException();
	}

	public override void OnMove(Vector2 direction)
	{
		base.OnMove(direction);
		dir = direction;
	}

	void FixRotation()
	{
		if (dir.x > 0)
			GetComponent<SpriteRenderer>().flipX = false;
		else
			GetComponent<SpriteRenderer>().flipX = true;
	}

	void ShotCooldown()
	{
		if(shot)
		{
			if(cooldown > 0)
			{
				cooldown -= Time.deltaTime;
			}
			else
			{
				cooldown = 0.5f;
				shot = false;
			}
		}
	}
}
