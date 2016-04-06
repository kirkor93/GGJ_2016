using System;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Player[] Players;

    [Range(0.0f, 1.0f)]
    public float ShootingMagnitude = 0.5f;

    private const string LeftStickHorizontalAxis = "Horizontal_L";
    private const string LeftStickVerticalAxis = "Vertical_L";
    private const string RightStickHorizontalAxis = "Horizontal_R";
    private const string RightStickVerticalAxis = "Vertical_R";
    private const string R2Axis = "R2";
    private const string L2Axis = "L2";

    private readonly float[] _lastRightSticksMagnitude = {0.0f, 0.0f};
    private readonly string[] _playerNames = { "Joy1", "Joy2" };

    protected void Awake()
    {
        if (Players.Length != 2)
        {
            Debug.LogError("You need to have 2 players in input manager");
        }
    }

    protected void Update()
    {
        Vector2[] movementAxes = { Vector2.zero, Vector2.zero };

        for (int i = 0; i < Players.Length; i++)
        {
            movementAxes[i].x += Input.GetAxis(string.Format("{0}_{1}", _playerNames[i], LeftStickHorizontalAxis));
            movementAxes[i].y += Input.GetAxis(string.Format("{0}_{1}", _playerNames[i], LeftStickVerticalAxis));
            Players[i].OnMove(movementAxes[i]);

            Vector2 aiming = Vector2.zero;
            aiming.x += Input.GetAxis(string.Format("{0}_{1}", _playerNames[i], RightStickHorizontalAxis));
            aiming.y += Input.GetAxis(string.Format("{0}_{1}", _playerNames[i], RightStickVerticalAxis));
            float magnitude = aiming.magnitude;
//            float r2 = Input.GetAxis(string.Format("{0}_{1}", _playerNames[i], R2Axis));
            if (magnitude > ShootingMagnitude && _lastRightSticksMagnitude[i] <= ShootingMagnitude)
            {
                Players[i].OnActionStart(aiming);
            }
            else if(magnitude <= ShootingMagnitude && _lastRightSticksMagnitude[i] > ShootingMagnitude)
            {
                Players[i].OnActionRelease(aiming);
            }
            _lastRightSticksMagnitude[i] = magnitude;
        }

        //joy 1
        if (Input.GetKeyDown(KeyCode.Joystick1Button0)
            || Input.GetKeyDown(KeyCode.Joystick1Button5))
        {
            Players[0].OnJumpStart();
        }
        if (Input.GetKeyUp(KeyCode.Joystick1Button0)
            || Input.GetKeyUp(KeyCode.Joystick1Button5))
        {
            Players[0].OnJumpRelease();
        }
        if (Input.GetKeyDown(KeyCode.Joystick1Button2))
        {
            Players[0].OnActionStart(movementAxes[0]);
        }
        if (Input.GetKeyUp(KeyCode.Joystick1Button2))
        {
            Players[0].OnActionRelease(movementAxes[0]);
        }

        //joy 2
        if (Input.GetKeyDown(KeyCode.Joystick2Button0)
            || Input.GetKeyDown(KeyCode.Joystick2Button5))
        {
            Players[1].OnJumpStart();
        }
        if (Input.GetKeyUp(KeyCode.Joystick2Button0)
            || Input.GetKeyUp(KeyCode.Joystick2Button5))
        {
            Players[1].OnJumpRelease();
        }
        if (Input.GetKeyDown(KeyCode.Joystick2Button2))
        {
            Players[1].OnActionStart(movementAxes[1]);
        }
        if (Input.GetKeyUp(KeyCode.Joystick2Button2))
        {
            Players[1].OnActionRelease(movementAxes[1]);
        }

        //keyboard
//        float verticalMovement = 0.0f;
//        if (Input.GetKeyDown(KeyCode.UpArrow))
//        {
//            verticalMovement = 1.0f;
//        }
//        else if(Input.GetKey(KeyCode.DownArrow))
//        {
//            verticalMovement = -1.0f;
//        }
//
//        float horizontalMovement = 0.0f;
//        if (Input.GetKey(KeyCode.RightArrow))
//        {
//            horizontalMovement = 1.0f;
//        }
//        else if(Input.GetKey(KeyCode.LeftArrow))
//        {
//            horizontalMovement = -1.0f;
//        }
//
//        Players[1].OnMove(new Vector2(horizontalMovement, verticalMovement));
//
//        if (Input.GetKeyDown(KeyCode.X))
//        {
//            Players[1].OnJumpStart();
//        }
//        if (Input.GetKeyUp(KeyCode.X))
//        {
//            Players[1].OnJumpRelease();
//        }
//        if (Input.GetKeyDown(KeyCode.Z))
//        {
//            Players[1].OnActionStart();
//        }
//        if (Input.GetKeyUp(KeyCode.Z))
//        {
//            Players[1].OnActionRelease();
//        }
    }
}

