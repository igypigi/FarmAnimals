﻿using UnityEngine;
using System.Collections;

public class AnimalProximity : MonoBehaviour {

	private const float maxDistanceToAnimalToShowDescription = 3.0f;


	private GameObject player;


	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
	}

	/* Show closest animals description if its distance to player is < maxDistanceToAnimalToShowDescription */
	void OnGUI () {
		Vector3 playerPosition = player.transform.position;

		Animal closestAnimal = null;
		float closestAnimalDistance = float.MaxValue;

		// Find animal closest to player
		foreach (Animal animal in GlobalFunctions.animals) {
			GameObject animalObject = GameObject.Find(animal.objectName);
			float dist = Vector3.Distance(playerPosition, animalObject.transform.position);
			if (dist < closestAnimalDistance && dist <= maxDistanceToAnimalToShowDescription) {
				closestAnimal = animal;
				closestAnimalDistance = dist;
			}
		}

		if (closestAnimal != null) {
			string animalDescription = closestAnimal.name + "\n\nDescription:\n" + closestAnimal.description;
			int width = 3 * 90 + 20;
			GUI.Box(new Rect(10, 10, width, width-40), "");
			GUI.DrawTexture(new Rect(40, 20, width-60, width-60), closestAnimal.image, ScaleMode.ScaleToFit, true, 0.0f);
			GUI.Box(new Rect(10, width -20, width, 100), animalDescription);
		}
	}
}
