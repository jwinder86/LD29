using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Collider))]
public class MineBehaviour : MonoBehaviour {

	public ExplosionBehaviour explosionPrefab;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider other) {
		ExplosionBehaviour explosion = (ExplosionBehaviour) Instantiate(explosionPrefab, transform.position, Quaternion.identity);
		explosion.Explode();
		Destroy(gameObject);
	}
}
