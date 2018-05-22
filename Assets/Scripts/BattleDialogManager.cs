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

    void Start()
    {
		battleInfoBox = battleInfoBoxGameObject.GetComponent<BattleInfoBox>();
		
		// Reset these game objects to the appropriate state for the start of the scene
        battleMenuGameObject.SetActive(false);
		battleInfoBoxGameObject.SetActive(true);
		targetSelectionBoxGameObject.SetActive(false);
	}

	public async Task IntroduceEnemiesAsync(string[] enemyNames)
	{
		battleMenuGameObject.SetActive(false);
		battleInfoBoxGameObject.SetActive(true);

		await battleInfoBox.TypeEncounteredEnemiesAsync(enemyNames);
	}

	public void PromptForCharacterAction(string characterName)
	{
		battleInfoBoxGameObject.SetActive(false);
		battleMenuGameObject.SetActive(true);
		characterNameText.text = characterName;
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
