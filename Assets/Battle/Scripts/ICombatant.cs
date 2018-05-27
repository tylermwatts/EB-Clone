using System.Collections.Generic;

public interface ICombatant 
{
    int Speed { get; }
    string Name { get; }
    int Offense { get; }
    int Defense { get; }
    bool IsDefending { get; set; }
    bool IsDazed { get; set; }
    int HitPoints { get; set; }

    BattleAction AutoFight(IEnumerable<ICombatant> combatants);
}
