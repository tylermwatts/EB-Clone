using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour 
{
	void OnEnable() 
	{
		ActivateButtons();
	}

	public void ActivateButtons()
	{
		var buttons = GetComponentsInChildren<Button>();
		foreach (var button in buttons)
		{
			button.enabled = true;
		}
		EventSystem.current.SetSelectedGameObject(buttons[0].gameObject);
		buttons[0].OnSelect(new BaseEventData(EventSystem.current));
	}

	public void DeactivateButtons()
	{
		var buttons = GetComponentsInChildren<Button>();
		foreach (var button in buttons)
		{
			button.enabled = false;
		}
	}
}
