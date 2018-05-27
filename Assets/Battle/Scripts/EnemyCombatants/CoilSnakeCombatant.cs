using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoilSnakeCombatant : EnemyCombatant
{
    public CoilSnakeCombatant()
	: base("Coil Snake", offense: 3, defense: 4, speed: 2, guts: 0, hitPoints: 18, battleSpriteName:"coilsnake")
    {
    }

    protected override BattleAction Action1(IEnumerable<Combatant> combatants)
    {
        throw new System.NotImplementedException();
    }

    protected override BattleAction Action2(IEnumerable<Combatant> combatants)
    {
        throw new System.NotImplementedException();
    }

    protected override BattleAction Action3(IEnumerable<Combatant> combatants)
    {
        throw new System.NotImplementedException();
    }

    protected override BattleAction Action4(IEnumerable<Combatant> combatants)
    {
        throw new System.NotImplementedException();
    }
}
