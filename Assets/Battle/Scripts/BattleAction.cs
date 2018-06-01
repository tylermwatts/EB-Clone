using System;

public class BattleAction
{
	public Combatant Performer { get; set; }
	public Combatant Target { get; set; } // TODO Figure out how to handle AOE actions
	public BattleActionType BattleActionType { get; set; }
	public string ActionName { get; set; }
	public int Magnitude { get; set; }
	public BattleActionResult Result { get; set; }
}
