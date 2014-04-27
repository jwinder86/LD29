using UnityEngine;
using System.Collections;

public class ChestBehaviour : MonoBehaviour {

	public LootBehaviour[] lootPrefabs;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Explode() {
		foreach (LootBehaviour loot in lootPrefabs) {
			Instantiate(loot, transform.position + new Vector3(Random.Range(-2f, 2f), Random.Range(0, 2f), 0f), Quaternion.Euler(new Vector3(0f, 0f, Random.Range(0f, 2306))));
		}

		Destroy(gameObject);
	}
}
