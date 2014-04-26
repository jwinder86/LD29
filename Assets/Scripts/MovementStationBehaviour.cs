using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Transform))]
public class MovementStationBehaviour : MonoBehaviour, Station {

	public float movementAccel = 3f;
	public float maxSpeed = 5f;

	private bool engaged;

	// Use this for initialization
	void Start () {
		engaged = false;
	}
	
	// Update is called once per frame
	void Update () {
		directionalInput(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
	}

	public void directionalInput(Vector2 moveVector) {
		if (engaged) {
			if (rigidbody.velocity.magnitude < maxSpeed) {
				rigidbody.AddForce(moveVector * movementAccel, ForceMode.Acceleration);
			}
		}
	}

	public void useStation(bool engage, PigBehaviour other) {
		if (engage) {
			rigidbody.isKinematic = false;
			engaged = true;
			other.useStation(this);
		} else {
			rigidbody.isKinematic = true;
			engaged = false;
			other.useStation(null);
		}
	}

	public void mouseInputDown(Vector3 mousePos, bool leftButtonDown, bool rightButtonDown) {}
	
	public void mouseInputHeld(Vector3 mousePos, bool leftButtonHeld, bool rightButtonHeld) {}
}
