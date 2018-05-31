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

    private readonly Dictionary <Stats, int> nessGrowthRates = new Dictionary <Stats, int>
    {
        {Stats.Offense, 18},
        {Stats.Defense, 5},
        {Stats.Speed, 4},
        {Stats.Guts, 7},
        {Stats.Luck, 6},
        {Stats.Vitality, 5},
        {Stats.IQ, 5}
    };
    
    private readonly Dictionary <Stats, int> paulaGrowthRates = new Dictionary <Stats, int>
    {
        {Stats.Offense, 12},
        {Stats.Defense, 3},
        {Stats.Speed, 8},
        {Stats.Guts, 5},
        {Stats.Luck, 5},
        {Stats.Vitality, 2},
        {Stats.IQ, 7}
    };

    private readonly Dictionary <Stats, int> jeffGrowthRates = new Dictionary <Stats, int>
    {
        {Stats.Offense, 10},
        {Stats.Defense, 6},
        {Stats.Speed, 5},
        {Stats.Guts, 5},
        {Stats.Luck, 4},
        {Stats.Vitality, 3},
        {Stats.IQ, 9}
    };
    
    private readonly Dictionary <Stats, int> pooGrowthRates = new Dictionary <Stats, int>
    {
        {Stats.Offense, 21},
        {Stats.Defense, 18},
        {Stats.Speed, 7},
        {Stats.Guts, 3},
        {Stats.Luck, 3},
        {Stats.Vitality, 4},
        {Stats.IQ, 4}
    };    

    // Use this for initialization
    void Start () {
		// Testing adding items to inventory

		Item item0 = new Item { ItemType = ItemType.Arms, ItemName = "Cheap Bracelet" };
		Item item1 = new HealingItem { ItemType = ItemType.Food, ItemName = "Bread Roll", RecoversHP = 8, NumberOfUses = 1 };
		Item item2 = new Item { ItemType = ItemType.Miscellaneous, ItemName = "For Sale Sign" };
		Item item3 = new Item { ItemType = ItemType.Weapon, ItemName = "Baseball Bat" };

		inventoryList.Add(item0);
		inventoryList.Add(item1);
		inventoryList.Add(item2);
		inventoryList.Add(item3);
	}

	void Update (){
		/* 	
            Still don't know if CharacterInfo actually needs an Update method. We can probably
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

    private void StatIncrease(Dictionary <Stats, int> growthRates, int oldLevel)
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
        int offenseGain = Mathf.RoundToInt(((growthRates[Stats.Offense] * oldLevel) - ((offense - 2) * 10)) * r / 50);
        offense += offenseGain;
        // Text.text = $"Offense went up by {offenseGain} points!"; if (offenseGain >= 3){Text.text += "Oh baby!";}

        // Defense gain
        int defenseGain = Mathf.RoundToInt(((growthRates[Stats.Defense] * oldLevel) - ((defense - 2) * 10)) * r / 50);
        defense += defenseGain;
        // Text.text = $"Defense went up by {defenseGain} points!"; if (defenseGain >= 3){Text.text += "Oh baby!";}

        // Speed gain
        int speedGain = Mathf.RoundToInt(((growthRates[Stats.Speed] * oldLevel) - ((speed - 2) * 10)) * r / 50);
        speed += speedGain;
        // Text.text = $"Speed went up by {speedGain} points!"; if (speedGain >= 3){Text.text += "Oh baby!";}

        // Guts gain
        int gutsGain = Mathf.RoundToInt(((growthRates[Stats.Guts] * oldLevel) - ((guts - 2) * 10)) * r / 50);
        guts += gutsGain;
        // Text.text = $"Guts went up by {gutsGain} points!"; if (gutsGain >= 3){Text.text += "Oh baby!";}

        // Luck gain
        int luckGain = Mathf.RoundToInt(((growthRates[Stats.Luck] * oldLevel) - ((luck - 2) * 10)) * r / 50);
        luck += luckGain;
        // Text.text = $"Luck went up by {luckGain} points!"; if (luckGain >= 3){Text.text += "Oh baby!";}

        // Vitality & HP Gain
        int vitGain = Mathf.RoundToInt(((growthRates[Stats.Vitality] * oldLevel) - ((vitality - 2) * 10)) * rVitIQ / 50);
        vitality += vitGain;
        int hpToGain = (vitGain * 15);
        maxHP += hpToGain;
        // Text.text = $"Vitality went up by {vitGain} points!"; if (vitGain >= 3){Text.text += "Oh baby!";}
        // Text.text = $"Maximum HP went up by {hpToGain} points!"; if (hpToGain >= 20){Text.text += "Sweet!";}

        // IQ & PP Gain
        int iqGain = Mathf.RoundToInt(((growthRates[Stats.IQ] * oldLevel) - ((iq - 2) * 10)) * rVitIQ / 50);
        iq += iqGain;
        // Text.text = "IQ went up by {iqGain} points!"; if (iqGain >= 3){Text.text += "Oh baby!";}
        // Jeff does not use PSI, so he does not gain PP from IQ
        if (characterName != CharacterName.Jeff)
        {
            int ppToGain = (iqGain * 5);
            maxPP += ppToGain;
            // Text.text = "Maximum PP went up by {ppToGain} points!"; if (ppToGain >= 8){Text.text += "That rocks!";}
        }
    }
}
