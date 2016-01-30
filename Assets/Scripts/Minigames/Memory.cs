using UnityEngine;
using System.Collections;

public class Memory : MonoBehaviour
{
	public GameObject[,] board = new GameObject[4,3];
	public GameObject tile;
	public GameObject player1Pointer;
	public GameObject player2Pointer;
	public int offset = 300;

	int player1x = 0;
	int player1y = 0;
	int player2x = 2;
	int player2y = 0;

	float player1MoveTimer = 0.25f;
	float player2MoveTimer = 0.25f;
	bool player1JustMoved = false;
	bool player2JustMoved = false;

	void Start ()
	{
		GenerateBoard();
	}
	
	void Update ()
	{
		Player1Input();
		Player2Input();
		HandleMovement();
	}

	void Player1Input()
	{
		if (!player1JustMoved)
		{
			float x = 0;
			float y = 0;
			if (Input.GetAxis(string.Format("{0}_{1}", "Joy1", "Horizontal")) > 0.5f)
			{
				if (player1x < 1)
				{
					player1x++;
					player1JustMoved = true;
				}
			}
			else if (Input.GetAxis(string.Format("{0}_{1}", "Joy1", "Horizontal")) < -0.5f)
			{
				if (player1x > 0)
				{
					player1x--;
					player1JustMoved = true;
				}
			}

			if (Input.GetAxis(string.Format("{0}_{1}", "Joy1", "Vertical")) > 0.5f)
			{
				if (player1y < 2)
				{
					player1y++;
					player1JustMoved = true;
				}
			}
			else if (Input.GetAxis(string.Format("{0}_{1}", "Joy1", "Vertical")) < -0.5f)
			{
				if (player1y > 0)
				{
					player1y--;
					player1JustMoved = true;
				}
			}

			x = player1x * offset - 600;
			y = player1y * offset - 300;
			player1Pointer.transform.position = new Vector3(x, y, 0);			
		}
		if (Input.GetKeyDown(KeyCode.Joystick1Button0))
		{
			foreach (GameObject tile in board)
			{
				if (Vector2.Distance(tile.transform.position, player1Pointer.transform.position) < 10)
				{
					if (tile.GetComponent<SpriteRenderer>().color == Color.white)
						tile.GetComponent<SpriteRenderer>().color = Color.green;
					else
						tile.GetComponent<SpriteRenderer>().color = Color.white;
				}
			}
		}
	}

	void Player2Input()
	{
		if (!player2JustMoved)
		{
			float x = 0;
			float y = 0;
			if (Input.GetAxis(string.Format("{0}_{1}", "Joy2", "Horizontal")) > 0.5f)
			{
				if (player2x < 3)
				{
					player2x++;
					player2JustMoved = true;
				}
			}
			else if (Input.GetAxis(string.Format("{0}_{1}", "Joy2", "Horizontal")) < -0.5f)
			{
				if (player2x > 2)
				{
					player2x--;
					player2JustMoved = true;
				}
			}

			if (Input.GetAxis(string.Format("{0}_{1}", "Joy2", "Vertical")) > 0.5f)
			{
				if (player2y < 2)
				{
					player2y++;
					player2JustMoved = true;
				}
			}
			else if (Input.GetAxis(string.Format("{0}_{1}", "Joy2", "Vertical")) < -0.5f)
			{
				if (player2y > 0)
				{
					player2y--;
					player2JustMoved = true;
				}
			}

			x = player2x * offset - 300;
			y = player2y * offset - 300;
			player2Pointer.transform.position = new Vector3(x, y, 0);
		}
		if (Input.GetKeyDown(KeyCode.Joystick2Button0))
		{
			foreach (GameObject tile in board)
			{
				if (Vector2.Distance(tile.transform.position, player2Pointer.transform.position) < 10)
				{
					if (tile.GetComponent<SpriteRenderer>().color == Color.white)
						tile.GetComponent<SpriteRenderer>().color = Color.green;
					else
						tile.GetComponent<SpriteRenderer>().color = Color.white;
				}
			}
		}
	}

	void HandleMovement()
	{
		if(player1JustMoved)
		{
			if (player1MoveTimer > 0)
				player1MoveTimer -= Time.deltaTime;
			else
			{
				player1MoveTimer = 0.25f;
				player1JustMoved = false;
			}
		}

		if (player2JustMoved)
		{
			if (player2MoveTimer > 0)
				player2MoveTimer -= Time.deltaTime;
			else
			{
				player2MoveTimer = 0.25f;
				player2JustMoved = false;
			}
		}
	}

	void GenerateBoard()
	{
		for(int x = 0; x < 4; x++)
		{
			for(int y = 0; y < 3; y++)
			{
				if(x < 2)
				{
					GameObject tempTile = Instantiate(tile, new Vector3(x * offset - 600, y * offset - 300, 0), Quaternion.identity) as GameObject;
					tempTile.transform.parent = transform;
					board[x, y] = tempTile;
				}
				else
				{
					GameObject tempTile = Instantiate(tile, new Vector3(x * offset - 300, y * offset - 300, 0), Quaternion.identity) as GameObject;
					tempTile.transform.parent = transform;
					board[x, y] = tempTile;
				}
			}
		}
	}
}
