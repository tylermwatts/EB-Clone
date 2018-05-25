using System;
using System.Collections.Generic;

public class Character : ICombatant
{
    public int Speed { get; set; }

    public string Name { get; set; }

    public BattleAction AutoFight(IEnumerable<ICombatant> combatants)
    {
        throw new NotImplementedException();
    }

    public BattleAction Bash()
    {
        throw new NotImplementedException();
    }
}
