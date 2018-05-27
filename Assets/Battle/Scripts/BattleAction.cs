using System;

public class BattleAction
{
	public Combatant Performer { get; set; }
	public Combatant Target { get; set; } // TODO Figure out how to handle AOE actions
	public BattleActionType BattleActionType { get; set; }
	public string ActionName { get; set; }
	public int Magnitude { get; set; }
	public BattleActionResult Result { get; set; }

	public void ApplyToTarget()
	{
		if (Target == null)
		{
			throw new ArgumentNullException(nameof(Target));
		}

		switch (BattleActionType)
		{
			case BattleActionType.Bash:
			    Target.HitPoints -= Magnitude;
			    return;
            case BattleActionType.Immobilize:
                if (Result == BattleActionResult.Successful)
                {
                    Target.Immobilized = true;
                }
                return;
		}
	}
}
