using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryList : MonoBehaviour {

	public Text[] itemSlots = new Text[42];

	private CharacterInfo player1;
	private List <string> itemNames = new List<string>();


	void Start(){
		player1 = GameObject.FindObjectOfType<CharacterInfo>();
	}

	void Update(){
		foreach (Item item in player1.inventoryList){
			string nameOfItem = item.ItemName;
			itemNames.Add(nameOfItem);
		}
		for (int i = 0; i < player1.inventoryList.Count; i++){
				itemSlots[i].text = itemNames[i];
			}

		for (int c = player1.inventoryList.Count; c < 42; c++){
			itemSlots[c].text = "";
		}
	}
	
}
