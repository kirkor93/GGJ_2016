using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CameraFollow : MonoBehaviour
{
	public List<GameObject> heroes;

	void Start ()
	{
	
	}
	
	void Update ()
	{
		UpdatePosition();
	}

	void UpdatePosition()
	{
		float distance = 0;
		if (heroes[0].transform.position.x > heroes[1].transform.position.x)
		{
			distance = heroes[0].transform.position.x - heroes[1].transform.position.x;
			transform.position = new Vector3(heroes[1].transform.position.x + distance / 2, 0, -10);
		}
		else
		{
			distance = heroes[1].transform.position.x - heroes[0].transform.position.x;
			transform.position = new Vector3(heroes[0].transform.position.x + distance / 2, 0, -10);
		}
		
		if(transform.position.x <= 0)
		{
			transform.position = new Vector3(0, transform.position.y, -10);
		}

	}
}
