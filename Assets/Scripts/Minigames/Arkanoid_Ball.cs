using UnityEngine;
using System.Collections;

public class Arkanoid_Ball : MonoBehaviour
{
	public Arkanoid_GameController game;
	public GameObject player1Platform;
	public GameObject player2Platform;
   public bool started = false;
	public float speed = 1000;

	Rigidbody2D _rigidbody;
	float platformLength = 234;
	float blockLength = 100;

    public Rigidbody2D Rigid
    {
        get { return _rigidbody; }
    }


	void Start ()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
	}
	
	void Update ()
    {
	
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Deadly")
        {
			game.Die ();
        }
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
      if(coll.gameObject.tag == "Safe")
      {
			FixVelocity();
      }

        if(coll.gameObject.tag == "Player")
      {
			if (coll.gameObject.name == "player1_platform")
				Bounce (player1Platform);
			else if (coll.gameObject.name == "player2_platform")
				Bounce (player2Platform);
      }

		if(coll.gameObject.tag == "Obstacle")
		{
			game.currentBlocks.Remove(coll.gameObject);
			coll.gameObject.GetComponent<Arkanoid_Block>().dead = true;
			BounceBlock(coll.gameObject);
			//Destroy(coll.gameObject);
			//_rigidbody.velocity = _rigidbody.velocity.normalized * speed;
		}
	}



	//void OnCollisionExit2D(Collision2D coll)
	//{
	//	if (coll.gameObject.tag == "Safe" || coll.gameObject.tag == "Obstacle") 
	//	{
	//		_rigidbody.velocity = _rigidbody.velocity.normalized * speed;
	//	}

		
	//}

	void FixVelocity()
	{
		Vector2 velocity = _rigidbody.velocity;
		if (velocity.y < 100 && velocity.y > -100)
		{
			if ((velocity.y < 0 && velocity.x < 0) || (velocity.y >= 0 && velocity.x > 0))
				GetComponent<Rigidbody2D>().velocity = game.RotateVector2(velocity, 1);
			else if ((velocity.y >= 0 && velocity.x < 0) || (velocity.y < 0 && velocity.x > 0))
				GetComponent<Rigidbody2D>().velocity = game.RotateVector2(velocity, -1);
		}
	}

	void Bounce(GameObject platform)
	{
		float y = 0.0f;
		y = (transform.position.y - platform.transform.position.y)/platformLength/2;

		if (platform == player1Platform) 
		{
			Vector2 dir = new Vector2 (1, y).normalized;
			_rigidbody.velocity = dir * speed;
		} 
		else if (platform == player2Platform) 
		{
			Vector2 dir = new Vector2 (-1, y).normalized;
			_rigidbody.velocity = dir * speed;
		}
		//Debug.Log ("Velocity: " + _rigidbody.velocity);
	}

	void BounceBlock(GameObject block)
	{
		float y = 0.0f;
		y = (transform.position.y - block.transform.position.y) / blockLength / 2;

		if (block.transform.position.x > transform.position.x)
		{
			Vector2 dir = new Vector2(-1, y).normalized;
			_rigidbody.velocity = dir * speed;
		}
		else
		{
			Vector2 dir = new Vector2(1, y).normalized;
			_rigidbody.velocity = dir * speed;
		}
	}

    public void OnStart()
    {
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(new Vector2(speed, 0), ForceMode2D.Impulse);
        transform.parent = game.transform;
        started = true;
    }
}
