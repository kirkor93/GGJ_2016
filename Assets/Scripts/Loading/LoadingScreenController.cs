using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LoadingScreenController : MonoBehaviour
{
    public Player[] Characters;
    public Text LoadingText;
    public float TextChangeInterval = 1.0f;

    public string[] LoadingTexts = {"Loading.", "Loading..", "Loading..."};

    protected void Start()
    {
        StartCoroutine(AnimateText());
    }

    private IEnumerator AnimateText()
    {
        while (true)
        {
            foreach (string loadingText in LoadingTexts)
            {
                LoadingText.text = loadingText;
                yield return new WaitForSeconds(TextChangeInterval);
            }
        }
    }

    protected void Update()
    {
        foreach (Player character in Characters)
        {
            character.OnMove(Vector2.right);
        }
    }
}
