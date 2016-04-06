using UnityEngine;

public class Chicken : Player
{
	public GameObject egg;
	public Sprite cannon;
	public Sprite chickenSprite;
	public float power;
	public AudioClip walkClip;
	public AudioClip shootClip;
	public AudioClip jumpClip1;
	public AudioClip jumpClip2;
	public AudioClip jumpClip3;
//	Vector2 dir;
	AudioSource _audioSource;
	float cooldown = 0.3f;
	bool shot = false;
	bool canMove = true;
	bool jumping = false;

	void Start ()
	{
		_audioSource = GetComponent<AudioSource>();
        SequenceMode = false;
        Water = false;
	}
	
	void Update ()
	{
//		FixRotation();
		ShotCooldown();
		UpdateJumpingAnimation();
	}

	public override void OnActionStart(Vector2 direction)
	{
		if (!shot && (direction.x != 0 && direction.y != 0) && !SequenceMode)
		{
			_audioSource.clip = shootClip;
			_audioSource.loop = false;
			_audioSource.Play();
			transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
			GetComponent<SpriteRenderer>().sprite = cannon;
			GameObject tempEgg = Instantiate(egg, transform.position, Quaternion.identity) as GameObject;
			Debug.Log("JAJO!!!");
			tempEgg.GetComponent<Rigidbody2D>().AddForce(direction * power, ForceMode2D.Impulse);
			shot = true;
			GetComponent<Animator>().SetBool("shooting", shot);
		}
	}

	public override void OnActionRelease(Vector2 direction)
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
//		dir = direction;

		if (direction.x > -0.1f && direction.x < 0.1f)
		{
			GetComponent<Animator>().SetBool("moving", false);
			if (_audioSource.clip == walkClip)
				_audioSource.Stop();
		}
		else if (!jumping)
		{
			GetComponent<Animator>().SetBool("moving", true);
			if (_audioSource.clip != walkClip && !_audioSource.isPlaying)
				_audioSource.clip = walkClip;

			if (!_audioSource.loop && !_audioSource.isPlaying)
				_audioSource.loop = true;

			if(!_audioSource.isPlaying)
				_audioSource.Play();
		}
		else if(jumping)
		{
			GetComponent<Animator>().SetBool("moving", false);
			if (_audioSource.clip == walkClip)
				_audioSource.Stop();
		}

        //fixing rotations
        if (direction.x > 0 && !shot)
            transform.localScale = new Vector3(1, 1, 1);
        else if (direction.x < 0 && !shot)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    protected override void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Level"))
        {
            UnblockMovement(); 
        }
        base.OnCollisionEnter2D(other);
    }

	public override void OnJumpStart()
	{
		if (_audioSource.clip == walkClip)
		{
			_audioSource.loop = false;
			int rand = UnityEngine.Random.Range(0, 3);
			switch (rand)
			{
				case 0: _audioSource.clip = jumpClip1; break;
				case 1: _audioSource.clip = jumpClip2; break;
				case 2: _audioSource.clip = jumpClip3; break;
			}
		}
		if (!_audioSource.isPlaying)
			_audioSource.Play();
		base.OnJumpStart();
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

//	void FixRotation()
//	{
//		if (dir.x > 0 && !shot)
//			transform.localScale = new Vector3(1, 1, 1);
//		else if (dir.x < 0 && !shot)
//			transform.localScale = new Vector3(-1, 1, 1);
//	}

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
