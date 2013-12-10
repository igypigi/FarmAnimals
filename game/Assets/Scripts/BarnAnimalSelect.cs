using UnityEngine;
using System.Collections;

public class BarnAnimalSelect : MonoBehaviour {
	// Where animals should stop
	public GameObject animalStopSpot;

	private GameObject player;

	// Current animal
	private Animal currentAnimal;
	private int currentAnimalIndex = 0;
	private bool animalInPlace = false;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		currentAnimal = GlobalFunctions.animals[0];
	}

	// Update is called once per frame
	void Update () {
		// Is animal in place
		if (!animalInPlace) {
			Debug.Log("Moving animal to entrance: " + currentAnimal.Name + ", " + currentAnimal.ObjectName);
			moveAnimal (animalStopSpot.transform.position);

			animalInPlace = true;
		}
	}
	
	void OnGUI () {
		GUI.Box(new Rect(10, 10, 3 * 90 + 20, GlobalFunctions.animals.Count / 3 * 50 + 50), "Izberi živali");
		int index = 0;
		foreach (Animal animal in GlobalFunctions.animals) {
			if (GUI.Button(new Rect(index % 3 * 90 + 20, (index / 3 + 1) * 50, 80, 40), animal.Name)) {
				// If animal is in place and is selected correctly release animal
				if (animalInPlace) {
					bool correct = false;
					if (animal.Name == currentAnimal.Name) {
						correct = true;
						
						Debug.Log("Animal selected correctly: " + animal.Name);
						// Send animal away
						moveAnimal (GameObject.Find (currentAnimal.Breed + "Fence").transform.position);
						
						animal.inBarn = false;
					}
					// Get next animal
					setNextAnimal(correct);
					animalInPlace = false;
				}
			}
			index ++;
		}	
	}

	private void setNextAnimal (bool correct) {
		int numberOfAnimals = GlobalFunctions.animals.Count - 1;
		int animalsLeft = numberOfAnimals;

		// Send animal back if wrong button pressed
		if (!correct) {
			moveAnimal (GameObject.Find (currentAnimal.ObjectName).GetComponent<AnimalMove> ().getFirstPostion ());
		}

		// Get next animal that is not yet released
		while (!currentAnimal.inBarn || !correct) {
			// Take next animal
			if (currentAnimalIndex < numberOfAnimals) {
				currentAnimalIndex ++;
			} else {
				currentAnimalIndex = 0;
			}
			currentAnimal = GlobalFunctions.animals[currentAnimalIndex];
			correct = true;

			// Are all animals released
			animalsLeft --;
			if (animalsLeft == 0) {
				Debug.Log("----------- All animals released.");
				// Remove start animal select script
				Destroy(GameObject.Find ("Barn").GetComponent<StartBarnSelect> ());
				// Enable user movement and change camera
				GlobalFunctions.switchCamera ("Main Camera");
				player.GetComponent<ThirdPersonController> ().enabled = true;
				// Remove this script
				Destroy(this);
				break;
			}
		}

	}

	private void moveAnimal (Vector3 position) {
		// Get animal object from scene
		GameObject animalObject = GameObject.Find (currentAnimal.ObjectName);
		// Get animal script and activate moving
		animalObject.GetComponent<AnimalMove> ().setMoving (position);
	}
}
