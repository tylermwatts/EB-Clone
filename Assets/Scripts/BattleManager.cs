using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BattleManager : MonoBehaviour 
{
	public GameObject dialogManagerGameObject;
	public GameObject battleEnemyPrefab;
	public GameObject battlefieldGameObject;

	BattleDialogManager dialogManager;
	Enemy[] enemies;
	List<ICombatant> combatants = new List<ICombatant>();
	AttackType attackType;
	bool battleIsOver;

	async void Start () 
	{
		dialogManager = dialogManagerGameObject.GetComponent<BattleDialogManager>();
		
		// TODO implement scenemanager through which enemies could be passed to this scene from the last scene
		// For now, hardcode enemies
		enemies = new Enemy[] 
		{ 
			new Enemy { Name = "Andrew", BattleSpriteName = "Starman" }, 
			new Enemy { Name = "Sleepy", BattleSpriteName = "Starman" }, 
			new Enemy { Name = "Sneezy", BattleSpriteName = "Starman" }
		};
		// TODO Add friendly characters to the combatant list somehow
		combatants.Add(new Character { Name = "Ness" });
		combatants.AddRange(enemies);

		PopulateBattleField();
        await IntroduceEnemiesAsync();
		StartBattle();
	}

    private void PopulateBattleField()
    {
        foreach (var enemy in enemies)
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
    }

	private async Task IntroduceEnemiesAsync()
    {
		var enemyNames = new List<string>();
        foreach (var enemy in enemies)
		{
			enemyNames.Add(enemy.Name);
		}

		await dialogManager.IntroduceEnemiesAsync(enemyNames.ToArray());
    }

	private void StartBattle()
    {
		SortCombatantsByAttackOrder();
		dialogManager.PromptForCharacterAction(combatants.FirstOrDefault(c => c is Character)?.Name);
    }

    private void SortCombatantsByAttackOrder()
    {
        combatants.OrderBy(c => c.Speed);
    }

    public void OnBashSelected()
	{
		attackType = AttackType.Bash;

		dialogManager.PromptForTargetSelection();
		foreach (var button in battlefieldGameObject.GetComponentsInChildren<Button>())
		{
			button.enabled = true;
		}

		EventSystem.current.SetSelectedGameObject(battlefieldGameObject.GetComponentsInChildren<Button>()[0].gameObject);
		battlefieldGameObject.GetComponentsInChildren<Button>()[0].OnSelect(new BaseEventData(EventSystem.current));
	}

	public void OnEnemySelected(BattleEnemy battleEnemy)
	{
		switch (attackType)
		{
			case AttackType.Bash:
			Debug.Log($"{battleEnemy.Name} selected for bashing.");
			break;
			
			default:
			break;
		}
	}
}
