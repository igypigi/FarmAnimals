using UnityEngine;
using System.Collections;

public class GlobalFunctions : MonoBehaviour {

	// TODO: Remove Start and Update functions
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

	public static void switchCamera (string cameraName) {
		// If camera not already enabled
		if (cameraName != Camera.current.name) {
			Debug.Log("Switching to camera " + cameraName);

			// Loop through all cameras in scene
			foreach (GameObject cam in GameObject.FindGameObjectsWithTag("Camera")) {
				Camera theCam = cam.GetComponent<Camera>() as Camera;

				// Enable cameraName and disable other
				if (cam.name == cameraName) {
					theCam.enabled = true;
				} else {
					theCam.enabled = false;
				}
			}  
		}
	}
}
