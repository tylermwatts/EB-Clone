using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleAction
{
	public ICombatant Performer { get; set; }
	public ICombatant Target { get; set; }
	public BattleActionType MyProperty { get; set; }
	public string ActionName { get; set; }
	public int ActionMagnitude { get; set; }
}
