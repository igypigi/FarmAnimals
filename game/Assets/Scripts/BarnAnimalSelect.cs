using UnityEngine;
using System.Collections;

public class BarnAnimalSelect : MonoBehaviour {
	// Where animals should stop
	public GameObject animalStopSpot;

	private GameObject player;

	// Current animal
	private AnimalMove currentAnimal;
	private int currentAnimalIndex = 0;
	private bool moveNextAnimal = true;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
		// Get first animal from list
		currentAnimal = GameObject.Find(GlobalFunctions.animals[0].ObjectName).GetComponent<AnimalMove>();
	}

	// Update is called once per frame
	void Update () {
		// Is animal in place
		if (moveNextAnimal) {
			currentAnimal.setMoving(animalStopSpot.transform.position);
			moveNextAnimal = false;
		}
	}
	
	void OnGUI () {
		GUI.Box(new Rect(10, 10, 3 * 90 + 20, GlobalFunctions.animals.Count / 3 * 50 + 50), "Izberi živali");

		int index = 0;
		foreach (Animal animal in GlobalFunctions.animals) {
			if (GUI.Button(new Rect(index % 3 * 90 + 20, (index / 3 + 1) * 50, 80, 40), animal.Name)) {
				// If is selected correctly release animal
				bool correct = false;
				if (animal.Name == currentAnimal.animal.Name) {
					Debug.Log("Animal selected correctly: " + animal.Name);
					correct = true;
					// Send animal to its fence
					currentAnimal.goToFence();
					animal.inBarn = false;
				}
				// Get next animal
				setNextAnimal (correct);
				moveNextAnimal = true;
			}
			index ++;
		}	
	}

	private void setNextAnimal (bool correct) {
		Animal nextAnimal = currentAnimal.animal;
		int numberOfAnimals = GlobalFunctions.animals.Count;
		int animalsLeft = numberOfAnimals;

		// Get next animal that is not yet released
		bool animalInBarn = false;
		while (!animalInBarn) {
			// Take next animal
			currentAnimalIndex ++;
			if (currentAnimalIndex == numberOfAnimals) {
				currentAnimalIndex = 0;
			}
			nextAnimal = GlobalFunctions.animals[currentAnimalIndex];
			animalInBarn = nextAnimal.inBarn;

			// Are all animals released
			if (animalsLeft == 0) {
				Debug.Log("----------- All animals released.");
				// Remove start animal select script
				Destroy(GameObject.Find ("Barn").GetComponent<StartBarnSelect> ());
				// Enable user movement and change camera
				GlobalFunctions.switchCamera ("Main Camera");
				player.GetComponent<ThirdPersonController> ().enabled = true;
				// Remove this script
				Destroy(this);

				// Enable animal proximity script
				player.GetComponent<AnimalProximity>().enabled = true;

				// Hide sparkles
				GameObject.Find ("AnimalReleaseSpotSparkle").SetActive(false);

//				GameObject.Find("Food").GetComponent<AnimalFood> ().enabled = true;
				break;
			}
			animalsLeft --;
		}

		// Send old animal back if wrong button pressed and this is not the only animal in barn
		if (!correct && animalsLeft > 0) {
			currentAnimal.goToFirstPostion();
		}
		currentAnimal = GameObject.Find(nextAnimal.ObjectName).GetComponent<AnimalMove>();
	}
}