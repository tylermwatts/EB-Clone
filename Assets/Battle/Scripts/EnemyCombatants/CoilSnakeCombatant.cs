using System.Collections.Generic;
using System.Linq;

public class CoilSnakeCombatant : EnemyCombatant
{
    public CoilSnakeCombatant()
	: base("Coil Snake", offense: 3, defense: 4, speed: 2, guts: 0, hitPoints: 18, battleSpriteName:"coilsnake")
    {
    }

    protected override BattleAction Action1(IEnumerable<Combatant> combatants)
    {
        // Biting Attack
        var target = SelectRandomCharacter(combatants.OfType<CharacterCombatant>());

        var battleAction = new BattleAction
        {
            Performer = this,
            Target = target,
            BattleActionType = BattleActionType.Bash,
            ActionName = "Bite",
            Result = GetPhysicalAttackResult(target, 15)
        };

        if (battleAction.Result == BattleActionResult.Successful || battleAction.Result == BattleActionResult.Smash)
        {
            battleAction.Magnitude = CalculatePhysicalAttackMagnitude(battleAction);

            // TODO figure out how to handle asynchronous damage to Characters
            target.HitPoints -= battleAction.Magnitude;
        }

        return battleAction;
    }

    protected override BattleAction Action2(IEnumerable<Combatant> combatants)
    {
        return Action1(combatants);
    }

    protected override BattleAction Action3(IEnumerable<Combatant> combatants)
    {
        return Action1(combatants);
    }

    protected override BattleAction Action4(IEnumerable<Combatant> combatants)
    {
        // Coil around target and attack
        var target = SelectRandomCharacter(combatants.OfType<CharacterCombatant>());

        var battleAction = new BattleAction
        {
            Performer = this,
            Target = target,
            BattleActionType = BattleActionType.Immobilize,
            ActionName = "Coil",
            Result = BattleActionResult.Successful
        };

        target.IsImmobilized = true;

        return battleAction;
    }
}
