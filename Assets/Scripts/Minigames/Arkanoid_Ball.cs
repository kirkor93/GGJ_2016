using UnityEngine;
using System.Collections;

public class Arkanoid_Ball : MonoBehaviour
{
	public Arkanoid_GameController game;
	public GameObject player1Platform;
	public GameObject player2Platform;
    public bool started = false;
    Rigidbody2D _rigidbody;
	float speed = 1000;
	float platformLength = 312;

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
             _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, -_rigidbody.velocity.y);
        }

        if(coll.gameObject.tag == "Player")
        {
			if (coll.gameObject.name == "player1_platform")
				Bounce (player1Platform);
			else if (coll.gameObject.name == "player2_platform")
				Bounce (player2Platform);
        }
    }

	void OnCollisionExit2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Safe") 
		{
			_rigidbody.velocity = _rigidbody.velocity.normalized * speed;
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
		Debug.Log ("Velocity: " + _rigidbody.velocity);
	}

    public void OnStart()
    {
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(new Vector2(1000, 0), ForceMode2D.Impulse);
        transform.parent = game.transform;
        started = true;
    }
}
