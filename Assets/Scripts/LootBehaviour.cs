using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (Collider))]
public class LootBehaviour : MonoBehaviour {

	public int value;
	private bool attached;

	// Use this for initialization
	void Start () {
		attached = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public int getValue() {
		return value;
	}

	public void attachTo(Transform otherTransform) {
		rigidbody.isKinematic = true;
		collider.enabled = false;
		transform.parent = otherTransform;
	}
}
