using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryList : MonoBehaviour {

	private List <string> inventoryItems = new List<string>();

	public void AddItem(string item){
		inventoryItems.Add(item);
	}

	public void DeleteItem(string item){
		inventoryItems.Remove(item);
	}

	public string ReturnItemAtIndex(int index){
		string listItem = inventoryItems[index];
		return listItem;
	}

	public int ReturnLengthOfList(){
		return inventoryItems.Count;
	}
}
