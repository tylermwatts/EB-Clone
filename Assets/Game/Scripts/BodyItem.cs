using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyItem : Item {

	public int IncreaseDefenseBy {get;set;}

	public int IncreaseSpeedBy {get;set;}

	public PermanentStatusAilment ProtectionFromPerm {get;set;}

	public InBattleStatusAilment ProtectionFromInBattle {get;set;}
}
