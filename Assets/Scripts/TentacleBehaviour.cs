using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Collider))]
[RequireComponent (typeof (AudioSource))]
public class TentacleBehaviour : MonoBehaviour {

	public float flapAngle;
	public float flapSpeed;
	public float hitForce;
	public float moveTime;
	public float hideTime;

	public bool startActive;

	public AudioClip hitSound;

	private float startAngle;
	private float start;

	private Vector3 hiddenPosition;
	private Vector3 outPosition;

	// Use this for initialization
	void Start () {
		start = Random.Range(0f, Mathf.PI * 2f);
		startAngle = transform.parent.eulerAngles.z;

		outPosition = transform.parent.position;
		hiddenPosition = transform.parent.position - transform.parent.right * 100;

		if (!startActive) {
			transform.parent.position = hiddenPosition;
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.parent.rotation = Quaternion.Euler(0f, transform.parent.eulerAngles.y, startAngle + getRot(start));
		start += Time.deltaTime * flapSpeed;
	}

	private float getRot(float timer) {
		return flapAngle * Mathf.Sin(timer * flapSpeed);
	}

	void OnTriggerEnter(Collider other) {
		BadSubBehaviour sub = other.GetComponent<BadSubBehaviour>();
		if (sub != null) {
			sub.Destroy();
		}
		
		SubExteriorBehaviour playerSub = other.GetComponent<SubExteriorBehaviour>();
		if (playerSub != null) {
			playerSub.takeDamage();
		}

		RocketBehaviour rocket = other.GetComponent<RocketBehaviour>();
		if (rocket != null) {
			rocket.Explode();
			hide();
		}
		
		if (other.rigidbody != null) {
			Vector3 direction = (other.transform.position - transform.position);
			direction.z = 0f;
			direction = direction.normalized;
			
			other.rigidbody.AddForce(direction * hitForce, ForceMode.VelocityChange);
			
			Debug.Log("Other pushed: " + other);
		}
	}

	public void hide() {
		StopAllCoroutines();
		StartCoroutine(HideCoroutine());
		audio.PlayOneShot(hitSound);
	}

	private IEnumerator HideCoroutine() {
		Vector3 startPos = transform.parent.position;
		for (float t = 0; t <= moveTime; t += Time.deltaTime) {
			transform.parent.position = Vector3.Lerp(startPos, hiddenPosition, t / moveTime);
			yield return null;
		}

		transform.parent.position = hiddenPosition;
		yield return new WaitForSeconds(hideTime);

		for (float t = 0; t <= moveTime; t += Time.deltaTime) {
			transform.parent.position = Vector3.Lerp(hiddenPosition, outPosition, t / moveTime);
			yield return null;
		}
	}
}
