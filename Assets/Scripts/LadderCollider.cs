using UnityEngine;
using System.Collections;

public class LadderCollider : MonoBehaviour {

	public float groundDuration = 0.2f;
	
	private float onLadder;
	
	// Use this for initialization
	void Start () {
		onLadder = 0f; 
	}
	
	void Update() {
		onLadder -= Time.deltaTime;
	}
	
	void OnTriggerEnter(Collider other) {
		onLadder = groundDuration;
	}
	
	void OnTriggerStay(Collider other) {
		onLadder = groundDuration;
	}
	
	void OnTriggerExit(Collider other) {
		onLadder = 0f;
	}
	
	public bool OnLadder() {
		return onLadder > 0f;
	}
}
