using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BattleInfoBox : MonoBehaviour 
{
	[SerializeField] double letterDelayInSeconds = 0.01;
	[SerializeField] double messageDelayInSeconds = 1.5;
	[SerializeField] string[] encounterPhrases = { "have encountered", "confront", "came upon" };
	[SerializeField] string[] singleFriendPhrases = { "cohort", "friend", "buddy" };
	[SerializeField] string[] pluralFriendPhrases = { "cohorts", "friends", "buddies" };
    [SerializeField] string[] attemptedPhrases = { "tried", "attempted" };
	Text textBox;
	bool skipAhead;

	void OnEnable()
	{
		textBox = GetComponentInChildren<Text>();
	}

	void Update()
	{
		if (Input.GetButtonDown("Submit"))
		{
			skipAhead = true;
		}
	}

	public async Task TypeEncounteredEnemiesAsync(string[] enemyNames)
	{
		if (enemyNames == null || enemyNames.Length == 0)
		{
			throw new ArgumentException("enemyNames must not be null and must be >= 1");
		}

		textBox.text = string.Empty;

		var encounterPhrase = encounterPhrases[Random.Range(0, encounterPhrases.Length)];
		var message = $"+ You {encounterPhrase} {enemyNames[0]}";
		if (enemyNames.Length == 1)
		{
			message += ".";
		}
		else
		{
			message += " and their";
			if (enemyNames.Length == 2)
			{
				var singleFriendPhrase = singleFriendPhrases[Random.Range(0, singleFriendPhrases.Length)];
				message += $" {singleFriendPhrase} {enemyNames[1]}.";
			}
			else
			{
				var pluralFriendPhrase = pluralFriendPhrases[Random.Range(0, pluralFriendPhrases.Length)];
				message += $" {pluralFriendPhrase}";
				for (int i = 1; i < enemyNames.Length; i++)
				{
					if (i == enemyNames.Length - 1)
					{
						message += $" and {enemyNames[i]}.";
					}
					else 
					{
						message += $" {enemyNames[i]}";
						if (enemyNames.Length != 3)
						{
							message += ",";
						}
					}
				}
			}
		}

		await AutoTypeAsync(message);
	}

	public async Task TypeBattleActionAttemptAsync(BattleAction battleAction)
	{
		textBox.text = string.Empty;
        var attemptedPhrase = attemptedPhrases[Random.Range(0, attemptedPhrases.Length)];
		await AutoTypeAsync($"+ {battleAction.Performer.Name} {attemptedPhrase} to {battleAction.ActionName} {battleAction.Target.Name}.");
	}

	public async Task TypeBattleActionResultAsync(BattleAction battleAction)
	{
        switch (battleAction.BattleActionType)
        {
            case BattleActionType.Bash:
                await TypeBashResultAsync(battleAction);
                break;
            case BattleActionType.Immobilize:
                await TypeImmobilizationResultAsync(battleAction);
                break;
        }
    }

    private async Task TypeBashResultAsync(BattleAction battleAction)
    {
        switch (battleAction.Result)
        {
            case BattleActionResult.Smash:
                await AutoTypeAsync($"\nSMAAAASH!!!!");
                goto case BattleActionResult.Successful;
            case BattleActionResult.Successful:
                await AutoTypeAsync($"\n+ {battleAction.Magnitude}HP damage to {battleAction.Target.Name}!");
                return;
            case BattleActionResult.Dodged:
                await AutoTypeAsync($"\n+ {battleAction.Target.Name} dodged!");
                return;
        }
    }

    private async Task TypeImmobilizationResultAsync(BattleAction battleAction)
    {
        if (battleAction.Result == BattleActionResult.Successful)
        {
            await AutoTypeAsync($"\n+ {battleAction.Target.Name} is immobilized!");
        }
        else
        {
            await AutoTypeAsync($"\n+ {battleAction.Performer.Name} failed!");
        }
    }

    public async Task TypeImmobilizationUpdate(string characterName, bool immobilized)
    {
        textBox.text = string.Empty;

        if (immobilized)
        {
            await AutoTypeAsync($"{characterName} is still immobilized.");
        }
        else
        {
            await AutoTypeAsync($"{characterName} broke immobilization!");
        }
    }

    async Task AutoTypeAsync(string message)
	{
		skipAhead = false;

		for (int i = 0; i < message.Length; i++)
		{
			textBox.text += message[i];
			if (!skipAhead)
			{
				await Task.Delay(TimeSpan.FromSeconds(letterDelayInSeconds));
			}
		}

		await Task.Delay(TimeSpan.FromSeconds(messageDelayInSeconds));
	}
}
