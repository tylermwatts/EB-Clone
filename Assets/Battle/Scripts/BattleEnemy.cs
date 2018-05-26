using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleEnemy : MonoBehaviour, ISubmitHandler, ISelectHandler
{
	BattleManager battleManager;
    BattleDialogManager dialogManager;
    Battlefield battlefield;

    Enemy enemy { get; set; }

    void Start() 
	{
		battleManager = FindObjectOfType<BattleManager>();
        dialogManager = FindObjectOfType<BattleDialogManager>();
        battlefield = GetComponentInParent<Battlefield>();
	}

    public void AssignEnemy(Enemy enemy)
    {
        this.enemy = enemy;
        var image = GetComponent<Image>();
        image.overrideSprite = Resources.Load<Sprite>(enemy.BattleSpriteName);
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
