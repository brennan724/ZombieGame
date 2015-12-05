using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UIManager_MainMenu : MonoBehaviour {

	public Text text;

	// Use this for initialization
	void Start () {
		Time.timeScale = 0;
		Cursor.visible = true;
		Cursor.lockState = CursorLockMode.None;
	}

	public void LoadMainLevel() {
		Time.timeScale = 1;
		Application.LoadLevel (1);
	}

	void Update() {
		if (Input.GetButtonDown ("Quit")) {
			Application.Quit();
		}
	}
}
