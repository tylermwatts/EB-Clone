using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BattleMenu : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
	{
		EventSystem.current.SetSelectedGameObject(GetComponentsInChildren<Button>()[0].gameObject);
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}
}
