﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Collider))]
public class StationBehaviour : MonoBehaviour {

	public MovementStationBehaviour movementStation;
	public GrappleStationBehaviour grappleStation;

	private Station station;

	// Use this for initialization
	void Start () {
		station = null;
	}
	
	// Update is called once per frame
	void Update () {
		if (station == null) {
			findStation();
		}
	}

	void OnTriggerStay(Collider other) {
		PigBehaviour pig = other.GetComponent<PigBehaviour>();

		if (pig != null && Input.GetButtonDown("Jump")) {
			station.useStation(true, pig);
		}
	}

	private void findStation() {
		if (movementStation != null) {
			station = movementStation;
			if (grappleStation != null) {
				Debug.LogError("Ignoring GrappleStation: " + grappleStation);
			}
		} else if (grappleStation != null) {
			station = grappleStation;
		} else {
			Debug.LogError(this + ": No station to use!" + movementStation + grappleStation);
		}
	}
}
