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
	EnemyCombatant[] enemies = new EnemyCombatant[]
        {
            new CoilSnakeCombatant()
        };
	CharacterCombatant[] characters = new CharacterCombatant[] 
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

        ArrangeCombatants();
        battlefield.PopulateBattleField(enemies);
        await dialogManager.IntroduceEnemiesAsync(enemies.Select(e => e.Name).ToArray());
        await RunBattleAsync();
    }

    private void ArrangeCombatants()
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
                await GetPlayerInput();
            }
            else
            {
                await ExecuteBattleActions();
                Reset();
                await RunBattleAsync();
            }
        }
    }

    private async Task GetPlayerInput()
    {
        if (characters[characterIndex].Immobilized)
        {
            var immobilizationBreakAttemptSuccessful = characters[characterIndex].AttemptToBreakImmobilization();
            if (immobilizationBreakAttemptSuccessful)
            {
                await dialogManager.DisplayImmobilizationUpdate(characters[characterIndex].Name, immobilized: false);
                dialogManager.PromptForCharacterAction(characters[characterIndex].Name);
            }
            else
            {
                await dialogManager.DisplayImmobilizationUpdate(characters[characterIndex].Name, immobilized: true);
                await RunBattleAsync();
            }
        }
        else
        {
            dialogManager.PromptForCharacterAction(characters[characterIndex].Name);
        }
    }

    private async Task ExecuteBattleActions()
    {
        DetermineEnemyActions();
        foreach (var battleAction in battleActions)
        {
            await dialogManager.DisplayBattleInfoAsync(battleAction);
            battleAction.ApplyToTarget();
        }
    }

    private void Reset()
    {
        battleActions.Clear();
        characterIndex = -1;
    }

    // TODO Flesh out BattleIsStillWaging
    private bool BattleIsWaging()
    {
        return true;
    }

    private void DetermineEnemyActions()
    {
        foreach (var enemy in enemies)
        {
            if (enemy.Immobilized)
            {
                var immobilizationBreakBattleAction = new BattleAction
                {
                    Performer = enemy,
                    Target = enemy,
                    BattleActionType = BattleActionType.BreakImmobilization,
                };

                if (enemy.AttemptToBreakImmobilization())
                {
                    immobilizationBreakBattleAction.Result = BattleActionResult.Successful;
                }
                else
                {
                    immobilizationBreakBattleAction.Result = BattleActionResult.Failed;
                }

                battleActions.Add(immobilizationBreakBattleAction);
            }

            if (!enemy.Immobilized)
            {
                battleActions.Add(enemy.AutoFight(combatants));
            }
        }
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
