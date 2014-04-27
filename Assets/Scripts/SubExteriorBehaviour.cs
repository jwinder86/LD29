using UnityEngine;
using System.Collections;

public class SubExteriorBehaviour : MonoBehaviour {

	public WaterClock waterClock;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void takeDamage() {
		waterClock.takeDamage();
	}
}
