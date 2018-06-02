using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterMover : MonoBehaviour 
{
	public float playerSpeed = 5.0f;

	private Vector3 targetPosition;
	private Animator bodyAnimator;
	private MenuParent menuParent;

	void Awake (){
		SceneManager.sceneLoaded += OnLevelLoaded;
	}

	void Start () 
	{
		SnapToGrid();
		targetPosition = transform.position;
		bodyAnimator = GetComponentInParent<Animator>();
		menuParent = GameObject.FindObjectOfType<MenuParent>();
	}

    private void SnapToGrid()
    {
        transform.position = new Vector3(
			Mathf.Round(transform.position.x),
			Mathf.Round(transform.position.y),
			transform.position.z);
    }

    void Update () 
	{
		if (menuParent.menuIsOpen == false){
			UpdateTarget();
			MoveToTarget();
			Animate();
		}
		
	}

    private void Animate()
    {
		float xDistanceToTarget = targetPosition.x - transform.position.x;
		float yDistanceToTarget = targetPosition.y - transform.position.y;
		if (xDistanceToTarget > 0 || Input.GetAxis("Horizontal") == 1)
		{
			bodyAnimator.SetInteger("horizontalMovement", 1);
			bodyAnimator.SetInteger("verticalMovement", 0);
		}
		else if (xDistanceToTarget < 0 || Input.GetAxis("Horizontal") == -1)
		{
			bodyAnimator.SetInteger("horizontalMovement", -1);
			bodyAnimator.SetInteger("verticalMovement", 0);
		}
		else if (yDistanceToTarget > 0 || Input.GetAxis("Vertical") == 1)
		{
			bodyAnimator.SetInteger("horizontalMovement", 0);
			bodyAnimator.SetInteger("verticalMovement", 1);
		}
		else if (yDistanceToTarget < 0 || Input.GetAxis("Vertical") == -1)
		{
			bodyAnimator.SetInteger("horizontalMovement", 0);
			bodyAnimator.SetInteger("verticalMovement", -1);
		}
		else 
		{
			bodyAnimator.SetInteger("horizontalMovement", 0);
			bodyAnimator.SetInteger("verticalMovement", 0);
		}
    }

    private void UpdateTarget()
    {
		var potentialTarget = targetPosition;

        if (Input.GetAxis("Horizontal") == 1)
		{
			potentialTarget = new Vector3(
				Mathf.Floor(transform.position.x + 1), 
				potentialTarget.y, 
				transform.position.z);
		} else if (Input.GetAxis("Horizontal") == -1)
		{
			potentialTarget = new Vector3(
				Mathf.Ceil(transform.position.x - 1), 
				potentialTarget.y, 
				transform.position.z);
		}
		
		if (Input.GetAxis("Vertical") == 1)
		{
			potentialTarget = new Vector3(
				potentialTarget.x, 
				Mathf.Floor(transform.position.y + 1), 
				transform.position.z);
		} else if (Input.GetAxis("Vertical") == -1)
		{
			potentialTarget = new Vector3(
				potentialTarget.x, 
				Mathf.Ceil(transform.position.y - 1), 
				transform.position.z);
		}

		var collider = CheckPathCollider(potentialTarget);

		if (collider == null)
		{
			targetPosition = potentialTarget;
		}
		else if (collider.tag == "Door")
		{
			collider.GetComponent<Doorway>().NextScene();
		}
    }

	private Collider2D CheckPathCollider(Vector3 target)
    {
		var direction = target - transform.position;
		var distance = Vector2.Distance(transform.position, target);
		var collider = Physics2D.Raycast(transform.position, direction, distance).collider;

		return collider;
    }
	private void MoveToTarget()
    {
		transform.position = Vector3.MoveTowards(
			transform.position, 
			targetPosition, 
			playerSpeed * Time.deltaTime);
    }

	private void OnLevelLoaded (Scene scene, LoadSceneMode lsMode){
        var spawnPoint = GameObject.Find("Spawnpoint");
        transform.position = spawnPoint.transform.position;
		targetPosition = spawnPoint.transform.position;
    }

}
