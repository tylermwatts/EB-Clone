using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class CharacterCombatant : ICombatant
{
    private readonly string name;
    private readonly int speed;
    private readonly int guts;
    private readonly int offense;
    private readonly int defense;

    public CharacterCombatant(string name, int offense, int defense, int speed, int guts)
    {
        this.name = name;
        this.offense = offense;
        this.defense = defense;
        this.speed = speed;
        this.guts = guts;
    }

    public int Speed => speed;
    public string Name => name;
    public int Guts => guts;
    public int Offense => offense + Weapon?.Offense ?? 0;
    public int Defense => defense;
    public Weapon Weapon { get; set; }
    public bool IsDefending { get; set; }
    public bool IsDazed { get; set; }
    public int HitPoints { get; set; }

    public abstract BattleAction AutoFight(IEnumerable<ICombatant> combatants);

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
            Result = GetBashResult(enemy)
        };

        battleAction.Magnitude = CalculateBashMagnitude(battleAction.Result, enemy);

        return battleAction;
    }

    private int CalculateBashMagnitude(BattleActionResult result, EnemyCombatant enemy)
    {
        var magnitude = 0;
        switch (result)
        {
            case BattleActionResult.Smash:
            magnitude = 4 * Offense - enemy.Defense;
            break;

            case BattleActionResult.Hit:
            // The 2 here below is supposed to vary between 1 and 4,
            // but I can't figure out where that logic comes from.
            var damage = 2 * Offense - enemy.Defense;
            var randomModifier = Random.Range(-0.25f * damage, 0.25f * damage);
            magnitude = Mathf.RoundToInt(damage + randomModifier);
            break;

            default:
            return magnitude;
        }

        magnitude = Mathf.Max(0, magnitude);

        if (magnitude > 0 && enemy.IsDefending)
        {
            magnitude /= 2;
        }

        return magnitude;
    }

    private BattleActionResult GetBashResult(EnemyCombatant enemy)
    {
        var hitChance = Random.Range(1, 17);
        var hitOddsOutOf16 = Weapon?.Accuracy ?? 15;
     
        if (IsDazed)
        {
            hitOddsOutOf16 = hitOddsOutOf16 / 2;
        }

        if (hitChance <= hitOddsOutOf16)
        {
            var smashChance = Random.Range(1, 501);
            var smashOddsOutOf500 = Mathf.Max(Guts, 25);

            if (smashChance <= smashOddsOutOf500)
            {
                return BattleActionResult.Smash;
            }

            var dodgeChance = Random.Range(1, 501);
            var dodgeOddsOutOf500 = 2 * enemy.Speed - Speed;

            if (dodgeChance <= dodgeOddsOutOf500)
            {
                return BattleActionResult.Dodged;
            }

            return BattleActionResult.Hit;
        }

        return BattleActionResult.Miss;
    }
}

public class TestCharacter : CharacterCombatant
{
    public TestCharacter() : base(
        name: "Ness", 
        offense: 2,
        defense: 2,
        speed: 2,
        guts: 2)
    {
    }

    public override BattleAction AutoFight(IEnumerable<ICombatant> combatants)
    {
        throw new NotImplementedException();
    }
}
