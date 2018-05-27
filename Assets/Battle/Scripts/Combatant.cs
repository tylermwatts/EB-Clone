using System.Collections.Generic;
using UnityEngine;

public abstract class Combatant 
{
    private readonly string name;
    protected readonly int offense;
    private readonly int defense;
    private readonly int speed;
    private readonly int guts;

    public Combatant(string name, int offense, int defense, int speed, int guts, int hitPoints)
    {
        this.name = name;
        this.offense = offense;
        this.defense = defense;
        this.speed = speed;
        this.guts = guts;
        HitPoints = hitPoints;
    }

    public string Name => name;
    public virtual int Offense => offense;
    public int Defense => defense;
    public int Speed => speed;
    public int Guts => guts;
    public bool IsDefending { get; set; }
    public bool IsDazed { get; set; }
    public int HitPoints { get; set; }

    public abstract BattleAction AutoFight(IEnumerable<Combatant> combatants);

    protected BattleActionResult GetPhysicalAttackResult(Combatant target, int weaponAccuracyOutOf16)
    {
        var hitChance = Random.Range(1, 17);
     
        if (IsDazed)
        {
            weaponAccuracyOutOf16 = weaponAccuracyOutOf16 / 2;
        }

        if (hitChance <= weaponAccuracyOutOf16)
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

            return BattleActionResult.Hit;
        }

        return BattleActionResult.Miss;
    }

    protected int CalculatePhysicalAttackMagnitude(BattleActionResult result, Combatant target)
    {
        var magnitude = 0;
        switch (result)
        {
            case BattleActionResult.Smash:
            magnitude = 4 * Offense - target.Defense;
            break;

            case BattleActionResult.Hit:
            // The 2 here below is supposed to vary between 1 and 4,
            // but I can't figure out where that logic comes from.
            var damage = 2 * Offense - target.Defense;
            var randomModifier = Random.Range(-0.25f * damage, 0.25f * damage);
            magnitude = Mathf.RoundToInt(damage + randomModifier);
            break;

            default:
            return magnitude;
        }

        magnitude = Mathf.Max(0, magnitude);

        if (magnitude > 0 && target.IsDefending)
        {
            magnitude /= 2;
        }

        return magnitude;
    }
}
