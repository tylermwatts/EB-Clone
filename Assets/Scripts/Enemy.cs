using System.Collections.Generic;

public class Enemy : ICombatant
{
	public string Name { get; set; }
	public string BattleSpriteName { get; set; }
	public int Speed { get; set; }

    public BattleAction AutoFight(IEnumerable<ICombatant> combatants)
    {
        throw new System.NotImplementedException();
    }
}
