﻿using UnityEngine;
using System.Collections;
using DG.Tweening;

public class AmbientSound : MonoBehaviour
{
	void Awake()
	{
		DontDestroyOnLoad(gameObject);
		GetComponent<AudioSource>().volume = 0;
		GetComponent<AudioSource>().DOFade(0.5f, 2);
	}

	void Start ()
	{
		
	}
	
	void Update ()
	{
	
	}
}
