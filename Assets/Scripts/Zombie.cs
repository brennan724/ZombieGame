using UnityEngine;
using System.Collections;

public class Zombie : MonoBehaviour {

	public Transform[] windowLocations;
	private bool outside;
	protected Animator myAnimator;
	private Transform transform;
	private NavMeshAgent navMeshAgent;
	private GameObject player;
	private Transform closestWindowLocation;
	private int health;
	public ParticleSystem bloodParticleSystem;
	private AudioSource attackSound;
	private ZombieSpawner zombieSpawner;

	// Use this for initialization
	void Start () {
		outside = true;
		health = 100;
		myAnimator = GetComponent<Animator> ();
		navMeshAgent = GetComponent<NavMeshAgent> ();
		transform = GetComponent<Transform> ();
		player = GameObject.FindGameObjectWithTag ("Player");
		closestWindowLocation = getClosest (windowLocations);
		attackSound = GetComponent<AudioSource> ();
		zombieSpawner = GameObject.FindGameObjectWithTag("spawner").GetComponent<ZombieSpawner>();
	}
	
	// Update is called once per frame
	void Update () {
		if (outside && health > 0) {
			navMeshAgent.SetDestination (closestWindowLocation.position);
		} else if (!outside && health > 0) {
			navMeshAgent.SetDestination (player.transform.position);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			myAnimator.Play("attack"); //this line ends the walking segment immediately. Otherwise there is delay
			myAnimator.SetBool ("isWithinRange", true);
		} else if (outside && other.tag.Equals ("window_bottom")) {
			myAnimator.SetBool ("reachedWindow", true);
			outside = false;
		}
	}

	void OnTriggerExit(Collider other) {
		if (other.tag == "Player") {
			myAnimator.SetBool ("isWithinRange", false);
		} else if (outside && other.tag.Equals ("window_bottom")) {
			myAnimator.SetBool ("reachedWindow", true);
			outside = false;
		}
	}

	void HurtPlayerIfInRange() {
		if(Vector3.Distance(player.transform.position, gameObject.transform.position) < 12.5) {
			player.GetComponent<PlayerHealth>().InflictDamage();
		}
	}
	
	Transform getClosest (Transform[] points) {
		Transform closestPoint = null;
		float closestDistance = Mathf.Infinity;
		Vector3 currentPosition = transform.position;
		foreach (Transform point in points) {
			float distance = Vector3.Distance(currentPosition, point.position);
			if (distance < closestDistance) {
				closestPoint = point;
				closestDistance = distance;
			}
		}
		return closestPoint;
	}

	public void InflictDamage() {
		health -= 5;
		if (health <= 0) {
			Die();
		}
	}

	private void Die() {
		zombieSpawner.SetZombiesToKill(zombieSpawner.GetZombiesToKill()-1);
		myAnimator.Play ("die");
		myAnimator.SetBool ("dead", true);
		GetComponent<SphereCollider> ().enabled = false;
		GetComponent<BoxCollider> ().enabled = false;
	}

	void DestroyCorpse() {
		Destroy (this.gameObject);
	}

	void playAttackSound() {
		attackSound.Play ();
	}

}
