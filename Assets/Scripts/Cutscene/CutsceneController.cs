using UnityEngine;
using System.Collections;

public class CutsceneController : MonoBehaviour
{
    public float ReferenceScreenAspect = 16.0f/9.0f;
    public Renderer SilverScreen;

    protected void Awake()
    {
        Camera c = GetComponent<Camera>();
        c.orthographicSize *= ReferenceScreenAspect/c.aspect;

        StartCoroutine(SwapSceneOnFinish());
    }

    private IEnumerator SwapSceneOnFinish()
    {
        MovieTexture texture = SilverScreen.material.mainTexture as MovieTexture;
        if (texture != null)
        {
            texture.Play();

            yield return new WaitForSeconds(texture.duration + 0.1f);
        }
        else
        {
            Debug.LogError("Texture assigned to silver screen is not MovieTexture");
        }

        SceneLoader.Instance.LoadLevel("level01");
    }
}
