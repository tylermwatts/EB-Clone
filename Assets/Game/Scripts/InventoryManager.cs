using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InventoryManager : MonoBehaviour {

	private List <CharacterInfo> characters = new List <CharacterInfo>();
	private int spotInParty = 0;
	private int totalCharacters;
	[SerializeField]
	private GameManager gameManager;
	
	public static InventoryManager instance;

	void Awake () {
		if (instance == null){
			instance = this;
		} else if (instance != this){
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}

	// Use this for initialization
	void Start () {
		characters = gameManager.characters.Values.ToList();
		totalCharacters = characters.Count();
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
