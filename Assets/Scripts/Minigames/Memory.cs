using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using DG.Tweening;
using UnityEngine.UI;

public class Memory : MonoBehaviour
{
	public GameObject[,] board = new GameObject[6,2];
	public GameObject tile;
	public GameObject player1Pointer;
	public GameObject player2Pointer;
	public TextMesh timerText;
	public int offsetX = 250;
	public int offsetY = 600;

	private List<GameObject> uncoveredCards = new List<GameObject>();
	private List<int> availableCards = new List<int>();

	private int player1x = 0;
	private int player1y = 0;
	private int player2x = 3;
	private int player2y = 0;

	private float player1MoveTimer = 0.25f;
	private float player2MoveTimer = 0.25f;
	private float uncoveredCardTimer = 0.5f;
	private float coverCardTimer = 0.5f;
	private float timeToCover = 0.5f;
	private float timeToFade = 0.5f;
	private float fadeCardTimer = 0.5f;
	private float timer = 60;
	private bool player1JustMoved = false;
	private bool player2JustMoved = false;
	private bool isTurningCard = false;
	private bool coveringCards = false;
	private bool fadingCards = false;
	private bool endGame = false;
	private bool gameOver = false;

	public bool EndGame
	{
		get	{return endGame;}
	}

	public bool GameOver
	{
		get { return gameOver; }
	}

	void Start ()
	{
		GenerateAvailableCards();
		GenerateBoard();
		timerText.GetComponent<MeshRenderer>().sortingOrder = 2;
		timer = 60;
	}
	
	void Update ()
	{
		if (!endGame && !gameOver)
		{
			Player1Input();
			Player2Input();
			HandleMovement();
			UncoverCard();
			CheckIfPair();
			CoverCard();
			FadeCard();
			CheckIfEnd();
			HandleTimer();
		}
	}

