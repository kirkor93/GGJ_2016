using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneLoader : Singleton<SceneLoader>
{
    public float LoadingScreenDuration = 5.0f;

    private string _sceneToLoad;
    private AsyncOperation _loadingOperation;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(gameObject);
    }

    public void LoadLevel(string name)
    {
        _sceneToLoad = name;
        SceneManager.LoadScene("LoadingScreen");
    }

    protected void OnLevelWasLoaded(int level)
    {
        if (SceneManager.GetActiveScene().name == "LoadingScreen")
        {
            StartCoroutine(Delay());
        }
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(LoadingScreenDuration);
        SceneManager.LoadScene(_sceneToLoad);
    }
}
