using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsItem : Item {

	public int IncreaseDefenseBy {get;set;}

	public int IncreaseLuckBy {get;set;}

	/* 	Some Arms items also offer protection from Hypnosis. We may want to add
		an enum of possible status ailments, and add a "ProtectionFrom" property
		to items that protect from certain ailments.
	*/
}
