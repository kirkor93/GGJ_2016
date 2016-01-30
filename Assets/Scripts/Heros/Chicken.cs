﻿using UnityEngine;
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
        SequenceMode = false;
	}
	
	void Update ()
	{
		FixRotation();
		ShotCooldown();
		UpdateJumpingAnimation();
	}

	public override void OnActionStart()
	{
		if (!shot && (dir.x != 0 && dir.y != 0) && !SequenceMode)
		{
			transform.localScale = new Vector3(-1, 1, 1);
			GetComponent<SpriteRenderer>().sprite = cannon;
			GameObject tempEgg = Instantiate(egg, transform.position, Quaternion.identity) as GameObject;
			tempEgg.GetComponent<Rigidbody2D>().AddForce(dir * power, ForceMode2D.Impulse);
			shot = true;
			GetComponent<Animator>().SetBool("shooting", shot);
		}
	}

	public override void OnActionRelease()
	{
        if (!SequenceMode)
        {
            transform.localScale = new Vector3(1, 1, 1);
            GetComponent<SpriteRenderer>().sprite = chickenSprite;
        }
	}

	public override void OnMove(Vector2 direction)
	{
		if(canMove && !shot)
			base.OnMove(direction);
		dir = direction;

		if(dir.x > -0.1f && dir.x < 0.1f)
			GetComponent<Animator>().SetBool("moving", false);
		else
			GetComponent<Animator>().SetBool("moving", true);

	}

	void UpdateJumpingAnimation()
	{
		if(Flying)
			GetComponent<Animator>().SetBool("jumping", true);
		else
			GetComponent<Animator>().SetBool("jumping", false);
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
				GetComponent<Animator>().SetBool("shooting", shot);
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
