using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BattleInfoBox : MonoBehaviour 
{
	[SerializeField]
	double letterDelay = 0.01;

	[SerializeField]
	double messageDelay = 1.0;

	[SerializeField] 
	string[] encounterPhrases = { "have encountered", "confront", "came upon" };

	[SerializeField] 
	string[] singleFriendPhrases = { "cohort", "friend", "buddy" };
	
	[SerializeField] 
	string[] pluralFriendPhrases = { "cohorts", "friends", "buddies" };

	Text textBox;

	void OnEnable()
	{
		textBox = GetComponentInChildren<Text>();
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

	public async Task TypeAttackAttemptAsync(string characterName, string attackName)
	{
		textBox.text = string.Empty;
		var message = $"+ {characterName} tried {attackName}.";
		await AutoTypeAsync(message);
	}

	async Task AutoTypeAsync(string message)
	{
		for (int i = 0; i < message.Length; i++)
		{
			textBox.text += message[i];
			await Task.Delay(TimeSpan.FromSeconds(letterDelay));
		}
		await Task.Delay(TimeSpan.FromSeconds(messageDelay));
	}
}
