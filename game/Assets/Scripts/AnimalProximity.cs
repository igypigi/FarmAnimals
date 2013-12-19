﻿using UnityEngine;
using System.Collections;

public class AnimalProximity : MonoBehaviour {

	private const float maxDistanceToAnimalToShowDescription = 3.0f;


	private GameObject player;


	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
		//this.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI () {
		Vector3 playerPosition = player.transform.position;

		Animal closestAnimal = null;
		float closestAnimalDistance = float.MaxValue;

		foreach (Animal animal in GlobalFunctions.animals) {
			GameObject animalObject = GameObject.Find(animal.ObjectName);
			float dist = Vector3.Distance(playerPosition, animalObject.transform.position);
			if (dist < closestAnimalDistance && dist <= maxDistanceToAnimalToShowDescription) {
				closestAnimal = animal;
				closestAnimalDistance = dist;
			}
		}

		if (closestAnimal != null) {
			string animalDescription = closestAnimal.Name + "\n\nDescription:\n" + closestAnimal.Description;
			//animalDescription += "\n\nDistance: " + closestAnimalDistance;

			GUI.Box(new Rect(10, 10, 3 * 90 + 20, 2 * 50 + 50), animalDescription);
		}
	}
}
