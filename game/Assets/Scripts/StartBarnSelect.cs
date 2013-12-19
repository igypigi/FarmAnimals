using UnityEngine;
using System.Collections;

public class StartBarnSelect : MonoBehaviour
{
	// Where user must stand
	public GameObject startScriptSpot;

	private bool userOnSpot = false;
	private GameObject player;
	private GameObject barn;
	
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
		float distance = Vector3.Distance (startScriptSpot.transform.position, player.transform.position);
		if (distance < 1.9 && userOnSpot) {
			startAnimalBarnSelect(true);

			// Disable this script
			this.enabled = false;
		} else {
			startAnimalBarnSelect(false);
		}
	}

	public void startAnimalBarnSelect (bool enableBarnAnimalSelect) {
		if (enableBarnAnimalSelect) {
			GlobalFunctions.switchCamera ("Barn Camera");
		} else {
			GlobalFunctions.switchCamera ("Main Camera");
		}
		// Disable/Enable animal proximity script
		player.GetComponent<AnimalProximity>().enabled = !enableBarnAnimalSelect;
		// Enable/disable user movement script
		player.GetComponent<ThirdPersonController> ().enabled = !enableBarnAnimalSelect;
		// Disable/enable barn select script
		barn.GetComponent<BarnAnimalSelect> ().enabled = enableBarnAnimalSelect;
	}
}