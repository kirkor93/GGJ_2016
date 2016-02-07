using UnityEngine;

public class Cheat : MonoBehaviour
{
    void Update ()
    {
        if (Input.GetKeyUp(KeyCode.Q))
        {
            SceneLoader.Instance.LoadLevel("level01");
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            SceneLoader.Instance.LoadLevel("memory");
        }
    }
}
