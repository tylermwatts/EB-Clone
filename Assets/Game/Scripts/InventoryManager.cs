using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InventoryManager : MonoBehaviour {

	private CharacterInfo[] characters;
	private int spotInParty = 0;
	private int totalCharacters;

	void Awake () {
		DontDestroyOnLoad(this.gameObject);
	}

	// Use this for initialization
	void Start () {
		characters = GameObject.FindObjectsOfType<CharacterInfo>();
		totalCharacters = characters.Length;
	}
	
	public void GiveItem(Item item){
		if (spotInParty == (totalCharacters - 1) && characters[spotInParty].inventoryList.Count >= 42){
			Debug.Log ("Inventory is full!");
		}
		if (characters[spotInParty].inventoryList.Count > 42){
			Debug.LogError("Number of items in inventory exceeds maximum capacity!");
		} else if (characters[spotInParty].inventoryList.Count == 42){
			spotInParty++;
			GiveItem(item);
		} else if (characters[spotInParty].inventoryList.Count < 42){
			characters[spotInParty].inventoryList.Add(item);
			spotInParty = 0;
		}

	}

}
