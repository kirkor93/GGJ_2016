using UnityEngine;
using System.Collections;

public class Arkanoid_Input : MonoBehaviour
{
    public GameObject player1Platform;
    public GameObject player2Platform;
    public Arkanoid_Ball ball;

    float minY = -320;
    float maxY = 320;

    void Start ()
    {
	
	}
	
	void Update ()
    {
        Player1Input();
        Player2Input();
	}

    void Player1Input()
    {
		if(!ball.started)
		{
			if (player1Platform.transform.FindChild("ball"))
			{
				if (Input.GetKeyDown(KeyCode.Joystick1Button0) || Input.GetKeyDown(KeyCode.Space))
				{
					ball.OnStart();
				}
			}
			else if (player2Platform.transform.FindChild("ball"))
			{
				if (Input.GetKeyDown(KeyCode.Joystick2Button0) || Input.GetKeyDown(KeyCode.Space))
				{
					ball.OnStart();
				}
			}

		  }

		if(Input.GetKeyDown(KeyCode.DownArrow))
		{
			player1Platform.transform.position = new Vector3(player1Platform.transform.position.x, player1Platform.transform.position.y - 10, 0);
		}

		if(Input.GetKeyDown(KeyCode.UpArrow))
		{
			player1Platform.transform.position = new Vector3(player1Platform.transform.position.x, player1Platform.transform.position.y + 10, 0);
		}

        float inputValue = Input.GetAxis(string.Format("{0}_{1}", "Joy1", "Vertical"));

        if((inputValue > 0 && player1Platform.transform.position.y < maxY) || (inputValue < 0 && player1Platform.transform.position.y > minY))
            player1Platform.transform.position = new Vector3(player1Platform.transform.position.x, player1Platform.transform.position.y + inputValue * 10, 0);
    }

    void Player2Input()
    {
        float inputValue = Input.GetAxis(string.Format("{0}_{1}", "Joy2", "Vertical"));

        if ((inputValue > 0 && player2Platform.transform.position.y < maxY) || (inputValue < 0 && player2Platform.transform.position.y > minY))
            player2Platform.transform.position = new Vector3(player2Platform.transform.position.x, player2Platform.transform.position.y + inputValue * 10, 0);
    }
}
