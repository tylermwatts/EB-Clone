using System;
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
	Enemy[] enemies = new Enemy[]
        {
            new Enemy { Name = "Andrew", BattleSpriteName = "Starman" },
            new Enemy { Name = "Sleepy", BattleSpriteName = "Starman" },
            new Enemy { Name = "Sneezy", BattleSpriteName = "Starman" }
        };
	Character[] characters = new Character[] { new Character { Name = "Ness" }, new Character { Name = "Paula" } };

	IEnumerable<ICombatant> combatants;
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
        var combatantsList = new List<ICombatant>();
        combatantsList.AddRange(characters);
        combatantsList.AddRange(enemies);
		combatants = combatantsList.OrderBy(c => c.Speed);
    }

	private async Task RunBattleAsync()
    {
		if (BattleIsWaging())
		{
			characterIndex++;

			if (characterIndex < characters.Length)
			{
				dialogManager.PromptForCharacterAction(characters[characterIndex].Name);
			}
			else
			{
				DetermineEnemyActions();
                await dialogManager.DisplayBattleInfoAsync(battleActions);
				battleActions.Clear();
				characterIndex = -1;
                await RunBattleAsync();
			}
		}
		
    }

	// TODO Flesh out BattleIsStillWaging
    private bool BattleIsWaging()
    {
        return true;
    }

    private void DetermineEnemyActions()
    {
        // TODO flesh out DetermineEnemyActions
        Debug.Log("BattleManager running DetermineEnemyActions");
    }

    public void OnBashSelected()
    {
        battlefield.ActivateBattleEnemiesForSelection();
		dialogManager.PromptForTargetSelection();
    }

	public async Task OnEnemySelectedForBashingAsync(Enemy enemy)
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
