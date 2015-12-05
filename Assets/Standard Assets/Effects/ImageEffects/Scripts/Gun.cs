using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {

	public GameObject fireParticles;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Fire1")) {
			//fire / particle effects
			print("GUN FIRED");
			Instantiate (fireParticles, GetComponent<Transform> ().position, Quaternion.identity);
		}
	}
	
}
