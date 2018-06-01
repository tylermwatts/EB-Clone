using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public abstract class Combatant 
{
    protected readonly int offense;
    protected readonly int guts;

    public Combatant(string name, int offense, int defense, int speed, int guts, int hitPoints)
    {
        Name = name;
        this.offense = offense;
        Defense = defense;
        Speed = speed;
        this.guts = guts;
        HitPoints = hitPoints;
    }

    public string Name { get; }
    public virtual int Offense => offense;
    public int Defense { get; }
    public int Speed { get; }
    public virtual int Guts => guts;
    public bool IsDefending { get; set; }
    public bool IsDazed { get; set; }
    public int HitPoints { get; set; }
    public bool IsImmobilized { get; set; }

    /// <summary>
    /// Call this at the start of a character's turn if the character is immobilized.
    /// </summary>
    /// <returns>True if immobilization was successfully broken.</returns>
    public bool AttemptToBreakImmobilization()
    {
        if (!IsImmobilized)
        {
            throw new InvalidOperationException();
        }

        var randomInt = Random.Range(0, 100);
        if (randomInt < 85)
        {
            IsImmobilized = false;
            return true;
        }

        return false;
    }

    public abstract BattleAction AutoFight(IEnumerable<Combatant> combatants);

    // The method below attempts to implement Physical Attack equations 
    // located here: http://starmen.net/mother2/gameinfo/technical/equations.php
    protected BattleActionResult GetPhysicalAttackResult(Combatant target, int accuracyOutOf16)
    {
        var hitChance = Random.Range(1, 17);
     
        if (IsDazed)
        {
            accuracyOutOf16 = accuracyOutOf16 / 2;
        }

        if (hitChance <= accuracyOutOf16)
        {
            var smashChance = Random.Range(1, 501);
            var smashOddsOutOf500 = Mathf.Max(Guts, 25);

            if (smashChance <= smashOddsOutOf500)
            {
                return BattleActionResult.Smash;
            }

            var dodgeChance = Random.Range(1, 501);
            var dodgeOddsOutOf500 = 2 * target.Speed - Speed;

            if (dodgeChance <= dodgeOddsOutOf500)
            {
                return BattleActionResult.Dodged;
            }

            return BattleActionResult.Successful;
        }

        return BattleActionResult.Failed;
    }

    // The method below attempts to implement Physical Attack equations 
    // located here: http://starmen.net/mother2/gameinfo/technical/equations.php
    protected int CalculatePhysicalAttackMagnitude(BattleAction battleAction)
    {
        if (battleAction?.Target == null)
        {
            throw new ArgumentNullException();
        }

        var magnitude = 0;
        switch (battleAction.Result)
        {
            case BattleActionResult.Smash:
                magnitude = 4 * Offense - battleAction.Target.Defense;
                break;
            case BattleActionResult.Successful:
                // The 2 here below is supposed to vary between 1 and 4 depending on something called "attack level",
                // but I can't figure out where that logic comes from.
                var damage = 2 * Offense - battleAction.Target.Defense;
                // I believe that, technically, the random range here should be normally distributed, 
                //but I don't know how to do that
                var randomModifier = Random.Range(-0.25f * damage, 0.25f * damage);
                magnitude = Mathf.RoundToInt(damage + randomModifier);
                break;
            default:
                return magnitude;
        }

        magnitude = Mathf.Max(0, magnitude);

        if (magnitude > 0 && battleAction.Target.IsDefending)
        {
            magnitude /= 2;
        }

        return magnitude;
    }
}
