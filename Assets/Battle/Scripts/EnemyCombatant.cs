using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyCombatant : Combatant
{
    private readonly string battleSpriteName;

    public EnemyCombatant(string name, int offense, int defense, int speed, int guts, int hitPoints, string battleSpriteName)
    : base(name, offense, defense, speed, guts, hitPoints)
    {
        this.battleSpriteName = battleSpriteName;
    }

    public string BattleSpriteName => battleSpriteName;
}

public class TestEnemyCombatant : EnemyCombatant
{
    public TestEnemyCombatant() : base(
        name: "Starman", 
        offense: 103,
        defense: 126,
        speed: 24,
        guts: 25,
        hitPoints: 545,
        battleSpriteName: "Starman")
    {
    }

    public override BattleAction AutoFight(IEnumerable<Combatant> combatants)
    {
        throw new System.NotImplementedException();
    }
}
