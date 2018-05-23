using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupMenu : MonoBehaviour {

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
			menuIsOpen = !menuIsOpen;
			popUpMenu.SetActive(!popUpMenu.activeSelf);
		}
	}

	public void Inventory(){
		invMenu.SetActive(!invMenu.activeSelf);
	}
}
