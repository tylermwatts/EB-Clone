using System;
using System.Collections.Generic;

public class CharacterCombatant : Combatant
{
    private CharacterName characterName;

    public CharacterCombatant(CharacterName characterName, int offense, int defense, int speed, int guts, int hitPoints)
    : base(characterName.ToString(), offense, defense, speed, guts, hitPoints)
    {
        this.characterName = characterName;

        // TEST
        Weapon = new Weapon
        {
            ItemName = "Cracked Bat",
            ItemType = ItemType.Weapon,
            Offense = 4,
            Guts = 0,
            Accuracy = 15
        };
    }

    public override int Offense
    {
        get
        {
            var weaponOffense = Weapon?.Offense ?? 0;
            return offense + weaponOffense;
        }
    }

    public override int Guts
    {
        get
        {
            var weaponGuts = Weapon?.Guts ?? 0;
            return guts + weaponGuts;
        }
    }

    public Weapon Weapon { get; set; }

    public override BattleAction AutoFight(IEnumerable<Combatant> combatants)
    {
        // TODO flesh out 
        throw new NotImplementedException();
    }

    public BattleAction Bash(EnemyCombatant enemy)
    {
        var battleAction = new BattleAction
        {
            Performer = this,
            Target = enemy,
            BattleActionType = BattleActionType.Bash,
            ActionName = "Bash",
            Result = GetPhysicalAttackResult(enemy, Weapon?.Accuracy ?? 15)
        };

        battleAction.Magnitude = CalculatePhysicalAttackMagnitude(battleAction);

        return battleAction;
    }
}
