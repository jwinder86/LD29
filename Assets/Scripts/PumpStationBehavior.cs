using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
public class PumpStationBehavior : StationBehaviour {

	public WaterClock waterClock;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	public override void UseStation(bool engage, PigBehaviour player) {
		base.UseStation(engage, player);
		if (engage) {
			waterClock.setPumpingWater(true);
		} else {
			waterClock.setPumpingWater(false);
		}
	}
}
