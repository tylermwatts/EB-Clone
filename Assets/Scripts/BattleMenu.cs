using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BattleMenu : MonoBehaviour 
{
	void Start () 
	{
		EventSystem.current.SetSelectedGameObject(GetComponentsInChildren<Button>()[0].gameObject);
	}
}
