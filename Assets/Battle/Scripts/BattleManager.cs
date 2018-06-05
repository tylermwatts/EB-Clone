using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using UnityEngine;

public class BattleManager : MonoBehaviour 
{
	[SerializeField] GameObject dialogManagerGameObject;
	[SerializeField] GameObject battleEnemyPrefab;
	[SerializeField] GameObject battlefieldGameObject;

	BattleDialogManager dialogManager;
	Battlefield battlefield;
	
	// TODO implement scenemanager through which enemies/characters could be passed to this scene from the last scene
	// For now, hardcode enemies and characters
	EnemyCombatant[] enemies = 
        {
            new CoilSnakeCombatant(),
            new CoilSnakeCombatant()
        };
	CharacterCombatant[] characters =  
		{ 
			new CharacterCombatant(CharacterName.Ness, offense: 2, defense: 2, speed: 2, guts: 2, hitPoints: 30), 
			new CharacterCombatant(CharacterName.Paula, offense: 2, defense: 2, speed: 2, guts: 2, hitPoints: 30) 
		};

	List<Combatant> combatants = new List<Combatant>();
	int characterIndex = -1;
	List<BattleAction> battleActions = new List<BattleAction>();

	async void Start ()
    {
        dialogManager = dialogManagerGameObject.GetComponent<BattleDialogManager>();
		battlefield = battlefieldGameObject.GetComponent<Battlefield>();

        AssembleCombatants();
        battlefield.PopulateBattleField(enemies);
        await dialogManager.IntroduceEnemiesAsync(enemies.Select(e => e.Name).ToArray());
        await RunBattleAsync();
    }

    private void AssembleCombatants()
    {
        combatants.AddRange(characters);
        combatants.AddRange(enemies);
        combatants = combatants.OrderBy(c => c.Speed).ToList();
    }

	private async Task RunBattleAsync()
    {
		if (BattleIsWaging())
		{
			characterIndex++;

			if (characterIndex < characters.Length)
            {
                dialogManager.PromptForPlayerInput(characters[characterIndex].Name);
            }
            else
            {
                dialogManager.ShowBattleOnlyInfoBox();
                await ExecuteCombatantActionsAsync();
                Reset();
                await RunBattleAsync();
            }
        }
        else
        {
            // TODO What happens when the fight is over?
        }
    }

    private bool BattleIsWaging()
    {
        // TODO Flesh out BattleIsWaging
        return true;
    }

    private async Task ExecuteCombatantActionsAsync()
    {
        foreach (var combatant in combatants)
        {
            if (combatant.HitPoints < 0)
            {
                continue;
            }

            if (combatant.IsImmobilized)
            {
                await dialogManager.DisplayImmobilizationUpdate(combatant.Name, combatant.AttemptToBreakImmobilization());
            }

            // If not immobilized, or not immobilized after break attempt
            if (!combatant.IsImmobilized)
            {
                if (combatant is CharacterCombatant)
                {
                    await ExecuteCharacterAction((CharacterCombatant)combatant);
                }
                else
                {
                    await ExecuteEnemyAction((EnemyCombatant)combatant);
                }
            }
        }
    }

    private async Task ExecuteCharacterAction(CharacterCombatant character)
    {
        var battleAction = battleActions.Find(ba => ba.Performer == character);

        VerifyTarget(battleAction);

        if (battleAction.Target != null)
        {
            await dialogManager.DisplayActionAttemptAsync(battleAction);
            character.ResolveAction(battleAction);
            await dialogManager.DisplayActionResultAsync(battleAction);

            if (battleAction.BattleActionType == BattleActionType.Bash &&
                battleAction.Target.HitPoints <= 0)
            {
                await dialogManager.DisplayEnemyDefeated(battleAction.Target.Name);
                var battleEnemies = battlefieldGameObject.GetComponentsInChildren<BattleEnemy>().ToList();
                var dyingEnemy = battleEnemies.Find(e => e.Enemy == battleAction.Target);
                Destroy(dyingEnemy.gameObject);
            }
        }
    }

    private void VerifyTarget(BattleAction battleAction)
    {
        if (battleAction.BattleActionType == BattleActionType.Bash &&
            battleAction.Target.HitPoints <= 0)
        {
            battleAction.Target = combatants.Find(c => c is EnemyCombatant && c.HitPoints > 0);
        }
    }

    private async Task ExecuteEnemyAction(EnemyCombatant enemy)
    {
        var battleAction = enemy.AutoFight(combatants);
        await dialogManager.DisplayActionAttemptAsync(battleAction);
        await dialogManager.DisplayActionResultAsync(battleAction);
        // TODO Handle impact to character hitpoints, etc.
    }

    private void Reset()
    {
        battleActions.Clear();
        characterIndex = -1;
    }

    public void OnBashSelected()
    {
        battlefield.ActivateBattleEnemiesForSelection();
		dialogManager.PromptForTargetSelection();
    }

	public async Task OnEnemySelectedForBashingAsync(EnemyCombatant enemy)
	{
		var battleAction = characters[characterIndex].Bash(enemy);
		battleActions.Add(battleAction);
        await RunBattleAsync();
	}

	public void OnDefendSelected()
	{
		// TODO flesh out OnDefendSelected
        Debug.Log("BattleManager running OnDefendSelected");
	}

	public async Task OnAutoFightSelectedAsync()
	{
		var battleAction = characters[characterIndex].AutoFight(combatants);
		battleActions.Add(battleAction);
        await RunBattleAsync();
	}

	public void OnRunAwaySelected()
	{
		// TODO flesh out OnRunAwaySelected
        Debug.Log("BattleManager running OnRunAwaySelected");
	}
}
