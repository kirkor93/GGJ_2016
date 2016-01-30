using UnityEngine;
using System.Collections;
using Assets.Scripts;

public class LoadingScreenController : MonoBehaviour
{
    public Player[] Characters;

    protected void Update()
    {
        foreach (Player character in Characters)
        {
            character.OnMove(Vector2.right);
        }
    }
}
