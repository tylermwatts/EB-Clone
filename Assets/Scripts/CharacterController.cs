using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {
	
	private Rigidbody2D characterRigidBody;

	// Use this for initialization
	void Start () {
		characterRigidBody = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.A)){
			transform.Translate(Vector3.left);
		}
		if (Input.GetKeyDown(KeyCode.W)){
			transform.Translate(Vector3.up);
		}
		if (Input.GetKeyDown(KeyCode.S)){
			transform.Translate(Vector3.down);
		}
		if (Input.GetKeyDown(KeyCode.D)){
			transform.Translate(Vector3.right);
		}
	}
}
