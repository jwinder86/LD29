using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class RocketStationBehaviour : MonoBehaviour, Station {

	public CameraBehaviour subCamera;

	public RocketBehaviour rocketPrefab;
	public float fireDistance;
	public float fireDelay;
	public float fireSpeed;
	public AudioClip activateStationSound;

	public Texture2D crosshairTex;
	public Color crosshairColor;
	public int texSize = 64;

	private bool engaged;
	private float fireTimer;

	// Use this for initialization
	void Start () {
		engaged = false;
		fireTimer = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (engaged) {
			if (fireTimer <= 0f && Input.GetButtonDown("Fire1")) {
				Vector3 fireDirection = relativeMouse(Input.mousePosition).normalized;
				FireRocket(fireDirection);
			}
		}

		if (fireTimer > 0f) {
			fireTimer -= Time.deltaTime;
		}
	}

	// draw the crosshairs
	void OnGUI() {
		if (engaged) {
			Rect rect = new Rect(
				Input.mousePosition.x - texSize / 2,
				Screen.height - Input.mousePosition.y - texSize / 2,
				texSize, texSize);
			
			GUI.color = crosshairColor;
			GUI.DrawTexture(rect, crosshairTex);
		}
	}

	private void FireRocket(Vector3 direction) {
		float angle = Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x);
		RocketBehaviour rocket = (RocketBehaviour) Instantiate(rocketPrefab, transform.position + direction * fireDistance, Quaternion.Euler(new Vector3(0f, 0f, angle)));
		rocket.rigidbody.velocity = rigidbody.velocity + direction * fireSpeed;

		fireTimer = fireDelay;
	}

	public void useStation(bool engage, PigBehaviour other) {

		if (engage) {
			Common.playSound(this.audio, activateStationSound);
			//rigidbody.isKinematic = false;
			engaged = true;
			other.useStation(this);
			subCamera.zoomCamera(true);
		} else {
			//rigidbody.isKinematic = true;
			engaged = false;
			other.useStation(null);
			subCamera.zoomCamera(false);
			audio.PlayOneShot(activateStationSound);
		}
	}

	private Vector3 relativeMouse(Vector3 mousePos) {
		float x = mousePos.x - (Screen.width / 2f);
		float y = mousePos.y - (Screen.height / 2f);
		return new Vector3(x, y, 0f);
	}
}
