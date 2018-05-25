using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour 
{
	void OnEnable () 
	{
		EventSystem.current.SetSelectedGameObject(GetComponentsInChildren<Button>()[0].gameObject);
		GetComponentsInChildren<Button>()[0].OnSelect(new BaseEventData(EventSystem.current));
	}
}
