using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponItem : Item {
	// public int accuracy;

	public int IncreaseOffenseBy {get;set;}

	public int IncreaseGutsBy {get;set;}

	public CharacterName EquippableBy {get;set;}

	// Not sure how accuracy is currently being assigned/used, but it could possibly work like this? Maybe.

	// public int Accuracy
	// {
	// 	get { return accuracy / 16; }
	// 	set	{ accuracy = value;	}
	// }
}
