using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	private bool showMenu = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			showMenu = !showMenu;

			if (showMenu) {
				Time.timeScale = 0.0f;
			} else {
				Time.timeScale = 1.0f;
			}
		}

		if (showMenu) {

		}
	}

	// show main menu
	void OnGUI () {
		if (showMenu) {
			int buttons = 2;
			int width = 300;
			int height = 25 + buttons * 50 + 25;
			int x = Screen.width / 2 - width / 2;
			int y = Screen.height / 2 - height / 2;
			int padding = width / 10;

			GUI.Box(new Rect(x, y, width, height), "");

			if (GUI.Button(new Rect(x + padding, y + padding + 0 * 2 * padding, width - 2 * padding, padding), "Quit game")) {
				Application.Quit();
			}

			if (GUI.Button(new Rect(x + padding, y + padding + 1 * 2 * padding, width - 2 * padding, padding), "Continue game")) {
				showMenu = false;
				Time.timeScale = 1.0f;
			}
		}
	}
}
