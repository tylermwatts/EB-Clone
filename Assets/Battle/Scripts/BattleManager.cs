using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using UnityEngine;

public class BattleManager : MonoBehaviour 
{
	[SerializeField] GameObject dialogManagerGameObject;
	[SerializeField] GameObject battleEnemyPrefab;
	[SerializeField] GameObject battlefieldGameObject;

	BattleDialogManager dialogManager;
	Battlefield battlefield;
	
	// TODO implement scenemanager through which enemies/characters could be passed to this scene from the last scene
	// For now, hardcode enemies and characters
	EnemyCombatant[] enemies = 
        {
            new CoilSnakeCombatant()
        };
	CharacterCombatant[] characters =  
		{ 
			new CharacterCombatant(CharacterName.Ness, offense: 2, defense: 2, speed: 2, guts: 2, hitPoints: 30), 
			new CharacterCombatant(CharacterName.Paula, offense: 2, defense: 2, speed: 2, guts: 2, hitPoints: 30) 
		};

	List<Combatant> combatants = new List<Combatant>();
	int characterIndex = -1;
	List<BattleAction> battleActions = new List<BattleAction>();

	async void Start ()
    {
        dialogManager = dialogManagerGameObject.GetComponent<BattleDialogManager>();
		battlefield = battlefieldGameObject.GetComponent<Battlefield>();

        AssembleCombatants();
        battlefield.PopulateBattleField(enemies);
        await dialogManager.IntroduceEnemiesAsync(enemies.Select(e => e.Name).ToArray());
        await RunBattleAsync();
    }

    private void AssembleCombatants()
    {
        combatants.AddRange(characters);
        combatants.AddRange(enemies);
        combatants = combatants.OrderBy(c => c.Speed).ToList();
    }

	private async Task RunBattleAsync()
    {
		if (BattleIsWaging())
		{
			characterIndex++;

			if (characterIndex < characters.Length)
            {
                dialogManager.PromptForPlayerInput(characters[characterIndex].Name);
            }
            else
            {
                dialogManager.ShowBattleOnlyInfoBox();
                ExecuteCombatantActions();
                Reset();

                await RunBattleAsync();
            }
        }
    }

    private void ExecuteCombatantActions()
    {
        foreach (var combatant in combatants)
        {
            if (combatant.IsImmobilized)
            {
                // Attempt to break
                // If broke
                    // Say so
                // Else
                    // Say so
            }

            if (!combatant.IsImmobilized)
            {
                if (combatant is CharacterCombatant)
                {
                    var battleAction = battleActions.Where(ba => ba.Performer == combatant).Single();
                    var character = (CharacterCombatant)combatant;
                    // await dialogManager.
                    // character.ResolveAction(battleAction);
                    // Display result
                    // Handle KO
                }
                else
                {
                    var enemy = (EnemyCombatant)combatant;
                    var battleAction = enemy.AutoFight(combatants);
                    //battleAction.ApplyToTarget(); // TODO HitPoints damage to CHARACTERs needs to happen over time, not all at once
                    // Display result
                }
            }
        }
    }

    private void Reset()
    {
        battleActions.Clear();
        characterIndex = -1;
    }

    // TODO Flesh out BattleIsStillWaging
    private bool BattleIsWaging()
    {
        return true;
    }

    public void OnBashSelected()
    {
        battlefield.ActivateBattleEnemiesForSelection();
		dialogManager.PromptForTargetSelection();
    }

	public async Task OnEnemySelectedForBashingAsync(EnemyCombatant enemy)
	{
		var battleAction = characters[characterIndex].Bash(enemy);
		battleActions.Add(battleAction);
        await RunBattleAsync();
	}

	public void OnDefendSelected()
	{
		// TODO flesh out OnDefendSelected
        Debug.Log("BattleManager running OnDefendSelected");
	}

	public async Task OnAutoFightSelectedAsync()
	{
		var battleAction = characters[characterIndex].AutoFight(combatants);
		battleActions.Add(battleAction);
        await RunBattleAsync();
	}

	public void OnRunAwaySelected()
	{
		// TODO flesh out OnRunAwaySelected
        Debug.Log("BattleManager running OnRunAwaySelected");
	}
}
