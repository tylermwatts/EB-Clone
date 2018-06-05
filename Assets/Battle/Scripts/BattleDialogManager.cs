using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class BattleDialogManager : MonoBehaviour 
{
	[SerializeField] GameObject battleInfoBoxGameObject;
	[SerializeField] GameObject battleMenuGameObject;
	[SerializeField] GameObject targetSelectionBoxGameObject;
	[SerializeField] Text characterNameText;
	BattleInfoBox battleInfoBox;
    Menu battleMenu;

    void Start()
    {
        battleInfoBox = battleInfoBoxGameObject.GetComponent<BattleInfoBox>();
        battleMenu = battleMenuGameObject.GetComponent<Menu>();

        // Reset these game objects to the appropriate state for the start of the scene
        ShowBattleOnlyInfoBox();
    }

    public void ShowBattleOnlyInfoBox()
    {
        battleMenuGameObject.SetActive(false);
        battleInfoBoxGameObject.SetActive(true);
        targetSelectionBoxGameObject.SetActive(false);
    }

    public void ResetBattleMenu()
	{
		battleMenuGameObject.SetActive(true);
		battleMenu.ActivateButtons();
		battleInfoBoxGameObject.SetActive(false);
		targetSelectionBoxGameObject.SetActive(false);
	}

	public async Task IntroduceEnemiesAsync(string[] enemyNames)
	{
		battleMenuGameObject.SetActive(false);
		battleInfoBoxGameObject.SetActive(true);

		await battleInfoBox.TypeEncounteredEnemiesAsync(enemyNames);
	}

	public void PromptForPlayerInput(string characterName)
	{
		battleInfoBoxGameObject.SetActive(false);
		battleMenuGameObject.SetActive(true);
        battleMenu.ActivateButtons();
        characterNameText.text = characterName;
	}

	public void PromptForTargetSelection()
	{
		battleMenu.DeactivateButtons();
		targetSelectionBoxGameObject.SetActive(true);
	}

	public void UpdateTargetText(string enemyName)
	{
		var text = targetSelectionBoxGameObject.GetComponentInChildren<Text>();
		text.text = $"To {enemyName}";
	}

	public async Task DisplayActionAttemptAsync(BattleAction battleAction)
	{
		await battleInfoBox.TypeBattleActionAttemptAsync(battleAction);
	}

	public async Task DisplayActionResultAsync(BattleAction battleAction)
	{
		// TODO kick off animation for result
		await battleInfoBox.TypeBattleActionResultAsync(battleAction);
	}

    public async Task DisplayImmobilizationUpdate(string characterName, bool brokeImmobilization)
    {
        await battleInfoBox.TypeImmobilizationUpdate(characterName, brokeImmobilization);
    }

	public async Task DisplayEnemyDefeated(string enemyName)
    {
        await battleInfoBox.TypeEnemyDefeated(enemyName);
    }
}
