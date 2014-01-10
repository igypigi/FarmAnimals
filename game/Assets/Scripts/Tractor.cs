using UnityEngine;
using System.Collections;

public class Tractor : MonoBehaviour {

	private const float maxDistanceToTractor = 5.0f;

	private bool drivingTractor = false;

	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Return)) {
			if (drivingTractor) {
				drivingTractor = false;
				player.SetActive(true);
				this.GetComponent<ThirdPersonController>().enabled = false;
				this.GetComponent<ThirdPersonCamera>().enabled = false;
				Vector3 pos = transform.position;
				pos.x += 1.5f;
				pos.y += 1.0f;
				player.transform.position = pos;
			} else {
				Vector3 playerPosition = player.transform.position;
				float dist = Vector3.Distance(playerPosition, transform.position);
				if (dist < maxDistanceToTractor) {
					drivingTractor = true;
					player.SetActive(false);
					this.GetComponent<ThirdPersonController>().enabled = true;
					this.GetComponent<ThirdPersonCamera>().enabled = true;
				}
			}
		}
	}
}
