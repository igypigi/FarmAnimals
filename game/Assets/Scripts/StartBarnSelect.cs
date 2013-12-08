using UnityEngine;
using System.Collections;

public class StartBarnSelect : MonoBehaviour
{
	private static bool userOnSpot = false;
	private static GameObject player;
	private static GameObject barn;
	
	// Use this for initialization
	void Start ()
	{
		player = GameObject.Find ("Player");
		barn = GameObject.Find ("Barn");
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKey (KeyCode.Return)) {
			userOnSpot = !userOnSpot;
		}

		// If user presses Return and is standing on a pod than start animal select
		float distance = Vector3.Distance (gameObject.transform.position, player.transform.position);
		if (distance < 1.9 && userOnSpot) {
			Debug.Log ("User on spot");
			GlobalFunctions.switchCamera ("Barn Camera");
			// Disable user movement script
			player.GetComponent<ThirdPersonController> ().enabled = false;
			// Enable barn select script
			barn.GetComponent<BarnAnimalSelect> ().enabled = true;
		} else {
			Debug.Log ("User off spot");
			GlobalFunctions.switchCamera ("Main Camera");
			// Enable user movement script
			player.GetComponent<ThirdPersonController> ().enabled = true;
			// Disable barn select script
			barn.GetComponent<BarnAnimalSelect> ().enabled = false;
		}
	}
}