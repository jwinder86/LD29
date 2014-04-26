﻿using UnityEngine;
using System.Collections;

public class PumpStationBehavior : MonoBehaviour, Station {

	public CameraBehaviour subCamera;

	public WaterClock waterClock;

	private bool engaged;


	// Use this for initialization
	void Start () {
		engaged = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (engaged) {
			waterClock.setPumpingWater(true);
		}else{
			waterClock.setPumpingWater(false);
		}
	}

	public void useStation(bool engage, PigBehaviour other) {
		if (engage) {
			engaged = true;
			other.useStation(this);
			//subCamera.zoomCamera(true);
		} else {
			engaged = false;
			other.useStation(null);
			//subCamera.zoomCamera(false);
		}
	}
}
