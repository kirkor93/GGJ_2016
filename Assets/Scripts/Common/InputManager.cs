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
        if (Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            Players[0].OnJumpStart();
        }
        if (Input.GetKeyDown(KeyCode.Joystick2Button0))
        {
            Players[1].OnJumpStart();
        }
        if (Input.GetKeyUp(KeyCode.Joystick1Button0))
        {
            Players[0].OnJumpRelease();
        }
        if (Input.GetKeyUp(KeyCode.Joystick2Button0))
        {
            Players[1].OnJumpRelease();
        }

        if (Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            Players[0].OnActionStart();
        }
        if (Input.GetKeyDown(KeyCode.Joystick2Button2))
        {
            Players[1].OnActionStart();
        }
        if (Input.GetKeyUp(KeyCode.Joystick1Button2))
        {
            Players[0].OnActionRelease();
        }
        if (Input.GetKeyUp(KeyCode.Joystick2Button2))
        {
            Players[1].OnActionRelease();
        }
    }
}

