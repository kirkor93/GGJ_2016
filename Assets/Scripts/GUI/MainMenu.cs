using UnityEngine;
using System.Collections;
using DG.Tweening;

public class MainMenu : MonoBehaviour
{
    public RectTransform MainMenuPanel;
    public RectTransform CreditsPanel;
    public RectTransform GameName;
    public RectTransform TargetNamePos;

    protected void Awake()
    {
        GameName.DOMoveY(TargetNamePos.position.y, 0.5f, true)
            .OnComplete(() =>
            {
                Transform t = GameName.GetChild(0);
                t.SetParent(transform);
                t.DOPunchRotation(Vector3.forward, 1.5f);
            });
    }

    public void StartGameButton()
    {
        SceneLoader.Instance.LoadLevel("Cutscene");
    }

    public void ExitButton()
    {
        Application.Quit();
    }

    public void CreditsButton()
    {
//        CreditsPanel.gameObject.SetActive(true);
//        MainMenuPanel.gameObject.SetActive(false);

        MainMenuPanel.DOScale(Vector3.zero, 0.5f).OnComplete(() => CreditsPanel.DOScale(Vector3.one, 0.5f));
    }

    public void BackButton()
    {
        CreditsPanel.DOScale(Vector3.zero, 0.5f).OnComplete(() => MainMenuPanel.DOScale(Vector3.one, 0.5f));
    }
}
