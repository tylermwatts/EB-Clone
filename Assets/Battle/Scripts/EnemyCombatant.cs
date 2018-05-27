using System.ComponentModel;
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

    public override BattleAction AutoFight(IEnumerable<Combatant> combatants)
    {
        var randomInt = Random.Range(1,5);
        switch (randomInt)
        {
            case 1:
            return Action1(combatants);
            case 2:
            return Action2(combatants);
            case 3:
            return Action3(combatants);
            case 4:
            return Action4(combatants);
            default:
            throw new InvalidEnumArgumentException();
        }
    }

    protected abstract BattleAction Action1(IEnumerable<Combatant> combatants);
    protected abstract BattleAction Action2(IEnumerable<Combatant> combatants);
    protected abstract BattleAction Action3(IEnumerable<Combatant> combatants);
    protected abstract BattleAction Action4(IEnumerable<Combatant> combatants);
}
