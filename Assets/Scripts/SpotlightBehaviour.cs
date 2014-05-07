using UnityEngine;
using System.Collections;

public class SpotlightBehaviour : MonoBehaviour {

	public float rotateSpeed;

	private float goalAngle;

	// Use this for initialization
	void Start () {
		goalAngle = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		Quaternion startingRotation = transform.rotation;
		Quaternion endingRotation = Quaternion.Euler(new Vector3(0f, 0f, goalAngle));
		float rotateTime = Quaternion.Angle(startingRotation, endingRotation) / rotateSpeed;

		if (rotateTime > Time.deltaTime) {
			transform.rotation = Quaternion.Slerp(startingRotation, endingRotation, Time.deltaTime / rotateTime);
		} else {
			transform.rotation = endingRotation;
		}
	}

	public void RotateTo(float angle) {
		goalAngle = angle;
	}
}
