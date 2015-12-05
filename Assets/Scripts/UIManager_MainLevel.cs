using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Text;

public class UIManager_MainLevel : MonoBehaviour {

	public Text currentLevelText;
	public Text deadText;
	public Button replayButton;
	public Image bloodLens;
	private ZombieSpawner zombieSpawner;
	public Text pauseText;
	private bool paused;
	public Texture2D crosshairTexture;
	private float crosshairScale;

	// Use this for initialization
	void Start () {
		deadText.enabled = false;
		replayButton.gameObject.SetActive(false);
		zombieSpawner = GameObject.FindGameObjectWithTag ("spawner").GetComponent<ZombieSpawner>();
		pauseText.enabled = false;
		paused = false;
		crosshairScale = 0.75f;
	}
	
	// Update is called once per frame
	void Update () {
		StringBuilder currentLevelSB = new StringBuilder ("Level ");
		currentLevelSB.Append (zombieSpawner.GetLevel ());
		currentLevelText.text = currentLevelSB.ToString ();

		checkIfQuitGame();
		checkIfPause ();
		if (Time.timeScale != 0) {
			hideCursor ();
		} else {
			showCursor();
		}
	}

	void OnGUI() {
		if(Time.timeScale != 0) {
			if(crosshairTexture!=null) {
				GUI.DrawTexture(new Rect((Screen.width-crosshairTexture.width*crosshairScale)/2 ,(Screen.height-crosshairTexture.height*crosshairScale)/2, crosshairTexture.width*crosshairScale, crosshairTexture.height*crosshairScale),crosshairTexture);
			} else {
				Debug.Log("No crosshair texture set in the Inspector");
			}
		}
	}

	public void TriggerBloodLens() {
		bloodLens.CrossFadeAlpha (110, 0f, false);
		bloodLens.CrossFadeAlpha (0, 3f, false);
	}

	public void DisplayGameOverScreen() {
		bloodLens.CrossFadeAlpha (150, 0f, false);
		deadText.enabled = true;
		replayButton.gameObject.SetActive(true);
	}

	public void replayButtonHandler() {
		Time.timeScale = 1f;
		Application.LoadLevel (Application.loadedLevel);
	}

	private void checkIfPause() {
		if (!paused && Input.GetButtonDown ("Pause")) {
			pauseText.enabled = true;
			paused = true;
			Time.timeScale = 0;
			if (Input.GetKeyDown (KeyCode.Escape)) {
				Application.Quit();
			}
		} else if (paused && Input.GetButtonDown("Pause")) {
			pauseText.enabled = false;
			Time.timeScale = 1;
			paused = false;
		}
	}

	private void checkIfQuitGame() {
		if (Input.GetButtonDown ("Quit")) {
			Application.Quit();
		}
		if (Input.GetButtonDown ("GoToMenu")) {
			Application.LoadLevel (0);
		}
	}

	private void hideCursor() {
		Cursor.visible = false;
		Cursor.lockState = CursorLockMode.Locked;
	}
	
	private void showCursor() {
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
	}
}
