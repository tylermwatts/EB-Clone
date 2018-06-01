using System.ComponentModel;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public abstract class EnemyCombatant : Combatant
{
    public EnemyCombatant(string name, int offense, int defense, int speed, int guts, int hitPoints, string battleSpriteName)
    : base(name, offense, defense, speed, guts, hitPoints)
    {
        BattleSpriteName = battleSpriteName;
    }

    public string BattleSpriteName { get; }

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

    protected CharacterCombatant SelectRandomCharacter(IEnumerable<CharacterCombatant> characters)
    {
        var randomIndex = Random.Range(0, characters.Count());
        return characters.ElementAt(randomIndex);
    }

    protected abstract BattleAction Action1(IEnumerable<Combatant> combatants);
    protected abstract BattleAction Action2(IEnumerable<Combatant> combatants);
    protected abstract BattleAction Action3(IEnumerable<Combatant> combatants);
    protected abstract BattleAction Action4(IEnumerable<Combatant> combatants);
}
