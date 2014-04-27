﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Transform))]
[RequireComponent (typeof(AudioSource))]
public class MovementStationBehaviour : MonoBehaviour, Station {

	public CameraBehaviour subCamera;

	public float movementAccel = 3f;
	public float maxSpeed = 5f;
	public AudioClip movementSound;
	public AudioClip activateStationSound;

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
			if(!audio.isPlaying){
				audio.PlayOneShot(movementSound);
			}			
			if (rigidbody.velocity.magnitude < maxSpeed) {
				rigidbody.AddForce(moveVector * movementAccel, ForceMode.Acceleration);
			}
		}	

	}

	public void useStation(bool engage, PigBehaviour other) {
		audio.PlayOneShot(activateStationSound);
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
		rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX;
		rigidbody.AddTorque(new Vector3(0f, 0f, Random.Range(-1, 1)) * 100f, ForceMode.Acceleration);
		subCamera.transform.parent = null;
	}
}
