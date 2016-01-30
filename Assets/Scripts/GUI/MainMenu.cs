using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour
{
    public RectTransform MainMenuPanel;
    public RectTransform CreditsPanel;


    public void StartGameButton()
    {
        SceneLoader.Instance.LoadLevel("level01");
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void CreditsButton()
    {
        CreditsPanel.gameObject.SetActive(true);
        MainMenuPanel.gameObject.SetActive(false);
    }

    public void BackButton()
    {
        CreditsPanel.gameObject.SetActive(false);
        MainMenuPanel.gameObject.SetActive(true);
    }
}
