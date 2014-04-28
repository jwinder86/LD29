﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
public class PumpStationBehavior : MonoBehaviour, Station {

	public CameraBehaviour subCamera;

	public WaterClock waterClock;
	public AudioClip activateStationSound;
	private bool engaged;


	// Use this for initialization
	void Start () {
		engaged = false;
	}
	
	// Update is called once per frame
	void Update () {
		/*if (engaged) {
			waterClock.setPumpingWater(true);
		}else{
			waterClock.setPumpingWater(false);
		}*/
	}

	public void useStation(bool engage, PigBehaviour other) {
		if (engage) {
			engaged = true;
			other.useStation(this);
			waterClock.setPumpingWater(true);
		} else {
			engaged = false;
			other.useStation(null);
			waterClock.setPumpingWater(false);
			audio.Stop ();
		}
		audio.PlayOneShot(activateStationSound);
	}
}
