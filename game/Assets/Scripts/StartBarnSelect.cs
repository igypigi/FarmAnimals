using UnityEngine;
using System.Collections;

public class StartBarnSelect : MonoBehaviour
{
	private static bool userOnSpot = false;
	private static GameObject player;
	
	// Use this for initialization
	void Start ()
	{
		player = GameObject.Find ("Player");
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
			GlobalFunctions.switchCamera("Barn Camera");
		} else {
			GlobalFunctions.switchCamera("Main Camera");
		}
	}
}