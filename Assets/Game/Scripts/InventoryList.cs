using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryList : MonoBehaviour {

	public GameObject invButtonPrefab;
	
	private Menu menu;

	[SerializeField]
	private GameManager gameManager;


	void OnEnable(){
		menu = GetComponent<Menu>();
		menu.enabled = false;
	}

	void Start(){
		PopulateInventoryText();
	}

	void Update()
    {
        
    }

    private void PopulateInventoryText()
    {
		CharacterInfo displayedCharacter = gameManager.characters[CharacterName.Ness];
		foreach (Item item in displayedCharacter.inventoryList)
        {
			var inventoryItem = Instantiate <GameObject>(invButtonPrefab);
			inventoryItem.transform.SetParent(transform, false);
			inventoryItem.transform.localScale = Vector3.one;
			inventoryItem.GetComponentInChildren<Text>().text = item.ItemName;
		}
		menu.enabled = true;
    }
}
