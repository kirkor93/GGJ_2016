using UnityEngine;
using System.Collections;

public class FinishScreen : MonoBehaviour
{
    protected void Awake()
    {
        CutsceneController.SetupCameraForClipping(GetComponent<Camera>());
    }
}
