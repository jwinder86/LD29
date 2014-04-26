﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof(ParticleSystem))]
[RequireComponent (typeof(AudioSource))]
[RequireComponent (typeof(Collider))]
public class ExplosionBehaviour : MonoBehaviour {
	
	private static CameraBehaviour screen = null;
	
	public AudioClip explosionSound;
	
	public float explosionForce;
	
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	public void Explode() {
		StartCoroutine(ExplodeAction());
		
		if (screen == null) {
			screen = (CameraBehaviour) FindObjectOfType(typeof(CameraBehaviour));
		}
		
		//screen.ShakeTime(0.5f);
	}
	
	private IEnumerator ExplodeAction() {
		particleSystem.Play();
		audio.PlayOneShot(explosionSound);
		
		yield return new WaitForSeconds(0.5f);
		
		collider.enabled = false;
		
		yield return new WaitForSeconds(5f);
		
		Destroy(gameObject);
	}
	
	public void OnTriggerEnter(Collider other) {
		/*PigBehaviour pig = other.GetComponent<PigBehaviour>();
		TargetTrigger target = other.GetComponent<TargetTrigger>();
		
		if (pig != null) {
			pig.Stun();
		}
		
		if (target != null){
			target.Explode();
		}*/
		
		if (other.rigidbody != null) {
			Vector3 direction = (other.transform.position - transform.position);
			direction.z = 0f;
			direction = direction.normalized;
			
			other.rigidbody.AddForce(direction * explosionForce, ForceMode.VelocityChange);
		}
	}
}