using System.Collections.Generic;

public interface ICombatant 
{
	int Speed { get; set; }
	string Name { get; set; }
	BattleAction AutoFight(IEnumerable<ICombatant> combatants);
}
