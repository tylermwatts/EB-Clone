using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneMachine {

	public static void LoadTheScene(string scene){
		SceneManager.LoadScene(scene);
	}

}
