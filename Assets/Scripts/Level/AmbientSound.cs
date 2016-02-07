using UnityEngine;
using DG.Tweening;

public class AmbientSound : MonoBehaviour
{
	void Awake()
	{
		DontDestroyOnLoad(gameObject);
		GetComponent<AudioSource>().volume = 0;
		GetComponent<AudioSource>().DOFade(0.3f, 2);
	}

	void Start ()
	{
		
	}
	
	void Update ()
	{
	
	}
}
