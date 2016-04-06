using UnityEngine;
using System.Collections;

public class Arkanoid_GameController : MonoBehaviour 
{
	public Arkanoid_Ball ball;
	public GameObject player1Platform;
	int extraLifes = 2;

	void Start () 
	{
	
	}
	
	void Update () 
	{
	
	}

	public void Die()
	{
		if (extraLifes > 0) 
		{
			extraLifes--;
			ball.transform.parent = player1Platform.transform;
			ball.transform.position = new Vector3 (player1Platform.transform.position.x + 65, player1Platform.transform.position.y, 0);
			ball.started = false;
			ball.Rigid.isKinematic = true;
		}
	}
}
