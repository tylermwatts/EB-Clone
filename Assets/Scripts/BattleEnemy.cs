using UnityEngine;
using UnityEngine.EventSystems;

public class BattleEnemy : MonoBehaviour, ISubmitHandler, ISelectHandler
{
	BattleManager battleManager;
    BattleDialogManager dialogManager;

    public string Name { get; set; }

    void Awake () 
	{
		battleManager = FindObjectOfType<BattleManager>();
        dialogManager = FindObjectOfType<BattleDialogManager>();
	}
    
    public void OnSelect(BaseEventData eventData)
    {
        dialogManager.UpdateTargetText(Name);
    }

    public void OnSubmit(BaseEventData eventData)
    {
        battleManager.OnEnemySelected(this);
    }
}
