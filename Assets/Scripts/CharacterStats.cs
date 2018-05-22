using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour {

	public List <string> inventoryList;

	// 1 IQ point = 5 PP
	// Guts = critical hit chance
	// Luck = chance to hit and dodge
	// Vitality = determines max HP
	// Speed = Determines who goes first in battles, also raises dodge chance
	// Offense = damage dealt
	// Defense = damage reduction from PHYSICAL sources. Does not affect damage from PSI.
	private int iq, guts, luck, vitality, speed, offense, defense;
	private int characterLevel, nextLevel, exp, hp, pp;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
