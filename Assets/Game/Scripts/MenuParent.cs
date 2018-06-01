using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuParent : MonoBehaviour {

	public GameObject popUpMenu;
	public GameObject invMenu;

	public bool menuIsOpen = false;

	private GameManager gameManager;

	// Use this for initialization
	void Start () {
		popUpMenu.SetActive(false);
		invMenu.SetActive(false);
		gameManager = FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.M)){
			if (menuIsOpen){
				invMenu.SetActive(false);
			}
			gameManager.paused = !gameManager.paused;
			menuIsOpen = !menuIsOpen;
			popUpMenu.SetActive(!popUpMenu.activeSelf);
		}
	}

	public void OnInventorySelected()
    {
        invMenu.SetActive(!invMenu.activeSelf);
		DisablePopupMenuButtons();
    }

    private void DisablePopupMenuButtons()
    {
        foreach (var menuButton in popUpMenu.GetComponentsInChildren<Button>())
		{
			menuButton.enabled = false;
		}
    }
}
