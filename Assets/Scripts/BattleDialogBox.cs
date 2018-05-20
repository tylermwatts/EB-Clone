using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BattleDialogBox : MonoBehaviour 
{
	[SerializeField]
	double letterDelay = 0.01;

	[SerializeField]
	double messageDelay = 2.0;

	[SerializeField]
	[Tooltip("Actual text box object must accommodate number of lines + 1")]
	int numberOfLinesToEnforce = 2;

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

	public async Task TypeEncounteredEnemies(string[] enemyNames)
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

		await AutoType(message);
	}

	public async Task TypeAttackAttempt(string characterName, string attackName)
	{
		textBox.text = string.Empty;
		var message = $"+ {characterName} tried {attackName}.";
		await AutoType(message);
	}

	async Task AutoType(string message)
	{
		for (int i = 0; i < message.Length; i++)
		{
			textBox.text += message[i];
			EnforceNumberOfLines();
			await Task.Delay(TimeSpan.FromSeconds(letterDelay));
		}
		await Task.Delay(TimeSpan.FromSeconds(messageDelay));
	}

    private void EnforceNumberOfLines()
    {
        Canvas.ForceUpdateCanvases();
		
		if (textBox.cachedTextGenerator.lines.Count > numberOfLinesToEnforce)
		{
			int startIndexOfSecondLine = textBox.cachedTextGenerator.lines[1].startCharIdx;
			textBox.text = textBox.text.Substring(startIndexOfSecondLine);
		}
    }
}
