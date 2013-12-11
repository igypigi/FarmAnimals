﻿using UnityEngine;
using System.Collections;

public class AnimalMove : MonoBehaviour {
	private NavMeshAgent agent;
	private Vector3 firstPosition;
	public Animal animal;

	// If animals should move this variable should be set
	private Vector3 moveToPoint;
	private bool moving;

	// Use this for initialization
	void Start () {
		agent = gameObject.GetComponent<NavMeshAgent> ();
		firstPosition = gameObject.transform.position;
		// Animal object
		animal = Animal.getAnimalByObjectName(gameObject.name);
	}

	// Update is called once per frame
	void Update () {
		if (moving) {
			// Distance beetwen move to point and animal
			float distance = Vector3.Distance(gameObject.transform.position, moveToPoint);
			if (distance > 0.2) {
				// If distance is larger than 10.0 than run else walk
				if (distance > 10.0) {
					agent.speed = 7;
					animation.Play("Run", PlayMode.StopAll);
				} else {
					agent.speed = 4;
					animation.Play("Walk", PlayMode.StopAll);
				}
				agent.SetDestination(moveToPoint);
			} else {
				// Animal reached spot, so stop movement
				moving = false;
			}
		} else {
			animation.Play("Idle", PlayMode.StopAll);
		}
	}

	public void setMoving (Vector3 moveTo) {
		moving = true;
		moveToPoint = moveTo;
	}

	public void goToFirstPostion () {
		setMoving (firstPosition);
	}

	public void goToFence () {
		setMoving (GameObject.Find(animal.Breed + "Fence").transform.position);
	}
}