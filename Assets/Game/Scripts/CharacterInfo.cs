﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class CharacterInfo : MonoBehaviour {

	public List <Item> inventoryList = new List<Item>();
	public CharacterName characterName;
	public ExpChart expChart;

	// More information on character stats https://github.com/warpfox/EB-Clone/wiki/Character-Stats
	[SerializeField] private int iq, guts, luck, vitality, speed, offense, defense;
    private Dictionary <Stats, int> stats = new Dictionary <Stats, int> ();

	[SerializeField] private int characterLevel, currentEXP, maxHP, maxPP, currentHP, currentPP;
    
    private Dictionary <Stats, int> GetGrowthRates()
    {
        switch (characterName)
        {
            case CharacterName.Ness:
                return new Dictionary<Stats, int>
                {
                    { Stats.Offense, 18 },
                    { Stats.Defense, 5 },
                    { Stats.Speed, 4 },
                    { Stats.Guts, 7 },
                    { Stats.Luck, 6 },
                    { Stats.Vitality, 5 },
                    { Stats.IQ, 5 }
                };
            case CharacterName.Paula:
                return new Dictionary<Stats, int>
                {
                    { Stats.Offense, 12 },
                    { Stats.Defense, 3 },
                    { Stats.Speed, 8 },
                    { Stats.Guts, 5 },
                    { Stats.Luck, 5 },
                    { Stats.Vitality, 2 },
                    { Stats.IQ, 7 }
                };
            case CharacterName.Jeff:
                return new Dictionary<Stats, int>
                {
                    { Stats.Offense, 10 },
                    { Stats.Defense, 6 },
                    { Stats.Speed, 5 },
                    { Stats.Guts, 5 },
                    { Stats.Luck, 4 },
                    { Stats.Vitality, 3 },
                    { Stats.IQ, 9 }
                };
            case CharacterName.Poo:
                return new Dictionary<Stats, int>
                {
                    { Stats.Offense, 21 },
                    { Stats.Defense, 18 },
                    { Stats.Speed, 7 },
                    { Stats.Guts, 3 },
                    { Stats.Luck, 3 },
                    { Stats.Vitality, 4 },
                    { Stats.IQ, 4 }
                };
            default:
                throw new InvalidEnumArgumentException();
        }
    }

    void Awake () {
        DontDestroyOnLoad(this.gameObject);
        SceneManager.sceneLoaded += OnLevelLoaded;
    }

    // Use this for initialization
    void Start () {

        stats.Add(Stats.Offense, offense);
        stats.Add(Stats.Defense, defense);
        stats.Add(Stats.Speed, speed);
        stats.Add(Stats.Guts, guts);
        stats.Add(Stats.Luck, luck);
        stats.Add(Stats.Vitality, vitality);
        stats.Add(Stats.IQ, iq);

		// Testing adding items to inventory
        var items = new List<Item>
        {
            new Item { ItemType = ItemType.Arms, ItemName = "Cheap Bracelet" },
            new HealingItem { ItemType = ItemType.Food, ItemName = "Bread Roll", RecoversHP = 8, NumberOfUses = 1 },
            new Item { ItemType = ItemType.Miscellaneous, ItemName = "For Sale Sign" },
            new Item { ItemType = ItemType.Weapon, ItemName = "Baseball Bat" },
        };

		inventoryList.AddRange(items);
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

        IncreaseStats(GetGrowthRates(), oldLevel);
    }

    private void IncreaseStats(Dictionary <Stats, int> growthRates, int oldLevel)
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
        if (characterLevel % 4 == 0) 
        { 
            r = Random.Range(7, 11); 
        } 
        else 
        { 
            r = Random.Range(3, 7); 
        }

        // Calculate randomization factor "rVitIQ" for Vitality and IQ only
        if (characterLevel <= 10) 
        { 
            rVitIQ = 5; 
        } 
        else if (characterLevel % 4 == 0) 
        { 
            rVitIQ = Random.Range(7, 11); 
        } 
        else 
        { 
            rVitIQ = Random.Range(3, 7); 
        }

        foreach (Stats Stats in Enum.GetValues(typeof(Stats)))
        {
            var gain = 0;

            if (Stats == Stats.Vitality || Stats == Stats.IQ)
            {
                gain = Mathf.RoundToInt(((growthRates[Stats] * oldLevel) - ((stats[Stats] - 2) * 10)) * rVitIQ / 50);

                if (Stats == Stats.Vitality)
                {
                    var hpToGain = gain * 15;
                    maxHP += hpToGain;
                    // Text.text = $"Maximum HP went up by {hpToGain} points!"; if (hpToGain >= 20){Text.text += "Sweet!";}
                }
                else if (characterName != CharacterName.Jeff)
                {
                    var ppToGain = gain * 5;
                    maxPP += ppToGain;
                    // Text.text = "Maximum PP went up by {ppToGain} points!"; if (ppToGain >= 8){Text.text += "That rocks!";}
                }
            }
            else
            {
                gain = Mathf.RoundToInt(((growthRates[Stats] * oldLevel) - ((stats[Stats] - 2) * 10)) * r / 50);
            }

            stats[Stats] += gain;
            // Text.text = $"{Stats.ToString()} went up by {gain} points!"; if (gain >= 3){Text.text += "Oh baby!";}
        }
    }

    private void OnLevelLoaded (Scene scene, LoadSceneMode lsMode){
        var spawnPoint = GameObject.Find("Spawnpoint");
        transform.position = spawnPoint.transform.position;
    }
}