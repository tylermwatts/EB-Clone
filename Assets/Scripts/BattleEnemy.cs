using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleEnemy : MonoBehaviour, ISubmitHandler, ISelectHandler
{
	BattleManager battleManager;
    BattleDialogManager dialogManager;

    Enemy enemy { get; set; }

    void Awake () 
	{
		battleManager = FindObjectOfType<BattleManager>();
        dialogManager = FindObjectOfType<BattleDialogManager>();
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

    public void OnSubmit(BaseEventData eventData)
    {
        battleManager.OnEnemySelectedForBashing(this);
    }
}
