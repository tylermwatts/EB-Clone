using System;
using System.Collections;
using System.Collections.Generic;
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

	void Start () 
	{
		dialogManager = dialogManagerGameObject.GetComponent<BattleDialogManager>();
		
		// TODO implement scenemanager through which enemies could be passed to this scene from the last scene
		// For now, hardcode enemies
		enemies = new Enemy[] 
		{ 
			new Enemy { Name = "Andrew", BattleSprite = null }, 
			new Enemy { Name = "Sleepy", BattleSprite = null }, 
			new Enemy { Name = "Sneezy", BattleSprite = null }
		};

		PopulateBattleField();
		IntroduceEnemies();
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

			var button = battleEnemyGameObject.GetComponent<Button>();
			button.enabled = false;
		}
    }

	private void IntroduceEnemies()
    {
		var enemyNames = new List<string>();
        foreach (var enemy in enemies)
		{
			enemyNames.Add(enemy.Name);
		}

		dialogManager.IntroduceEnemies(enemyNames.ToArray());
    }

    public void OnBashSelected()
	{
		dialogManager.PromptForTargetSelection();
		foreach (var button in battlefieldGameObject.GetComponentsInChildren<Button>())
		{
			button.enabled = true;
		}

		EventSystem.current.SetSelectedGameObject(battlefieldGameObject.GetComponentsInChildren<Button>()[0].gameObject);
		battlefieldGameObject.GetComponentsInChildren<Button>()[0].OnSelect(new BaseEventData(EventSystem.current));
	}

	public void OnEnemySelected()
	{
		Debug.Log("OnEnemySelected fired.");
	}
}
