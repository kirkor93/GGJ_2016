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
	bool canMove = true;

	void Start ()
	{
        sequenceMode = false;
	}
	
	void Update ()
	{
		FixRotation();
		ShotCooldown();
	}

	public override void OnActionStart()
	{
		if (!shot && (dir.x != 0 && dir.y != 0) && !sequenceMode)
		{
			transform.localScale = new Vector3(-1, 1, 1);
			GetComponent<SpriteRenderer>().sprite = cannon;
			GameObject tempEgg = Instantiate(egg, transform.position, Quaternion.identity) as GameObject;
			tempEgg.GetComponent<Rigidbody2D>().AddForce(dir * power, ForceMode2D.Impulse);
			shot = true;
		}
	}

	public override void OnActionRelease()
	{
        if (!sequenceMode)
        {
            transform.localScale = new Vector3(1, 1, 1);
            GetComponent<SpriteRenderer>().sprite = chickenSprite;
        }
	}

	public override void OnMove(Vector2 direction)
	{
		if(canMove)
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

	public void BlockMovement()
	{
		canMove = false;
	}

	public void UnblockMovement()
	{
		canMove = true;
	}
}
