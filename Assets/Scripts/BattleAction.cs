public class BattleAction
{
	public ICombatant Performer { get; set; }
	public ICombatant Target { get; set; }
	public BattleActionType BattleActionType { get; set; }
	public string ActionName { get; set; }
	public int Magnitude { get; set; }
	public bool Successful { get; set; }
}
