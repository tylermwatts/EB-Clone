using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class StatusMenu : MonoBehaviour {

	[SerializeField] private Text characterName;
	[SerializeField] private Text level;
	[SerializeField] private Text offense;
	[SerializeField] private Text defense;
	[SerializeField] private Text speed;
	[SerializeField] private Text guts;
	[SerializeField] private Text vitality;
	[SerializeField] private Text iq;
	[SerializeField] private Text luck;
	[SerializeField] private Text hitPoints;
	[SerializeField] private Text psychicPoints;
	[SerializeField] private Text expPoints;
	[SerializeField] private Text statusCondition;
	[SerializeField] private Text expToNextLevel;

	private GameManager gameManager;
	private CharacterInfo displayedCharacter;


	void OnEnable () {
		gameManager = FindObjectOfType <GameManager>();
		displayedCharacter = gameManager.characters[CharacterName.Ness];
		FillInformation();
	}

	void Start () {
		
	}

    private void FillInformation()
    {
        characterName.text = displayedCharacter.characterName.ToString();
		level.text = displayedCharacter.CharacterLevel.ToString();
		offense.text = displayedCharacter.Offense.ToString();
		defense.text = displayedCharacter.Defense.ToString();
		speed.text = displayedCharacter.Speed.ToString();
		guts.text = displayedCharacter.Guts.ToString();
		vitality.text = displayedCharacter.Vitality.ToString();
		iq.text = displayedCharacter.IQ.ToString();
		luck.text = displayedCharacter.Luck.ToString();
		hitPoints.text = $"{displayedCharacter.CurrentHitPoints} / {displayedCharacter.MaxHitPoints}";
		psychicPoints.text = $"{displayedCharacter.CurrentPsychicPoints} / {displayedCharacter.MaxPsychicPoints}";
		expPoints.text = displayedCharacter.CurrentEXP.ToString();
		expToNextLevel.text = displayedCharacter.ExpToNextLevel.ToString();

		if (displayedCharacter.PermStatus == PermanentStatusAilment.Normal &&
			displayedCharacter.CoexistingStatus == CoexistingStatusAilment.Normal)
		{
			statusCondition.text = "Normal";
		}
		else if (displayedCharacter.PermStatus != PermanentStatusAilment.Normal &&
			displayedCharacter.CoexistingStatus == CoexistingStatusAilment.Normal)
		{
			statusCondition.text = $"{displayedCharacter.PermStatus}";
		}
		else if (displayedCharacter.PermStatus == PermanentStatusAilment.Normal &&
			displayedCharacter.CoexistingStatus != CoexistingStatusAilment.Normal)
		{
			statusCondition.text = $"{displayedCharacter.CoexistingStatus}";
		}
		else
		{
			statusCondition.text = $"{displayedCharacter.PermStatus}, {displayedCharacter.CoexistingStatus}";
		}

    }

    // Update is called once per frame
    void Update () {
		
	}
}
