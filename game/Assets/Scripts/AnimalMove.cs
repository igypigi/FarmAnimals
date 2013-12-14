using UnityEngine;
using System.Collections;

public class AnimalMove : MonoBehaviour {
	private NavMeshAgent agent;
	private Vector3 firstPosition;
	public Animal animal;

	// If animals should move this variable should be set
	private Vector3 moveToPoint;
	private bool moveNavmesh = false;
	private bool moveRandom = false;
	private bool running = false;
	private bool walking = false;

	// Use this for initialization
	void Start () {
		agent = gameObject.GetComponent<NavMeshAgent> ();
		firstPosition = gameObject.transform.position;
		// Animal object
		animal = Animal.getAnimalByObjectName(gameObject.name);
	}

	// Update is called once per frame
	void Update () {
		if (moveNavmesh) {
			navmeshMove ();
		} else if (moveRandom) {
			StartCoroutine(randomMove());   
		}

		if (running) {
			agent.speed = 7;
			animation.Play("Run", PlayMode.StopAll);
		} else if (walking) {
			agent.speed = 4;
			animation.Play("Walk", PlayMode.StopAll);
			if (moveRandom) {
				transform.Translate(4*Time.deltaTime,0,0);
			}
		} else {
			Debug.Log("Idle");
			animation.Play("Idle", PlayMode.StopAll);
		}
	}

	public void setMoving (Vector3 moveTo) {
		moveNavmesh = true;
		moveToPoint = moveTo;
	}

	public void goToFirstPostion () {
		setMoving (firstPosition);
	}

	public void goToFence () {
		setMoving (GameObject.Find(animal.Breed + "Fence").transform.position);
		// Start random move
//		moveRandom = true;
	}

	IEnumerator randomMove () {
		walking = true;
		Debug.Log("Move random");
		yield return new WaitForSeconds (8.0f);
		Debug.Log("Stop random");
		walking = false;
	}

	public void navmeshMove () {
		// Distance beetwen move to point and animal
		float distance = Vector3.Distance(gameObject.transform.position, moveToPoint);
		if (distance > 0.4) {
			// If distance is larger than 10.0 than run else walk
			if (distance > 10.0) {
				walking = false;
				running = true;
			} else {
				running = false;
				walking = true;
			}
			agent.SetDestination(moveToPoint);
		} else {
			// Animal reached spot, so stop movement
			moveNavmesh = false;
			running = false;
			walking = false;
		}
	}
}