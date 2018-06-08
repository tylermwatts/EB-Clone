using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	public bool paused = false;
	public List <CharacterInfo> characters = new List <CharacterInfo>();


	void Awake(){
		if (instance == null){
			instance = this;
		} else if (instance != this){
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}

	void Start () {
		characters = FindObjectsOfType<CharacterInfo>().ToList();
	}
	
	void Update (){
		if (paused){
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}
	}

}
