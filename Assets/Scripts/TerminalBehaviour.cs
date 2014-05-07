using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Collider))]
public class TerminalBehaviour : MonoBehaviour {

	public StationBehaviour station;
	public RocketStationBehaviour rocketStation;

	// Use this for initialization
	void Start () {
		if (station == null) {
			station = (StationBehaviour) rocketStation;
		}
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerStay(Collider other) {
		PigBehaviour pig = other.GetComponent<PigBehaviour>();

		if (pig != null && Input.GetButtonDown("Jump")) {
			station.UseStation(true, pig);
		}
	}
}
