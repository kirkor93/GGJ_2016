using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arkanoid_GameController : MonoBehaviour 
{
	public Arkanoid_Ball ball;
	public GameObject player1Platform;
	public GameObject player2Platform;
	public GameObject block;
	public Transform blocksParent;
	public Vector3 firstPos;
	public Text extraLifesText;

	public List<GameObject> currentBlocks;

	private int extraLifes = 2;
	private float endTimer = 2.0f;
	private float offsetX = 80;
	private float offsetY = 200;
	private bool end = false;
	private bool endGame = false;
	private bool gameOver = false;

	public bool EndGame
	{
		get { return endGame; }
	}

	public bool GameOver
	{
		get { return gameOver; }
	}

	void Start () 
	{
		GenerateBoard();
	}

	void Update()
	{
		if(!endGame && !gameOver)
		{
			CheckIfEnd();
		}
	}

    void GenerateBoard()
	{
		for(int i = 0; i < 14; i++)
		{
			if(i == 0 || i == 2 || i == 4 || i == 6 || i == 7 || i == 9 || i == 11 || i == 13)
			{
				GameObject tempBlock = Instantiate(block) as GameObject;
				tempBlock.transform.position = new Vector3(firstPos.x + i * offsetX, firstPos.y, 0);
				tempBlock.transform.parent = blocksParent;

				GameObject tempBlock2 = Instantiate(block) as GameObject;
				tempBlock2.transform.position = new Vector3(firstPos.x + i * offsetX, firstPos.y + offsetY, 0);
				tempBlock2.transform.parent = blocksParent;

				currentBlocks.Add(tempBlock);
				currentBlocks.Add(tempBlock2);
			}
			else
			{
				GameObject tempBlock = Instantiate(block) as GameObject;
				tempBlock.transform.position = new Vector3(firstPos.x + i * offsetX, firstPos.y - offsetY/2, 0);
				tempBlock.transform.parent = blocksParent;

				GameObject tempBlock2 = Instantiate(block) as GameObject;
				tempBlock2.transform.position = new Vector3(firstPos.x + i * offsetX, firstPos.y + offsetY / 2, 0);
				tempBlock2.transform.parent = blocksParent;

				GameObject tempBlock3 = Instantiate(block) as GameObject;
				tempBlock3.transform.position = new Vector3(firstPos.x + i * offsetX, firstPos.y + 1.5f * offsetY, 0);
				tempBlock3.transform.parent = blocksParent;

				currentBlocks.Add(tempBlock);
				currentBlocks.Add(tempBlock2);
				currentBlocks.Add(tempBlock3);
			}
		}
	}

	private void CheckIfEnd()
	{
		if (end)
		{
			if (endTimer > 0)
			{
				endTimer -= Time.deltaTime;
			}
			else
			{
				gameOver = true;
				endTimer = 2.0f;
				SceneLoader.Instance.LoadLevel("arkanoid");
			}
		}

		if(currentBlocks.Count == 0)
		{
			endGame = true;
			SceneLoader.Instance.LoadLevel("Finish");
		}
	}

	public void Die()
	{
		if (extraLifes > 0) 
		{
			extraLifes--;
			extraLifesText.text = "Extra Lifes: " + extraLifes;
			ball.started = false;
			ball.Rigid.isKinematic = true;
			ball.speed *= 1.25f;

			if(extraLifes == 1)
			{
				ball.transform.parent = player2Platform.transform;
				ball.transform.position = new Vector3(player2Platform.transform.position.x - 65, player2Platform.transform.position.y, 0);
			}
			else
			{
				ball.transform.parent = player1Platform.transform;
				ball.transform.position = new Vector3(player1Platform.transform.position.x + 65, player1Platform.transform.position.y, 0);
			}
		}
		else
		{
			end = true;
		}
	}

	public Vector2 RotateVector2(Vector2 v, float angle)
	{
		float sin = Mathf.Sin(angle * Mathf.Deg2Rad);
		float cos = Mathf.Cos(angle * Mathf.Deg2Rad);

		float tx = v.x;
		float ty = v.y;
		v.x = (cos * tx) - (sin * ty);
		v.y = (sin * tx) + (cos * ty);
		return v;
	}
}
