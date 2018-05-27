using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyCombatant : ICombatant
{
    private readonly string name;
    private readonly int offense;
    private readonly int defense;
    private readonly int speed;
    private readonly int guts;
    private readonly string battleSpriteName;

    public EnemyCombatant(string name, int offense, int defense, int speed, int guts, int hitPoints, string battleSpriteName)
    {
        this.name = name;
        this.offense = offense;
        this.defense = defense;
        this.speed = speed;
        this.guts = guts;
        HitPoints = hitPoints;
        this.battleSpriteName = battleSpriteName;
    }

    public string Name => name;
    public int Offense => offense;
    public int Defense => defense;
    public int Speed => speed;
    public int Guts => guts;
    public string BattleSpriteName => battleSpriteName;
    public bool IsDefending { get; set; }
    public bool IsDazed { get; set; }
    public int HitPoints { get; set; }
    

    public virtual BattleAction AutoFight(IEnumerable<ICombatant> combatants)
    {
        throw new System.NotImplementedException();
    }
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

    public override BattleAction AutoFight(IEnumerable<ICombatant> combatants)
    {
        throw new System.NotImplementedException();
    }
}
