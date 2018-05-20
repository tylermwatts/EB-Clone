using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleDialogManager : MonoBehaviour 
{
	public GameObject dialogBoxGameObject;
	public GameObject battleMenuGameObject;
	public GameObject targetSelectionBoxGameObject;

	private BattleInfoBox battleInfoBox;

    void Start()
    {
		// Reset these game objects to the appropriate state for the start of the scene
        battleMenuGameObject.SetActive(false);
		dialogBoxGameObject.SetActive(true);
		targetSelectionBoxGameObject.SetActive(false);
    }

	public async void IntroduceEnemies(string[] enemyNames)
	{
		battleMenuGameObject.SetActive(false);
		dialogBoxGameObject.SetActive(true);

        battleInfoBox = dialogBoxGameObject.GetComponent<BattleInfoBox>();
		await battleInfoBox.TypeEncounteredEnemies(enemyNames);

		dialogBoxGameObject.SetActive(false);
		battleMenuGameObject.SetActive(true);
	}

	public void PromptForTargetSelection()
	{
		foreach (var button in battleMenuGameObject.GetComponentsInChildren<Button>())
		{
			button.enabled = false;
		}

		targetSelectionBoxGameObject.SetActive(true);
	}

	public void UpdateTargetText(string enemyName)
	{
		var text = targetSelectionBoxGameObject.GetComponentInChildren<Text>();
		text.text = $"To {enemyName}";
	}
}
