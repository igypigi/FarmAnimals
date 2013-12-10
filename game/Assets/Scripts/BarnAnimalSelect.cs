using UnityEngine;
using System.Collections;

public class BarnAnimalSelect : MonoBehaviour {
	private static GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player");
	}

	// Current animal
	private static Animal currentAnimal;
	private static int currentAnimalIndex = 0;
	private static bool animalInPlace = false;
	
	// Update is called once per frame
	void Update () {
		// Is animal in place
		if (!animalInPlace) {
			// Get current animal
			currentAnimal = GlobalFunctions.animals[currentAnimalIndex];
			Debug.Log("Moving animal to entrance: " + currentAnimal.Name + ", " + currentAnimal.ObjectName);

			// Get animal object from scene
			GameObject animalObject = GameObject.Find (currentAnimal.ObjectName);
			// Get animal script and activate moving
			animalObject.GetComponent<AnimalMove> ().setMoving (
				true, 
				GameObject.Find ("AnimalBarnSpot").transform.position
			);

			animalInPlace = true;
		}
	}
	
	void OnGUI () {
		GUI.Box(new Rect(10, 10, 3 * 90 + 20, GlobalFunctions.animals.Count / 3 * 50 + 50), "Izberi živali");
		int index = 0;
		
		foreach (Animal animal in GlobalFunctions.animals) {
			if (GUI.Button(new Rect(index % 3 * 90 + 20, (index / 3 + 1) * 50, 80, 40), animal.Name)) {
				// If animal is in place and is selected correctly release animal
				if (animalInPlace && animal.Name == currentAnimal.Name) {
					Debug.Log("Animal selected correctly: " + animal.Name);

					// Send animal away
					GameObject animalObject = GameObject.Find (currentAnimal.ObjectName);
					// Get animal script and activate moving
					animalObject.GetComponent<AnimalMove> ().setMoving (
						true, 
						GameObject.Find ("TempPostion").transform.position
					);

					// Take next animal
					if (currentAnimalIndex < GlobalFunctions.animals.Count - 1) {
						currentAnimalIndex ++;
					} else {
						currentAnimalIndex = 0;
					}
					animal.inBarn = false;
					animalInPlace = false;
				}
			}
			index++;
		}	
	}
}
