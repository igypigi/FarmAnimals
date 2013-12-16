//using UnityEngine;
//using System.Collections;
//
//public class AnimalFood : MonoBehaviour {
//	// Current animal
//	private AnimalMove currentAnimal;
//	private int currentAnimalIndex = 0;
//	private bool feedNext = true;
//
//	private GameObject player;
//
//	// Use this for initialization
//	void Start () {
//		player = GameObject.Find ("Player");
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		if (feedNext) {
//			currentAnimal = GameObject.Find(GlobalFunctions.animals[currentAnimalIndex].ObjectName).GetComponent<AnimalMove>();
//			feedNext = false;
//		} else {
//			// Distance beetwen food basket and player
//			float distance = Vector3.Distance(currentAnimal.foodBasket.transform.position, player.transform.position);
//			Debug.Log(currentAnimal.animal.Name);
//		}
//	}
//}
