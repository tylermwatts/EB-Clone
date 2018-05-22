using UnityEngine;
using UnityEngine.UI;

public class TextBox : MonoBehaviour 
{
	[SerializeField]
	[Tooltip("Actual text box object must accommodate number of lines + 1")]
	int numberOfLinesToEnforce = 2;
	
	Text textBox;

	void Start () {
		textBox = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
		Canvas.ForceUpdateCanvases();
		
		if (textBox.cachedTextGenerator.lines.Count > numberOfLinesToEnforce)
		{
			int startIndexOfSecondLine = textBox.cachedTextGenerator.lines[1].startCharIdx;
			textBox.text = textBox.text.Substring(startIndexOfSecondLine);
		}
	}
}
