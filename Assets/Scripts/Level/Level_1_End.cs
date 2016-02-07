using UnityEngine;

public class Level_1_End : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.layer == LayerMask.NameToLayer("hero") )
        {
            SceneLoader.Instance.LoadLevel("memory");
        }
    }
}
