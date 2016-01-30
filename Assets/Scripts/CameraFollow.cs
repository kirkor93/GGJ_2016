using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraFollow : MonoBehaviour
{
	public List<GameObject> heroes;
	public float maxX;
	public float maxY;

	void Start ()
	{
	
	}
	
	void Update ()
	{
		UpdatePosition();
	}

	void UpdatePosition()
	{
		float distanceX = 0;
		float distanceY = 0;
		if (heroes[0].transform.position.x > heroes[1].transform.position.x)
		{
			distanceX = heroes[0].transform.position.x - heroes[1].transform.position.x;
			transform.position = new Vector3(heroes[1].transform.position.x + distanceX / 2, transform.position.y, -10);
		}
		else
		{
			distanceX = heroes[1].transform.position.x - heroes[0].transform.position.x;
			transform.position = new Vector3(heroes[0].transform.position.x + distanceX / 2, transform.position.y, -10);
		}

		if (heroes[0].transform.position.y > heroes[1].transform.position.y)
		{
			distanceY = heroes[0].transform.position.y - heroes[1].transform.position.y;
			transform.position = new Vector3(transform.position.x, heroes[1].transform.position.y + distanceY / 2, -10);
		}
		else
		{
			distanceY = heroes[1].transform.position.y - heroes[0].transform.position.y;
			transform.position = new Vector3(transform.position.x, heroes[0].transform.position.y + distanceY / 2, -10);
		}

		if (transform.position.x <= 0)
		{
			transform.position = new Vector3(0, transform.position.y, -10);
		}

		if (transform.position.y <= 0)
		{
			transform.position = new Vector3(transform.position.x, 0, -10);
		}

		if(transform.position.x >= maxX)
		{
			transform.position = new Vector3(maxX, transform.position.y, -10);
		}

		if(transform.position.y >= maxY)
		{
			transform.position = new Vector3(transform.position.x, maxY, -10);
		}
	}
}
