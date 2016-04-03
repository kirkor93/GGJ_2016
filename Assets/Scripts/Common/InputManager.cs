using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Player[] Players;

    private readonly string[] PlayerNames = { "Joy1", "Joy2" };
    private const string HorizontalAxis = "Horizontal";
    private const string VerticalAxis = "Vertical";

    protected void Awake()
    {
        if (Players.Length != 2)
        {
            Debug.LogError("You need to have 2 players in input manager");
        }
    }

    protected void Update()
    {
        for (int i = 0; i < Players.Length; i++)
        {
            Vector2 movement = Vector2.zero;
            movement.x += Input.GetAxis(string.Format("{0}_{1}", PlayerNames[i], HorizontalAxis));
            movement.y += Input.GetAxis(string.Format("{0}_{1}", PlayerNames[i], VerticalAxis));
            Players[i].OnMove(movement);
        }

        //joy 1
        if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            Players[0].OnJumpStart();
        }
        if (Input.GetKeyUp(KeyCode.Joystick1Button0))
        {
            Players[0].OnJumpRelease();
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            Players[0].OnActionStart();
        }
        if (Input.GetKeyUp(KeyCode.Joystick1Button2))
        {
            Players[0].OnActionRelease();
        }

        //joy 2
        if (Input.GetKeyDown(KeyCode.Joystick2Button0))
        {
            Players[1].OnJumpStart();
        }
        if (Input.GetKeyUp(KeyCode.Joystick2Button0))
        {
            Players[1].OnJumpRelease();
        }
        if (Input.GetKeyDown(KeyCode.Joystick2Button2))
        {
            Players[1].OnActionStart();
        }
        if (Input.GetKeyUp(KeyCode.Joystick2Button2))
        {
            Players[1].OnActionRelease();
        }

        //keyboard
        float verticalMovement = 0.0f;
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            verticalMovement = 1.0f;
        }
        else if(Input.GetKey(KeyCode.DownArrow))
        {
            verticalMovement = -1.0f;
        }

        float horizontalMovement = 0.0f;
        if (Input.GetKey(KeyCode.RightArrow))
        {
            horizontalMovement = 1.0f;
        }
        else if(Input.GetKey(KeyCode.LeftArrow))
        {
            horizontalMovement = -1.0f;
        }

        Players[1].OnMove(new Vector2(horizontalMovement, verticalMovement));

        if (Input.GetKeyDown(KeyCode.X))
        {
            Players[1].OnJumpStart();
        }
        if (Input.GetKeyUp(KeyCode.X))
        {
            Players[1].OnJumpRelease();
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            Players[1].OnActionStart();
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            Players[1].OnActionRelease();
        }
    }
}

