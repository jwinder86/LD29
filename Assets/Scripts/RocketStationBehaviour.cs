using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class RocketStationBehaviour : MovementStationBehaviour {

	public SpotlightBehaviour spotlight;

	public RocketBehaviour rocketPrefab;
	public float fireDistance;
	public float fireDelay;
	public float fireSpeed;

	private float fireTimer;

	// Use this for initialization
	void Start () {
		fireTimer = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		directionalInput(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));

		if (engaged) {
			Vector3 fireDirection = getRelativeMouse().normalized;
			if (fireTimer <= 0f && Input.GetButtonDown("Fire1")) {
				FireRocket(fireDirection);
			}

			spotlight.RotateTo(Mathf.Atan2(fireDirection.y, fireDirection.x) * Mathf.Rad2Deg);
		}

		if (fireTimer > 0f) {
			fireTimer -= Time.deltaTime;
		}
	}

	private void FireRocket(Vector3 direction) {
		float angle = Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x);
		RocketBehaviour rocket = (RocketBehaviour) Instantiate(rocketPrefab, transform.position + direction * fireDistance, Quaternion.Euler(new Vector3(0f, 0f, angle)));
		rocket.rigidbody.velocity = rigidbody.velocity + direction * fireSpeed;

		fireTimer = fireDelay;
	}
}
