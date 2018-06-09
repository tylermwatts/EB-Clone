using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NessGameObject : MonoBehaviour {

public static NessGameObject instance = null;

public void Awake ()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
		}
		DontDestroyOnLoad(gameObject);
	}
}
