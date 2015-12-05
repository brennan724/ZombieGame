using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth : MonoBehaviour {
	
	private int health;
	private Transform transform;
	private UIManager_MainLevel uiManagerMainLevel;

	// Use this for initialization
	void Start () {
		transform = GetComponent<Transform> ();
		uiManagerMainLevel = GameObject.FindGameObjectWithTag ("ui_manager").GetComponent<UIManager_MainLevel>();
		health = 100;
	}

	void Update() {
		//if player falls off the map
		if (transform.position.y < -10) {
			Die ();
		}
	}
	
	public void InflictDamage() {
		health -= 20;
		uiManagerMainLevel.TriggerBloodLens ();
		if (health <= 0) {
			Die ();
		}
	}

	private void Die() {
		Time.timeScale = 0f;
		uiManagerMainLevel.DisplayGameOverScreen ();
	}
}
