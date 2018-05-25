using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class BattleManager : MonoBehaviour 
{
	[SerializeField] GameObject dialogManagerGameObject;
	[SerializeField] GameObject battleEnemyPrefab;
	[SerializeField] GameObject battlefieldGameObject;

	BattleDialogManager dialogManager;
	
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
	BattleActionType attackType;
	int characterIndex = 0;
	List<BattleAction> battleActions = new List<BattleAction>();

	async void Start ()
    {
        dialogManager = dialogManagerGameObject.GetComponent<BattleDialogManager>();

        ArrangeCombatants();
        PopulateBattleField();
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

    private void PopulateBattleField()
    {
        foreach (var enemy in enemies)
        {
            InstantiateBattleEnemy(enemy);
        }
    }

	private void InstantiateBattleEnemy(Enemy enemy)
    {
        var battleEnemyGameObject = (GameObject)Instantiate(battleEnemyPrefab);

        battleEnemyGameObject.transform.SetParent(battlefieldGameObject.transform, false);
        battleEnemyGameObject.transform.localScale = Vector3.one;

		var battleEnemy = battleEnemyGameObject.GetComponent<BattleEnemy>();
        battleEnemy.Name = enemy.Name;

		var image = battleEnemyGameObject.GetComponent<Image>();
        image.overrideSprite = Resources.Load<Sprite>(enemy.BattleSpriteName);

		var button = battleEnemyGameObject.GetComponent<Button>();
        button.enabled = false;
    }

	private void RunBattle()
    {
		characterIndex++;

		if (characterIndex <= characters.Length)
		{
			dialogManager.PromptForCharacterAction(characters[characterIndex - 1].Name);
		}
		else
		{
			DetermineEnemyActions();
			CarryOutActions();
			characterIndex = 0;
		}
    }

    private void DetermineEnemyActions()
    {
        throw new NotImplementedException();
    }

    private void CarryOutActions()
    {
        foreach (var action in battleActions)
		{

		}
    }

    public void OnBashSelected()
    {
        attackType = BattleActionType.Bash;
        dialogManager.PromptForTargetSelection();
        ActivateBattleEnemiesForSelection();
    }

    private void ActivateBattleEnemiesForSelection()
    {
        foreach (var button in battlefieldGameObject.GetComponentsInChildren<Button>())
        {
            button.enabled = true;
        }

        EventSystem.current.SetSelectedGameObject(battlefieldGameObject.GetComponentsInChildren<Button>()[0].gameObject);
        battlefieldGameObject.GetComponentsInChildren<Button>()[0].OnSelect(new BaseEventData(EventSystem.current));
    }

	public void OnDefendSelected()
	{
		throw new NotImplementedException();
	}

	public void OnAutoFightSelected()
	{
		var battleAction = characters[characterIndex].AutoFight(combatants);
		battleActions.Add(battleAction);
		RunBattle();
	}

	public void OnRunAwaySelected()
	{
		throw new NotImplementedException(); 
	}

    public void OnEnemySelectedForBashing(BattleEnemy battleEnemy)
	{
		// switch (attackType)
		// {
		// 	case BattleActionType.Bash:
		// 	Debug.Log($"{battleEnemy.Name} selected for bashing.");
		// 	break;
			
		// 	default:
		// 	break;
		// }

		RunBattle();
	}
}
