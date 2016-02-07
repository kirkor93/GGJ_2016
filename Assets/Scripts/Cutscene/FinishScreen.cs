using UnityEngine;

public class FinishScreen : MonoBehaviour
{
    protected void Awake()
    {
        CutsceneController.SetupCameraForClipping(GetComponent<Camera>());
    }
}
