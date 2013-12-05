using UnityEngine;
using System.Collections;

public class BarnAnimalSelect : MonoBehaviour {
	private static string[,] animals =  {{"Pujs", "Pujsa", "Pujsek"}, {"Kurec", "Kura", "Pisancek"}, {"Bik", "Krava", "Tele"}};
	private static bool userOnSpot = false;
	private static GameObject player;

	// Use this for initialization
	void Start () {
		// TODO: Create a sequence list of animals
		player = GameObject.Find ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		float distance = Vector3.Distance(gameObject.transform.position, player.transform.position);
		if (distance < 1.9) {
//			GameObject.Find("BarnCamera").camera.active = true;
//			GameObject.Find("Camera").camera.active = false;
			userOnSpot = true; 
		} else {
//			GameObject.Find("Camera").camera.active = true;
//			GameObject.Find("BarnCamera").camera.active = false;
			userOnSpot = false;
		}
		// TODO: If animal was correctly choosen send animals out and select next one from list
	}
	
	void OnGUI () {
		if (userOnSpot) {
			GUI.Box(new Rect(10, 10, 3 * 90 + 20, animals.GetLength(0) * 50 + 50), "Izberi živali");
			int index = 0;
			
			foreach (string animal in animals) {
				if (GUI.Button(new Rect(index % 3 * 90 + 20, (index / 3 + 1) * 50, 80, 40), animal)) {
					Debug.Log(animal);
				}
				index++;
			}	
		}
	}
}
