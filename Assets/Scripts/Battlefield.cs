using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Battlefield : MonoBehaviour 
{
	[SerializeField] GameObject battleEnemyPrefab;

	public void PopulateBattleField(IEnumerable<Enemy> enemies)
    {
        foreach (var enemy in enemies)
        {
            InstantiateBattleEnemy(enemy);
        }
    }

	private void InstantiateBattleEnemy(Enemy enemy)
    {
        var battleEnemyGameObject = Instantiate<GameObject>(battleEnemyPrefab);

        battleEnemyGameObject.transform.SetParent(transform, false);
        battleEnemyGameObject.transform.localScale = Vector3.one;

		var battleEnemy = battleEnemyGameObject.GetComponent<BattleEnemy>();
        battleEnemy.AssignEnemy(enemy);

		var button = battleEnemyGameObject.GetComponent<Button>();
        button.enabled = false;
    }

	public void ActivateBattleEnemiesForSelection()
    {
		var buttons = GetComponentsInChildren<Button>();
        foreach (var button in GetComponentsInChildren<Button>())
        {
            button.enabled = true;
        }

        EventSystem.current.SetSelectedGameObject(buttons[0].gameObject);
        buttons[0].OnSelect(new BaseEventData(EventSystem.current));
    }

	public void DeactivateBattleEnemies()
	{
        foreach (var button in GetComponentsInChildren<Button>())
        {
            button.enabled = false;
        }
	}
}
