using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleEnemy : MonoBehaviour, ISubmitHandler, ISelectHandler
{
	BattleManager battleManager;
    BattleDialogManager dialogManager;
    Battlefield battlefield;

    EnemyCombatant enemy { get; set; }

    void Start() 
	{
		battleManager = FindObjectOfType<BattleManager>();
        dialogManager = FindObjectOfType<BattleDialogManager>();
        battlefield = GetComponentInParent<Battlefield>();
	}

    public void AssignEnemy(EnemyCombatant enemy)
    {
        this.enemy = enemy;
        var image = GetComponent<Image>();
        image.overrideSprite = Resources.Load<Sprite>(enemy.BattleSpriteName);
        image.SetNativeSize();
    }
    
    public void OnSelect(BaseEventData eventData)
    {
        dialogManager.UpdateTargetText(enemy.Name);
    }

    public async void OnSubmit(BaseEventData eventData)
    {
        dialogManager.ResetBattleMenu();
        battlefield.DeactivateBattleEnemies();
        await battleManager.OnEnemySelectedForBashingAsync(enemy);
    }
}
