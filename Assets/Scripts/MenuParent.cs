using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuParent : MonoBehaviour {

	public GameObject popUpMenu;
	public GameObject invMenu;

	public bool menuIsOpen = false;

	// Use this for initialization
	void Start () {
		popUpMenu.SetActive(false);
		invMenu.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.M)){
			if (menuIsOpen){
				invMenu.SetActive(false);
				EnablePopupButtons();
			}
			menuIsOpen = !menuIsOpen;
			popUpMenu.SetActive(!popUpMenu.activeSelf);
		}
	}

	public void OnInventorySelected()
    {
        invMenu.SetActive(!invMenu.activeSelf);
        DisablePopupButtons();
    }

    private void DisablePopupButtons()
    {
        foreach (var button in popUpMenu.GetComponentsInChildren<Button>())
        {
            button.enabled = false;
        }
    }
    private void EnablePopupButtons()
    {
        foreach (var button in popUpMenu.GetComponentsInChildren<Button>())
        {
            button.enabled = true;
        }
    }
}
