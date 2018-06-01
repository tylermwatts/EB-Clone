using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Doorway : MonoBehaviour {

	public string scene;

	public void NextScene () {
		SceneManager.LoadScene(scene);
	}
}
