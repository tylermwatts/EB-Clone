using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopupMenu : MonoBehaviour {

	public GameObject popUpMenu;

	// Use this for initialization
	void Start () {
		popUpMenu.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.M)){
			popUpMenu.SetActive(!popUpMenu.activeSelf);
		}
	}
}
