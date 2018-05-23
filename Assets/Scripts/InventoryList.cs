using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryList : MonoBehaviour {

	public Text[] itemSlots = new Text[42];

	private CharacterStats player_1_items;
	private List <string> itemNames = new List<string>();


	void Start(){
		player_1_items = GameObject.FindObjectOfType<CharacterStats>();
	}

	void Update(){
		foreach (Item item in player_1_items.inventoryList){
			string nameOfItem = item.ItemName;
			itemNames.Add(nameOfItem);
		}
		for (int i = 0; i < player_1_items.inventoryList.Count; i++){
				itemSlots[i].text = itemNames[i];
			}

		for (int c = player_1_items.inventoryList.Count; c < 42; c++){
			itemSlots[c].text = "";
		}
	}
	
}
