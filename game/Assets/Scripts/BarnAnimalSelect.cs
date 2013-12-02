using UnityEngine;
using System.Collections;

public class BarnAnimalSelect : MonoBehaviour {
	
	private static string[,] animals =  {{"Pujs", "Pujsa", "Pujsek"}, {"Kurec", "Kura", "Pisancek"}, {"Bik", "Krava", "Tele"}};

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	void OnGUI () {
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
