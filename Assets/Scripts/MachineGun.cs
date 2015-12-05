using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MachineGun : MonoBehaviour {

	protected Animator animator;
	public GameObject particleChild;
	private ParticleSystem muzzleFlare;
	private Transform transform;
	private AudioSource gunfireSound;
	private float gunfireTimer;
	
	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		transform = GetComponent<Transform> ();
		muzzleFlare = particleChild.GetComponent<ParticleSystem> ();
		gunfireSound = GetComponent<AudioSource> ();
		gunfireTimer = float.MaxValue;
	}
	
	// Update is called once per frame
	void Update () {
		if (Time.timeScale != 0) {
			gunfireSound.mute = false;
			gunfireSoundLogic ();
			gunfireMotionAndParticlesLogic ();
		} else {
			gunfireSound.mute = true;
		}
	}
	
	private void gunfireSoundLogic() {
		if (Input.GetMouseButton (0) && gunfireTimer >= gunfireSound.clip.length) {
			gunfireTimer = 0f;
			gunfireSound.Play ();
		} else if (Input.GetMouseButton (0) && gunfireTimer < gunfireSound.clip.length) { 
			gunfireTimer += 0.01f;
		} else {
			gunfireSound.Stop ();
			gunfireTimer = float.MaxValue;
		}
	}

	private void gunfireMotionAndParticlesLogic() {
		if (Input.GetButton ("Fire1")) {
			animator.Play ("fire");
			muzzleFlare.Emit (6);
			FireBullet();
		} else {
			animator.Play("default");
		}
	}

	void FireBullet() {
		RaycastHit hitData;
		Vector3 forward = transform.TransformDirection(Vector3.forward);
		Physics.Raycast (GetComponent<Transform> ().position, forward, out hitData, Mathf.Infinity);
		if (hitData.collider != null && hitData.collider.tag.Equals("zombie")) {
			var zombie = hitData.collider.gameObject.GetComponent<Zombie>();
			zombie.bloodParticleSystem.transform.position = hitData.point;
			zombie.InflictDamage();
			zombie.bloodParticleSystem.Emit(6);
		}
	}
}
