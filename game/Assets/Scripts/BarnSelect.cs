using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BarnSelect : MonoBehaviour
{
	// Where animals should stop
	public GameObject animalStopSpot;
	private Vector3 animalStopSpotPosition;

	private const float maxDistanceToSpot = 1.9f;

	// Current animal
	private AnimalMove currentAnimal;
	private int currentAnimalIndex = 0;
	private bool moveNextAnimal = true;

	// Where user must stand
	public GameObject startScriptSpot;

	private bool userOnSpot = false;
	private GameObject player;

	private List<Animal> animals;

	private bool animalSelectEnabled = false;
	
	// Use this for initialization
	void Start ()
	{
		animals = new List<Animal>(GlobalFunctions.animals);
		player = GameObject.Find ("Player");
		animalStopSpotPosition = animalStopSpot.transform.position;
		// Get first animal from list
		currentAnimal = animals[0].gameObject.GetComponent<AnimalMove>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey (KeyCode.Return) && !animalSelectEnabled) {
			userOnSpot = !userOnSpot;
		}

		// If user presses Return and is standing on a pod than start animal select
		float distance = Vector3.Distance (startScriptSpot.transform.position, player.transform.position);
		animalSelectEnabled = distance < maxDistanceToSpot && userOnSpot;
		if (animalSelectEnabled) {
			// Is animal in place
			GlobalFunctions.switchCamera ("Barn Camera");
			if (moveNextAnimal) {
				currentAnimal.setMoving(animalStopSpotPosition);
				moveNextAnimal = false;
			}
		} else {
			GlobalFunctions.switchCamera ("Main Camera");
		}
		// Disable/Enable animal proximity script
		player.GetComponent<AnimalProximity>().enabled = !animalSelectEnabled;
		// Enable/disable user movement script
		player.GetComponent<ThirdPersonController>().enabled = !animalSelectEnabled;
	}
	
	void OnGUI () {
		if (animalSelectEnabled) {
			GUI.Box(new Rect(10, 10, 3 * 90 + 20, GlobalFunctions.animals.Count / 3 * 50 + 50), "Select animal");
			
			int index = 0;
			foreach (Animal animal in GlobalFunctions.animals) {
				if (GUI.Button(new Rect(index % 3 * 90 + 20, (index / 3 + 1) * 50, 80, 40), animal.name)) {
					// If is selected correctly release animal
					bool correct = false;
					if (animal.name == currentAnimal.animal.name) {
						Debug.Log("Animal selected correctly: " + animal.name);
						correct = true;
						// Send animal to its fence
						currentAnimal.goToFence();
						animals.Remove(animal);
						currentAnimalIndex --;
					}
					// Get next animal
					setNextAnimal (correct);
					moveNextAnimal = true;
				}
				index ++;
			}
		}
	}
	
	private void setNextAnimal (bool correct) {
		Animal nextAnimal = currentAnimal.animal;

		if (animals.Count == 0) {
			Debug.Log("----------- All animals released.");
			// Enable user movement and change camera
			GlobalFunctions.switchCamera ("Main Camera");

			player.GetComponent<ThirdPersonController>().enabled = true;
			// Disable this script
			this.enabled = false;
			// Enable animal proximity script
			player.GetComponent<AnimalProximity>().enabled = true;

			// enable animal food script
			player.GetComponent<AnimalFood>().enabled = true;
			
			// Hide sparkles
			GameObject.Find ("AnimalReleaseSpotSparkle").SetActive(false);
		} else {
			currentAnimalIndex ++;
			if (currentAnimalIndex >= animals.Count) {
				currentAnimalIndex = 0;
			}
			
			// Send old animal back if wrong button pressed and this is not the only animal in barn
			nextAnimal = animals[currentAnimalIndex];
			if (!correct && animals.Count > 1) {
				Debug.Log("Animal wrong");
				currentAnimal.goToFirstPostion();
			} 
			currentAnimal = nextAnimal.gameObject.GetComponent<AnimalMove>();
		}
	}
}