using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public void StartGameButton()
    {
        SceneLoader.Instance.LoadLevel("level01");
    }
}
