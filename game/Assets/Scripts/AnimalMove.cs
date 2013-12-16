using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimalMove : MonoBehaviour {
	public Animal animal;

	private NavMeshAgent agent;
	private Vector3 firstPosition;
	private GameObject fence;
//	public Vector3 foodBasket;
	private List<Vector3> possibleMoves;

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
		// Animal fence object
		fence = GameObject.Find(animal.Breed + "Fence");
		// Food Basket
//		foodBasket = fence.transform.Find("Food basket").transform.position;

		// Possible moves inside fence
		possibleMoves = new List<Vector3>();
		foreach (Transform child in fence.transform.Find("Possible moves").transform) { 
			possibleMoves.Add(child.position);
		}
	}

	// Update is called once per frame
	void Update () {
		if (moveNavmesh) {
			navmeshMove ();
		} else if (moveRandom && !moveNavmesh) {
			// Start new random movement
			StartCoroutine(randomMove());
		}

		if (running) {
			agent.speed = 7;
			animation.Play("Run", PlayMode.StopAll);
		} else if (walking) {
			agent.speed = 4;
			animation.Play("Walk", PlayMode.StopAll);
		} else {
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
		setMoving (possibleMoves[Random.Range(0, possibleMoves.Count - 1)]);
		// Start random move
		moveRandom = true;
	}

	IEnumerator randomMove () {
		// Start moving else stay idle
		if (Random.Range(0.0f, 1.0f) > 0.7f) {
			setMoving (possibleMoves[Random.Range(0, possibleMoves.Count - 1)]);
		}
		moveRandom = false;
		yield return new WaitForSeconds (Random.Range(6, 20));
		moveRandom = true;
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