	void Player1Input()
	{
		if (!player1JustMoved)
		{
			float x = 0;
			float y = 0;
			if (Input.GetAxis(string.Format("{0}_{1}", "Joy1", "Horizontal")) > 0.5f)
			{
				if (player1x < 2)
				{
					if (board[player1x + 1, player1y] != null)
					{
						player1x++;
						player1JustMoved = true;
					}
					else if(player1x < 1 && board[player1x + 2, player1y] != null)
					{
						player1x += 2;
						player1JustMoved = true;
					}
					else if(player1y == 0)
					{
						player1y++;
					}
					else
					{
						player1y--;
					}
				}
			}
			else if (Input.GetAxis(string.Format("{0}_{1}", "Joy1", "Horizontal")) < -0.5f)
			{
				if (player1x > 0)
				{
					if (board[player1x - 1, player1y] != null)
					{
						player1x--;
						player1JustMoved = true;
					}
					else if(player1x > 1 && board[player1x - 2, player1y] != null)
					{
						player1x -= 2;
						player1JustMoved = true;
					}
					else if (player1y == 0)
					{
						player1y++;
					}
					else
					{
						player1y--;
					}
				}
			}

			if (Input.GetAxis(string.Format("{0}_{1}", "Joy1", "Vertical")) > 0.5f)
			{
				if (player1y < 1)
				{
					if (board[player1x, player1y + 1] != null)
					{
						player1y++;
						player1JustMoved = true;
					}
				}
			}
			else if (Input.GetAxis(string.Format("{0}_{1}", "Joy1", "Vertical")) < -0.5f)
			{
				if (player1y > 0)
				{
					if (board[player1x, player1y - 1] != null)
					{
						player1y--;
						player1JustMoved = true;
					}
				}
			}

			x = player1x * offsetX - 700;
			y = player1y * offsetY - 200;
			player1Pointer.transform.position = new Vector3(x, y, 0);			
		}
		if (Input.GetKeyDown(KeyCode.Joystick1Button0) && uncoveredCards.Count < 2 && !isTurningCard)
		{
			foreach (GameObject tile in board)
			{
				if (tile != null)
				{
					if (Vector2.Distance(tile.transform.position, player1Pointer.transform.position) < 10)
					{
						if (tile.transform.FindChild("back").gameObject.activeSelf)
						{
							tile.transform.DOScaleX(0, 0.2f);
							uncoveredCards.Add(tile);
							isTurningCard = true;
						}
					}
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
				if (player2x < 6)
				{
					if (board[player2x + 1, player2y] != null)
					{
						player2x++;
						player2JustMoved = true;
					}
					else if (player2x < 5 && board[player2x + 2, player2y] != null)
					{
						player2x += 2;
						player2JustMoved = true;
					}
					else if (player2y == 0)
					{
						player2y++;
					}
					else
					{
						player2y--;
					}
				}
			}
			else if (Input.GetAxis(string.Format("{0}_{1}", "Joy2", "Horizontal")) < -0.5f)
			{
				if (player2x > 3)
				{
					if (board[player2x - 1, player2y] != null)
					{
						player2x--;
						player2JustMoved = true;
					}
					else if (player2x > 4 && board[player2x - 2, player2y] != null)
					{
						player2x -= 2;
						player2JustMoved = true;
					}
					else if (player2y == 0)
					{
						player2y++;
					}
					else
					{
						player2y--;
					}
				}
			}

			if (Input.GetAxis(string.Format("{0}_{1}", "Joy2", "Vertical")) > 0.5f)
			{
				if (player2y < 1)
				{
					if (board[player2x, player2y + 1] != null)
					{
						player2y++;
						player2JustMoved = true;
					}
				}
			}
			else if (Input.GetAxis(string.Format("{0}_{1}", "Joy2", "Vertical")) < -0.5f)
			{
				if (player2y > 0)
				{
					if (board[player2x, player2y - 1] != null)
					{
						player2y--;
						player2JustMoved = true;
					}
				}
			}

			x = player2x * offsetX - 500;
			y = player2y * offsetY - 200;
			player2Pointer.transform.position = new Vector3(x, y, 0);
		}
		if (Input.GetKeyDown(KeyCode.Joystick2Button0) && uncoveredCards.Count < 2 && !isTurningCard)
		{
			foreach (GameObject tile in board)
			{
				if (tile != null)
				{
					if (Vector2.Distance(tile.transform.position, player2Pointer.transform.position) < 10)
					{
						if (tile.transform.FindChild("back").gameObject.activeSelf)
						{
							tile.transform.DOScaleX(0, 0.2f);
							uncoveredCards.Add(tile);
							isTurningCard = true;
						}
					}
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
		for(int x = 0; x < 6; x++)
		{
			for(int y = 0; y < 2; y++)
			{
				if(x < 3)
				{
					GameObject tempTile = Instantiate(tile, new Vector3(x * offsetX - 700, y * offsetY - 200, 0), Quaternion.identity) as GameObject;
					tempTile.transform.parent = transform;
					int rand = UnityEngine.Random.Range(0, availableCards.Count);
					tempTile.GetComponent<Animator>().Play("tile", 0, availableCards[rand] * 0.2f);
					tempTile.transform.FindChild("back").gameObject.SetActive(true);
					board[x, y] = tempTile;
					availableCards.RemoveAt(rand);
				}
				else
				{
					GameObject tempTile = Instantiate(tile, new Vector3(x * offsetX - 500, y * offsetY - 200, 0), Quaternion.identity) as GameObject;
					tempTile.transform.parent = transform;
					int rand = UnityEngine.Random.Range(0, availableCards.Count);
					tempTile.GetComponent<Animator>().Play("tile", 0, availableCards[rand] * 0.2f);
					tempTile.transform.FindChild("back").gameObject.SetActive(true);
					board[x, y] = tempTile;
					availableCards.RemoveAt(rand);
				}
			}
		}
	}

	void GenerateAvailableCards()
	{
		for (int i=0; i < 12; i++)
		{
			if (i % 2 == 0)
				availableCards.Add(i / 2);
			else
				availableCards.Add((i - 1) / 2);
		}
	}

	void UncoverCard()
	{
		if(isTurningCard)
		{
			if (uncoveredCardTimer > 0.25f)
			{
				uncoveredCardTimer -= Time.deltaTime;
			}
			else if(uncoveredCardTimer <= 0.25f && uncoveredCardTimer > 0.2f)
			{
				uncoveredCards[uncoveredCards.Count - 1].transform.DOScaleX(1, 0.2f);
				uncoveredCards[uncoveredCards.Count - 1].transform.FindChild("back").gameObject.SetActive(false);
				uncoveredCardTimer = 0.2f;
			}
			else if (uncoveredCardTimer > 0)
			{
				uncoveredCardTimer -= Time.deltaTime;
			}
			else
			{
				uncoveredCardTimer = 0.5f;
				isTurningCard = false;
			}
		}
	}

	void CheckIfPair()
	{
		if(uncoveredCards.Count == 2)
		{
			if (uncoveredCards[0].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime == uncoveredCards[1].GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime)
			{
				FadeCards();
			}
			else
			{
				CoverCards();
			}
        }
	}

	void CoverCards()
	{
		if (!isTurningCard && !coveringCards)
		{
			if (timeToCover > 0)
			{
				timeToCover -= Time.deltaTime;
			}
			else
			{
				foreach (GameObject tile in uncoveredCards)
				{
					tile.transform.DOScaleX(0, 0.2f);
				}
				coveringCards = true;
				timeToCover = 0.5f;
			}
		}
	}

	void FadeCards()
	{
		if(!fadingCards)
		{
			if(timeToFade > 0)
			{
				timeToFade -= Time.deltaTime;
			}
			else
			{
				foreach(GameObject tile in uncoveredCards)
				{
					tile.GetComponent<SpriteRenderer>().DOFade(0, 0.5f);
				}
				fadingCards = true;
				timeToFade = 0.5f;
			}
		}
	}

	void CoverCard()
	{
		if(coveringCards)
		{
			if(coverCardTimer > 0.25f)
			{
				coverCardTimer -= Time.deltaTime;
			}
			else if(coverCardTimer <= 0.25f && coverCardTimer > 0.2f)
			{
				foreach (GameObject tile in uncoveredCards)
				{
					tile.transform.DOScaleX(1, 0.2f);
					tile.transform.FindChild("back").gameObject.SetActive(true);
				}
				coverCardTimer = 0.2f;
			}
			else if(coverCardTimer > 0)
			{
				coverCardTimer -= Time.deltaTime;
			}
			else
			{
				coveringCards = false;
				uncoveredCards.Clear();
				coverCardTimer = 0.5f;
			}
		}
	}

	void FadeCard()
	{
		if(fadingCards)
		{
			if(fadeCardTimer > 0)
			{
				fadeCardTimer -= Time.deltaTime;
			}
			else
			{
				fadingCards = false;
				for(int x = 0; x < board.GetLength(0); x++)
				{
					for (int y = 0; y < board.GetLength(1); y++)
					{
						if (board[x, y] != null)
						{
							if (board[x, y] == uncoveredCards[0] || board[x, y] == uncoveredCards[1])
							{
								board[x, y] = null;
							}
						}
					}
				}
				Destroy(uncoveredCards[0]);
				Destroy(uncoveredCards[1]);
				uncoveredCards.Clear();
			}
		}
	}

	void CheckIfEnd()
	{
		int counter = 0;
		foreach(GameObject card in board)
		{
			if (card != null)
				counter++;
		}
		if (counter == 0)
		{
			endGame = true;
			SceneLoader.Instance.LoadLevel("Finish");
			Debug.Log("WINNER!!!!!!!!!11111oneoneone");
		}
	}

	void HandleTimer()
	{
		if (timer > 0)
		{
			timer -= Time.deltaTime;
			timerText.text = Math.Round(timer, 2).ToString();
		}
		else
		{
			timerText.text = "00.00";
			timer = 60;
			gameOver = true;
			SceneLoader.Instance.LoadLevel("memory");
			Debug.Log("GAME OVER LOSERS!!!");
		}
	}
}
