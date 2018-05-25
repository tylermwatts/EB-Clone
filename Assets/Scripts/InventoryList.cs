﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryList : MonoBehaviour {

	public GameObject invButtonPrefab;

	private CharacterInfo player1;

	void Start(){
		player1 = GameObject.FindObjectOfType<CharacterInfo>();
		PopulateInventoryText();
	}

	void Update()
    {
        
    }

    private void PopulateInventoryText()
    {
		foreach (Item item in player1.inventoryList)
        {
			var inventoryItem = Instantiate <GameObject>(invButtonPrefab);
			inventoryItem.transform.SetParent(transform, false);
			inventoryItem.transform.localScale = Vector3.one;
			inventoryItem.GetComponentInChildren<Text>().text = item.ItemName;
		}
    }
}
