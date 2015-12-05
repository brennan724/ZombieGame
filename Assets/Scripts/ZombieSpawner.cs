using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ZombieSpawner : MonoBehaviour {

	public GameObject zombie;
	private Transform spawnerLoc;
	private float timeBetweenSpawns;
	private int zombiesToSpawn;
	private int level = 1;
	private int zombiesToKill;
	
	// Use this for initialization
	void Start () {
		timeBetweenSpawns = Random.Range(0, 8);
		zombiesToSpawn = level;
	}
	
	// Update is called once per frame
	void Update () {
		if (zombiesToSpawn != 0) {
			if (timeBetweenSpawns <= 0) {
				Spawn();
				timeBetweenSpawns = Random.Range(0, 5);
				zombiesToSpawn--;
			}
			else {
				timeBetweenSpawns -= Time.deltaTime;
			}
		}
		else if (zombiesToKill <= 0) {
			level++;
			zombiesToSpawn = level;
		}
	}

	public int GetLevel() {
		return level;
	}
	
	private Vector3 ProduceSpawnLocation() {
		float x;
		float y;
		float z;
		int side = Random.Range (0, 4);
		// spawn on the left side of the house
		if (side == 0) {
			x = -300f;
			y = 5f;
			z = Random.Range(-300, 301);
		}
		// spawn on the top of the house
		else if (side == 1) {
			x = Random.Range(-300, 301);
			y = 5f;
			z = 300f;
		}
		// spawn on the right side of the house
		else if (side == 2) {
			x = 300f;
			y = 5f;
			z = Random.Range(-300, 301);
		}
		// spawn on the bottom of the house
		else {
			x = Random.Range(-300, 301);
			y = 5f;
			z = -300f;
		}
		return new Vector3(x, y, z);
		
	}
	
	void Spawn() {
		zombiesToKill++;
		Vector3 pos = ProduceSpawnLocation();
		Instantiate(zombie, pos, Quaternion.identity);
	}

	public void SetZombiesToKill(int numberOfZombies) {
		zombiesToKill = numberOfZombies;
	}

	public int GetZombiesToKill() {
		return zombiesToKill;
	}
}
