using System.Collections.Generic;
using System.Linq;

public class Character : ICombatant
{
    public int Speed { get; set; }

    public string Name { get; set; }

    public BattleAction AutoFight(IEnumerable<ICombatant> combatants)
    {
        // TODO use some formula to determine what action to do.
        var enemy = combatants.OfType<Enemy>().First();
        return Bash(enemy);
    }

    public BattleAction Bash(Enemy enemy)
    {
        var battleAction =  new BattleAction
        {
            Performer = this,
            Target = enemy,
            BattleActionType = BattleActionType.Bash,
            // The following is for testing purposes
            Successful = true,
            ActionName = "Bash",
            Magnitude = 10
        };

        return battleAction;
    }
}
