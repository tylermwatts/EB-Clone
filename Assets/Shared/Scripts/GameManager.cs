﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	public bool paused = false;

	void Awake(){
		if (instance == null){
			instance = this;
		} else if (instance != this){
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}
	
	void Update (){
		if (paused){
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}
	}

}