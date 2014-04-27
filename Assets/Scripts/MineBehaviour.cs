using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Collider))]
public class MineBehaviour : MonoBehaviour {

	public ExplosionBehaviour explosionPrefab;
	public LootBehaviour lootPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
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
	}
}
