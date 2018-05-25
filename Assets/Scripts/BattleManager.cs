using System;
using System.Collections.Generic;
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
		RunBattle();
    }

    private void ArrangeCombatants()
    {
        var combatantsList = new List<ICombatant>();
        combatantsList.AddRange(characters);
        combatantsList.AddRange(enemies);
		combatants = combatantsList.OrderBy(c => c.Speed);
    }

	private void RunBattle()
    {
		if (BattleIsStillWaging())
		{
			characterIndex++;

			if (characterIndex < characters.Length)
			{
				dialogManager.PromptForCharacterAction(characters[characterIndex].Name);
			}
			else
			{
				DetermineEnemyActions();
				CarryOutActions();
				battleActions.Clear();
				characterIndex = -1;
				RunBattle();
			}
		}
		
    }

	// TODO Rename this and flesh it out
    private bool BattleIsStillWaging()
    {
        return true;
    }

    private void DetermineEnemyActions()
    {
        // TODO flesh out
        Debug.Log("BattleManager running DetermineEnemyActions");
    }

    private void CarryOutActions()
    {
        foreach (var action in battleActions)
		{
			// TODO flesh out
        	Debug.Log("BattleManager running CarryOutActions per action");
		}
    }

    public void OnBashSelected()
    {
        battlefield.ActivateBattleEnemiesForSelection();
		dialogManager.PromptForTargetSelection();
    }

	public void OnDefendSelected()
	{
		// TODO flesh out
        Debug.Log("BattleManager running OnDefendSelected");
	}

	public void OnAutoFightSelected()
	{
		var battleAction = characters[characterIndex].AutoFight(combatants);
		battleActions.Add(battleAction);
		RunBattle();
	}

	public void OnRunAwaySelected()
	{
		// TODO flesh out
        Debug.Log("BattleManager running OnRunAwaySelected");
	}

    public void OnEnemySelectedForBashing(BattleEnemy battleEnemy)
	{
		var battleAction = characters[characterIndex].Bash();
		battleActions.Add(battleAction);
		RunBattle();
	}
}
