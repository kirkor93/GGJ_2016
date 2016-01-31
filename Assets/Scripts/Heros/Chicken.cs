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
	public AudioClip walkClip;
	Vector2 dir;

	float cooldown = 0.3f;
	bool shot = false;
	bool canMove = true;
	bool jumping = false;

	void Start ()
	{
        SequenceMode = false;
        Water = false;
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
			transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
			GetComponent<SpriteRenderer>().sprite = cannon;
			GameObject tempEgg = Instantiate(egg, transform.position, Quaternion.identity) as GameObject;
			Debug.Log("JAJO!!!");
			tempEgg.GetComponent<Rigidbody2D>().AddForce(dir * power, ForceMode2D.Impulse);
			shot = true;
			GetComponent<Animator>().SetBool("shooting", shot);
		}
	}

	public override void OnActionRelease()
	{
        if (!SequenceMode)
        {
            //transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
            GetComponent<SpriteRenderer>().sprite = chickenSprite;
        }
	}

	public override void OnMove(Vector2 direction)
	{
		if(canMove && !shot)
			base.OnMove(direction);
		dir = direction;

		if (dir.x > -0.1f && dir.x < 0.1f)
		{
			GetComponent<Animator>().SetBool("moving", false);
			if (GetComponent<AudioSource>().clip == walkClip)
				GetComponent<AudioSource>().Stop();
		}
		else if (!jumping)
		{
			GetComponent<Animator>().SetBool("moving", true);
			if (GetComponent<AudioSource>().clip != walkClip)
				GetComponent<AudioSource>().clip = walkClip;

			if (!GetComponent<AudioSource>().loop)
				GetComponent<AudioSource>().loop = true;

			if(!GetComponent<AudioSource>().isPlaying)
				GetComponent<AudioSource>().Play();
		}
		else if(jumping)
		{
			GetComponent<Animator>().SetBool("moving", false);
			if (GetComponent<AudioSource>().clip == walkClip)
				GetComponent<AudioSource>().Stop();
		}

	}

    protected override void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Level"))
        {
            UnblockMovement(); 
        }
        base.OnCollisionEnter2D(other);
    }

	void UpdateJumpingAnimation()
	{
		if (!Water)
		{
			if (Flying)
			{
				jumping = true;
				GetComponent<Animator>().SetBool("jumping", jumping);
			}
			else
			{
				jumping = false;
				GetComponent<Animator>().SetBool("jumping", jumping);
			}
		}
		else
		{
			jumping = true;
			GetComponent<Animator>().SetBool("jumping", jumping);
		}
	}

	void FixRotation()
	{
		if (dir.x > 0 && !shot)
			transform.localScale = new Vector3(1, 1, 1);
		else if (dir.x < 0 && !shot)
			transform.localScale = new Vector3(-1, 1, 1);
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
				cooldown = 0.3f;
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
