using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour {

	public static GameManager instance = null;

	public bool paused = false;
	public Dictionary <CharacterInfo, CharacterName> characters = new Dictionary <CharacterInfo, CharacterName>();


	void Awake(){
		if (instance == null){
			instance = this;
		} else if (instance != this){
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}

	void Start () {
		FillCharacterDictionary();
	}

    private void FillCharacterDictionary()
    {
		CharacterInfo[] chars = GameObject.FindObjectsOfType<CharacterInfo>();
        foreach (CharacterInfo character in chars){
			characters.Add(character, character.characterName);
		}
    }

    void Update (){
		if (paused){
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}
	}

}
