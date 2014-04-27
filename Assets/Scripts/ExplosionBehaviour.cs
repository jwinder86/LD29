using UnityEngine;
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
		StartCoroutine(ExplodeAction(null));
		
		if (screen == null) {
			screen = (CameraBehaviour) FindObjectOfType(typeof(CameraBehaviour));
		}
		
		screen.ShakeTime(0.5f);
	}

	public void ExplodeAndInstantiate(GameObject obj) {
		StartCoroutine(ExplodeAction(obj));
		
		if (screen == null) {
			screen = (CameraBehaviour) FindObjectOfType(typeof(CameraBehaviour));
		}
		
		screen.ShakeTime(0.5f);
	}
	
	private IEnumerator ExplodeAction(GameObject obj) {
		particleSystem.Play();
		audio.PlayOneShot(explosionSound);
		
		yield return new WaitForSeconds(0.5f);
		
		collider.enabled = false;

		if (obj != null) {
			Instantiate(obj, transform.position, Quaternion.identity);
		}
		
		yield return new WaitForSeconds(5f);
		
		Destroy(gameObject);
	}
	
	public void OnTriggerEnter(Collider other) {
		Debug.Log ("Explosion: " + other);

		BadSubBehaviour sub = other.GetComponent<BadSubBehaviour>();
		if (sub != null) {
			sub.Destroy();
		}

		SubExteriorBehaviour playerSub = other.GetComponent<SubExteriorBehaviour>();
		if (playerSub != null) {
			playerSub.takeDamage();
		}

		ChestBehaviour chest = other.GetComponent<ChestBehaviour>();
		if (chest != null) {
			chest.Explode();
		}
		
		if (other.rigidbody != null) {
			Vector3 direction = (other.transform.position - transform.position);
			direction.z = 0f;
			direction = direction.normalized;
			
			other.rigidbody.AddForce(direction * explosionForce, ForceMode.VelocityChange);

			Debug.Log("Other pushed: " + other);
		}
	}
}