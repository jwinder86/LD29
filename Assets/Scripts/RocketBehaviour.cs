using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(Collider))]
[RequireComponent (typeof(AudioSource))]
public class RocketBehaviour : MonoBehaviour {
	
	public float rocketLifetime;
	public float acceleration;
	public float maxSpeed = 50;
	
	public ParticleSystem particleSystem;
	public Transform model;
	
	public ExplosionBehaviour explosionPrefab;
	
	public AudioClip thrustSound;
	
	private bool exploded;
	
	// Use this for initialization
	void Start () {
		exploded = false;
		
		audio.PlayOneShot(thrustSound);
		
		StartCoroutine(LifetimeAction());
	}
	
	// Update is called once per frame
	void Update () {
		// thrust forward
		Vector3 direction = transform.right;
		rigidbody.AddForce(direction * acceleration, ForceMode.Acceleration);
		
		// speed limiter
		float speed = rigidbody.velocity.magnitude;
		if (speed > maxSpeed && !rigidbody.isKinematic){
			rigidbody.velocity = rigidbody.velocity.normalized * maxSpeed;
		}
	}
	
	public void OnCollisionEnter(Collision collision) {
		Explode();
	}
	
	public void Explode() {
		if (!exploded) {
			exploded = true;
			
			ExplosionBehaviour explosion = (ExplosionBehaviour) Instantiate(explosionPrefab, transform.position, Quaternion.identity);
			explosion.Explode();
			
			// disable
			particleSystem.Stop();
			collider.enabled = false;
			rigidbody.isKinematic = true;
			Destroy(model.gameObject);
			audio.Stop();
		}
		
		// destroy later
		StartCoroutine(DestroyAction());
	}
	
	private IEnumerator LifetimeAction() {
		yield return new WaitForSeconds(rocketLifetime);
		Explode ();
	}
	
	private IEnumerator DestroyAction() {
		yield return new WaitForSeconds(4f);
		Destroy(gameObject);
	}
}
