using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class CharacterCombatant : Combatant
{
    private CharacterName characterName;

    public CharacterCombatant(CharacterName characterName, int offense, int defense, int speed, int guts, int hitPoints)
    : base(characterName.ToString(), offense, defense, speed, guts, hitPoints)
    {
        this.characterName = characterName;
    }

    public override int Offense => offense + Weapon?.Offense ?? 0;
    public Weapon Weapon { get; set; }

    public override BattleAction AutoFight(IEnumerable<Combatant> combatants)
    {
        throw new NotImplementedException();
    }

    // The method below attempts to implement Physical Attack equations 
    // located here: http://starmen.net/mother2/gameinfo/technical/equations.php
    public BattleAction Bash(EnemyCombatant enemy)
    {
        var battleAction = new BattleAction
        {
            Performer = this,
            Target = enemy,
            BattleActionType = BattleActionType.Bash,
            ActionName = "Bash",
            Result = GetBashResult(enemy, Weapon?.Accuracy ?? 15)
        };

        battleAction.Magnitude = CalculateBashMagnitude(battleAction.Result, enemy);

        return battleAction;
    }
}
