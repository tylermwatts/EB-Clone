using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInfo : MonoBehaviour {

	public List <Item> inventoryList = new List<Item>();
	public CharacterName characterName;
	public ExpChart expChart;

	// 1 IQ point = 5 PP
	// Guts = critical hit chance
	// Luck = chance to hit and dodge
	// Vitality = determines max HP
	// Speed = Determines who goes first in battles, also raises dodge chance
	// Offense = damage dealt
	// Defense = damage reduction from PHYSICAL sources. Does not affect damage from PSI.
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
		//		LevelUp();
		// }
	}

    public void PutItemInInventory(Item item){
		inventoryList.Add(item);
	}

	private void LevelUp(){
		characterLevel++;

		#region Base Stats Increase
		iq += UnityEngine.Random.Range(1,5);
		guts += UnityEngine.Random.Range(1,5);
		luck += UnityEngine.Random.Range(1,5);
		vitality += UnityEngine.Random.Range(1,5);
		speed += UnityEngine.Random.Range(1,5);
		offense += UnityEngine.Random.Range(1,5);
		defense += UnityEngine.Random.Range(1,5);
		#endregion
		
		if (vitality % 5 == 0){
			maxHP++;
		}

		if (iq % 5 == 0){
			maxPP++;
		}

	}

	public void AwardExperiencePoints(int exp){
		currentEXP += exp;
	}

}
