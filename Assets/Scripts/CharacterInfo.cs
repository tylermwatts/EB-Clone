using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterInfo : MonoBehaviour {

	public List <Item> inventoryList = new List<Item>();
	public CharacterName characterName;
	public ExpChart expChart;

	// More information on character stats https://github.com/warpfox/EB-Clone/wiki/Character-Stats
	[SerializeField] private int iq, guts, luck, vitality, speed, offense, defense;
	[SerializeField] private int characterLevel, currentEXP, maxHP, maxPP, currentHP, currentPP;

	// Use this for initialization
	void Start () {
		// Testing adding items to inventory

		Item item0 = new Item { ItemType = ItemType.Arms, ItemName = "Cheap Bracelet" };
		Item item1 = new Item { ItemType = ItemType.Consumable, ItemName = "Bread Roll", RecoversHP = 8 };
		Item item2 = new Item { ItemType = ItemType.Reusable, ItemName = "For Sale Sign" };
		Item item3 = new Item { ItemType = ItemType.Weapon, ItemName = "Baseball Bat" };

		inventoryList.Add(item0);
		inventoryList.Add(item1);
		inventoryList.Add(item2);
		inventoryList.Add(item3);
	}

	void Update (){
		// if (currentEXP >= expChart.expToLevel[characterLevel - 1]){
		// 	LevelUp();
		// }
	}

    public void PutItemInInventory(Item item){
		inventoryList.Add(item);
	}

	private void LevelUp(){
		characterLevel++;

		#region Base Stats Increase
		int tempVit = Random.Range(1,6);
		vitality += tempVit;
		// Text.text = $"Vitality increased by {tempVit} points!";
		int hpToGain = (tempVit * 15);
		maxHP += hpToGain;
		// Text.text = $"HP increased by {hpToGain} points!";
		
		int tempIQ = Random.Range(1,6);
		iq += tempIQ;
		// Text.text = $"IQ increased by {tempIQ} points!";
		// if (!Jeff){
		int ppToGain = (tempIQ * 5);
		maxPP += ppToGain;
		// Text.text = $"PP increased by {ppToGain} points!";
		// }
		
		int tempGuts = Random.Range(1,6);
		guts += tempGuts;
		// Text.text = $"Guts increased by {tempGuts} points!";
		
		// Text.text etc., as above for each stat
		luck += Random.Range(1,6);
		speed += Random.Range(1,6);
		offense += Random.Range(1,6);
		defense += Random.Range(1,6);
		#endregion

	}

	public void GiveExp(int exp){
		currentEXP += exp;
	}

}
