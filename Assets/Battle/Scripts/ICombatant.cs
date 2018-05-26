using System.Collections.Generic;

public interface ICombatant 
{
    int Speed { get; }
    string Name { get; }

    BattleAction AutoFight(IEnumerable<ICombatant> combatants);
}
