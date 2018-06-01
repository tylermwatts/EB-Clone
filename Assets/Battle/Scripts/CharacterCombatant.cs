using System;
using System.Collections.Generic;

public class CharacterCombatant : Combatant
{
    private CharacterName characterName;

    public CharacterCombatant(CharacterName characterName, int offense, int defense, int speed, int guts, int hitPoints)
    : base(characterName.ToString(), offense, defense, speed, guts, hitPoints)
    {
        this.characterName = characterName;

        // START TEST
        Weapon = new WeaponItem("Cracked Bat", ItemType.Weapon, offense: 4, guts: 0, accuracy: 15);
        // END TEST
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

    public WeaponItem Weapon { get; set; }

    public override BattleAction AutoFight(IEnumerable<Combatant> combatants)
    {
        // TODO flesh out 
        throw new NotImplementedException();
    }

    public void ResolveAction(BattleAction action)
    {
        switch (action.BattleActionType)
        {
            case BattleActionType.Bash:
                action.Result = GetPhysicalAttackResult(action.Target, Weapon?.Accuracy ?? 15);
                if (action.Result == BattleActionResult.Successful || action.Result == BattleActionResult.Smash)
                {
                    action.Magnitude = CalculatePhysicalAttackMagnitude(action);
                    action.Target.HitPoints -= action.Magnitude;
                }
                break;
        }
    }

    public BattleAction Bash(EnemyCombatant enemy)
    {
        var battleAction = new BattleAction
        {
            Performer = this,
            Target = enemy,
            BattleActionType = BattleActionType.Bash,
            ActionName = "Bash"
        };

        return battleAction;
    }
}
