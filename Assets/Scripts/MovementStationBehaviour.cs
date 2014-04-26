using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Transform))]
public class MovementStationBehaviour : MonoBehaviour, Station {

	public CameraBehaviour subCamera;

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
			subCamera.zoomCamera(true);
		} else {
			rigidbody.isKinematic = true;
			engaged = false;
			other.useStation(null);
			subCamera.zoomCamera(false);
		}
	}

	public void sinkSub(){
		engaged = false;
		subCamera.zoomCamera(true);
		rigidbody.isKinematic = false;
		rigidbody.useGravity = true;
//		transform.rigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
//		rigidbody.AddTorque(Random.rotation.eulerAngles, ForceMode.VelocityChange);
	}
}
