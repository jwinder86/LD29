using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Collider))]
public class MineBehaviour : MonoBehaviour {

	public ExplosionBehaviour explosionPrefab;
	public LootBehaviour lootPrefab;

	private bool exploded;

	// Use this for initialization
	void Start () {
		exploded = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		if (!exploded) {
			RocketBehaviour rocket = other.GetComponent<RocketBehaviour>();
			if (rocket != null) {
				rocket.Explode();
			}

			LootBehaviour loot = other.GetComponent<LootBehaviour>();
			if (loot != null) {
				return;
			}

			ExplosionBehaviour explosion = (ExplosionBehaviour) Instantiate(explosionPrefab, transform.position, Quaternion.identity);
			explosion.ExplodeAndInstantiate(lootPrefab.gameObject);
			Destroy(gameObject);

			exploded = true;
		}
	}
}
