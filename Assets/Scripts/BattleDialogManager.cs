using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleDialogManager : MonoBehaviour 
{
	public GameObject dialogBoxGameObject;
	public GameObject battleMenuGameObject;

	private BattleDialogBox battleDialogBox;

    async void Start()
    {
        battleMenuGameObject.SetActive(false);
		dialogBoxGameObject.SetActive(true);

        battleDialogBox = dialogBoxGameObject.GetComponent<BattleDialogBox>();
		await battleDialogBox.TypeEncounteredEnemies(new string[] {"Andrew", "Sleepy", "Sneezy"});

		dialogBoxGameObject.SetActive(false);
		battleMenuGameObject.SetActive(true);
    }

	void Update () {
		
	}
}
