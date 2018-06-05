using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleEnemy : MonoBehaviour, ISubmitHandler, ISelectHandler
{
	BattleManager battleManager;
    BattleDialogManager dialogManager;
    Battlefield battlefield;

    public EnemyCombatant Enemy { get; private set; }

    void Start() 
	{
		battleManager = FindObjectOfType<BattleManager>();
        dialogManager = FindObjectOfType<BattleDialogManager>();
        battlefield = GetComponentInParent<Battlefield>();
	}

    public void AssignEnemy(EnemyCombatant enemy)
    {
        Enemy = enemy;
        var image = GetComponent<Image>();
        image.overrideSprite = Resources.Load<Sprite>(enemy.BattleSpriteName);
        image.SetNativeSize();
    }
    
    public void OnSelect(BaseEventData eventData)
    {
        dialogManager.UpdateTargetText(Enemy.Name);
    }

    public async void OnSubmit(BaseEventData eventData)
    {
        dialogManager.ResetBattleMenu();
        battlefield.DeactivateBattleEnemies();
        await battleManager.OnEnemySelectedForBashingAsync(Enemy);
    }
}
