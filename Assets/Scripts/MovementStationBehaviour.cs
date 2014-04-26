using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
public class MovementStationBehaviour : MonoBehaviour, Station {

	public float movementAccel = 3f;
	public float maxSpeed = 5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		directionalInput(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
	}

	public void directionalInput(Vector2 moveVector) {
		if (rigidbody.velocity.magnitude < maxSpeed) {
			rigidbody.AddForce(moveVector * movementAccel, ForceMode.Acceleration);
		}
	}

	public void mouseInputDown(Vector3 mousePos, bool leftButtonDown, bool rightButtonDown) {}
	
	public void mouseInputHeld(Vector3 mousePos, bool leftButtonHeld, bool rightButtonHeld) {}
}
