using System;
using System.Collections.Generic;
using UnityEngine;

public class Character : ICombatant
{
    public int Speed { get; set; }

    public string Name { get; set; }

    public BattleAction AutoFight(IEnumerable<ICombatant> combatants)
    {
        // TODO flesh out
        Debug.Log("Character running AutoFight");
        return new BattleAction();
    }

    public BattleAction Bash()
    {
        // TODO flesh out
        Debug.Log("Character running Bash");
        return new BattleAction();
    }
}
