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
	private int[] paulaGrowthRates = new int[]{12,3,8,5,5,2,7};
  private int[] nessGrowthRates = new int[]{18,5,4,7,6,5,5};
	private int[] jeffGrowthRates = new int[]{10,6,5,5,4,3,9};
	private int[] pooGrowthRates = new int[]{21,18,7,3,3,4,4};

    // Use this for initialization
    void Start () {
		// Testing adding items to inventory

		Item item0 = new Item { ItemType = ItemType.Arms, ItemName = "Cheap Bracelet" };
		Item item1 = new Item { ItemType = ItemType.Food, ItemName = "Bread Roll" };
		Item item2 = new Item { ItemType = ItemType.Miscellaneous, ItemName = "For Sale Sign" };
		Item item3 = new Item { ItemType = ItemType.Weapon, ItemName = "Baseball Bat" };

		inventoryList.Add(item0);
		inventoryList.Add(item1);
		inventoryList.Add(item2);
		inventoryList.Add(item3);
	}

	void Update (){
		/* 	Still don't know if CharacterInfo actually needs an Update method. We can probably
			make any changes we need to do from other scripts through public methods on this
			script. Leaving it for now, but we may end up foregoing Update altogether.
		*/
	}

    public void PutItemInInventory(Item item){
		inventoryList.Add(item);
	}

	public void GiveExp(int exp){
		currentEXP += exp;
		if (currentEXP >= expChart.expToLevel[characterLevel - 1]){
			LevelUp();
		}
	}

	private void LevelUp()
    {
        int oldLevel = characterLevel;
        characterLevel++;
		// Text.text = $"{characterName} is now level {characterLevel}!";

        #region Character Specific Stat Growth Rates

        switch (characterName)
        {
            case CharacterName.Ness:
                StatIncrease(nessGrowthRates, oldLevel);
                break;
            case CharacterName.Paula:
                StatIncrease(paulaGrowthRates, oldLevel);
                break;
            case CharacterName.Jeff:
                StatIncrease(jeffGrowthRates, oldLevel);
                break;
            case CharacterName.Poo:
                StatIncrease(pooGrowthRates, oldLevel);
                break;
        }
        #endregion

    }

    private void StatIncrease(int[] growthRates, int oldLevel)
    {
		int r, rVitIQ;
        // Stat gain = ((growth rate * old level) - ((stat-2) * 10)) * r/50
        /*
			r is given by one of the following: 
				- If the stat is vitality or IQ, and the new level is 10 or lower, r=5. 
				- Otherwise, if the new level is divisible by 4, r is a random number from 7 to 10. 
				- Otherwise, r is a random number from 3 to 6. 
		*/
        // Calculate randomization factor "r" for every stat except Vitality & IQ
        if (characterLevel % 4 == 0) { r = Random.Range(7, 11); } else { r = Random.Range(3, 7); }
        // Calculate randomization factor "rVitIQ" for Vitality and IQ only
        if (characterLevel <= 10) { rVitIQ = 5; } else if (characterLevel % 4 == 0) { rVitIQ = Random.Range(7, 11); } else { rVitIQ = Random.Range(3, 7); }

        // Offense gain
        float offenseGain = Mathf.Round(((growthRates[0] * oldLevel) - ((offense - 2) * 10)) * r / 50);
        offense += (int)offenseGain;
        // Text.text = $"Offense went up by {offenseGain} points!"; if (offenseGain >= 3){Text.text += "Oh baby!";}

        // Defense gain
        float defenseGain = Mathf.Round(((growthRates[1] * oldLevel) - ((defense - 2) * 10)) * r / 50);
        defense += (int)defenseGain;
        // Text.text = $"Defense went up by {defenseGain} points!"; if (defenseGain >= 3){Text.text += "Oh baby!";}

        // Speed gain
        float speedGain = Mathf.Round(((growthRates[2] * oldLevel) - ((speed - 2) * 10)) * r / 50);
        speed += (int)speedGain;
        // Text.text = $"Speed went up by {speedGain} points!"; if (speedGain >= 3){Text.text += "Oh baby!";}

        // Guts gain
        float gutsGain = Mathf.Round(((growthRates[3] * oldLevel) - ((guts - 2) * 10)) * r / 50);
        guts += (int)gutsGain;
        // Text.text = $"Guts went up by {gutsGain} points!"; if (gutsGain >= 3){Text.text += "Oh baby!";}

        // Luck gain
        float luckGain = Mathf.Round(((growthRates[4] * oldLevel) - ((luck - 2) * 10)) * r / 50);
        luck += (int)luckGain;
        // Text.text = $"Luck went up by {luckGain} points!"; if (luckGain >= 3){Text.text += "Oh baby!";}

        // Vitality & HP Gain
        float vitGain = Mathf.Round(((growthRates[5] * oldLevel) - ((vitality - 2) * 10)) * rVitIQ / 50);
        vitality += (int)vitGain;
        int hpToGain = (int)(vitGain * 15);
        maxHP += hpToGain;
        // Text.text = $"Vitality went up by {vitGain} points!"; if (vitGain >= 3){Text.text += "Oh baby!";}
        // Text.text = $"Maximum HP went up by {hpToGain} points!"; if (hpToGain >= 20){Text.text += "Sweet!";}

        // IQ & PP Gain
        float iqGain = Mathf.Round(((growthRates[6] * oldLevel) - ((iq - 2) * 10)) * rVitIQ / 50);
        iq += (int)iqGain;
        // Text.text = "IQ went up by {iqGain} points!"; if (iqGain >= 3){Text.text += "Oh baby!";}
        // Jeff does not use PSI, so he does not gain PP from IQ
        if (characterName != CharacterName.Jeff)
        {
            int ppToGain = (int)(iqGain * 5);
            maxPP += ppToGain;
            // Text.text = "Maximum PP went up by {ppToGain} points!"; if (ppToGain >= 8){Text.text += "That rocks!";}
        }
    }
}
