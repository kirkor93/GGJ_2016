using UnityEngine;
using System.Collections;
using DG.Tweening;

public class AmbientSound : MonoBehaviour
{
	void Awake()
	{
		DontDestroyOnLoad(gameObject);
		GetComponent<AudioSource>().volume = 0;
		GetComponent<AudioSource>().DOFade(1, 2);
	}

	void Start ()
	{
		
	}
	
	void Update ()
	{
	
	}

	void OnLevelWasLoaded(int level)
	{
		GetComponent<AudioSource>().volume = 0;
		GetComponent<AudioSource>().DOFade(1, 2);
	}
}
