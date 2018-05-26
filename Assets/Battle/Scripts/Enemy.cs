using System.Collections.Generic;
using UnityEngine;

public class Enemy : ICombatant
{
	public string Name { get; set; }
	public string BattleSpriteName { get; set; }
	public int Speed { get; set; }

    public BattleAction AutoFight(IEnumerable<ICombatant> combatants)
    {
        // TODO flesh out AutoFight
        Debug.Log("Enemy running AutoFight");
        return new BattleAction();
    }
}
