using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyCombatant : ICombatant
{
    protected readonly string name;

    protected readonly int speed;

    protected readonly string battleSpriteName;

    public EnemyCombatant(string name, int speed, string battleSpriteName)
    {
        this.name = name;
        this.speed = speed;
        this.battleSpriteName = battleSpriteName;
    }

    public int Speed => speed;

    public string Name => name;

    public string BattleSpriteName => battleSpriteName;

    public virtual BattleAction AutoFight(IEnumerable<ICombatant> combatants)
    {
        throw new System.NotImplementedException();
    }
}

public class TestEnemyCombatant : EnemyCombatant
{
    public TestEnemyCombatant() : base("Starman", 0, "Starman")
    {
    }

    public override BattleAction AutoFight(IEnumerable<ICombatant> combatants)
    {
        throw new System.NotImplementedException();
    }
}
