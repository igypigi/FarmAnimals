using UnityEngine;
using System.Collections;

public class DescriptionAboveObject : MonoBehaviour {

	private const float maxDistanceToFood = 4.0f;

	private float x;
	private float y;
	private float z;

	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("Player");
	}
	
	// Update is called once per frame
	void Update () {
		Camera cam = GameObject.Find ("Main Camera").camera;
		Vector3 objectPosition = transform.position;
		objectPosition.y += 1.2f;

		Vector3 screenPos = cam.WorldToScreenPoint(objectPosition);

		x = screenPos.x;
		y = Screen.height - screenPos.y;
		z = screenPos.z;
	}

	// show user current task
	void OnGUI () {
		Vector3 playerPosition = player.transform.position;
		float dist = Vector3.Distance(playerPosition, transform.position);

		if (z >= 0.0f && dist < maxDistanceToFood) {
			int width = 100;
			GUI.Box(new Rect(x - width / 2, y, width, 25), this.name);
		}
	}
}
