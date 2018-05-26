using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class CharacterCombatant : ICombatant
{
    protected readonly string name;

    protected readonly int speed;

    public CharacterCombatant(string name, int speed)
    {
        this.name = name;
        this.speed = speed;
    }

    public int Speed => speed;

    public string Name => name;

    public virtual BattleAction AutoFight(IEnumerable<ICombatant> combatants)
    {
        throw new System.NotImplementedException();
    }

    public virtual BattleAction Bash(EnemyCombatant enemy)
    {
        var battleAction = new BattleAction
        {
            Performer = this,
            Target = enemy,
            BattleActionType = BattleActionType.Bash
        };

        return battleAction;
    }

    private bool SuccessfulBash()
    {
        var randomInt = 0;
        //if ()
        Random.Range(0,16);
        return randomInt != 0;
    }
}

public class TestCharacter : CharacterCombatant
{
    public TestCharacter() : base("Ness",0)
    {
    }

    public override BattleAction AutoFight(IEnumerable<ICombatant> combatants)
    {
        throw new System.NotImplementedException();
    }

    public override BattleAction Bash(EnemyCombatant enemy)
    {
        var battleAction = base.Bash(enemy);

        return battleAction;
    }
}
