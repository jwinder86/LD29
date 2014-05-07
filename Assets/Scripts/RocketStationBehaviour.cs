using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class RocketStationBehaviour : StationBehaviour {

	public CameraBehaviour subCamera;
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
		if (engaged) {
			Vector3 fireDirection = relativeMouse(Input.mousePosition).normalized;
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

	public override void UseStation(bool engage, PigBehaviour player) {
		base.UseStation(engage, player);
		if (engage) {
			subCamera.zoomCamera(true);
		} else {
			subCamera.zoomCamera(false);
		}
	}

	private Vector3 relativeMouse(Vector3 mousePos) {
		float x = mousePos.x - (Screen.width / 2f);
		float y = mousePos.y - (Screen.height / 2f);
		return new Vector3(x, y, 0f);
	}
}
