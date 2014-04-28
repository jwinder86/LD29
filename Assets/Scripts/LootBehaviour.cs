using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (Collider))]
public class LootBehaviour : MonoBehaviour {

	public int value;
	public bool alertKraken = false;
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
		if (!attached) {
			rigidbody.isKinematic = true;
			collider.enabled = false;
			transform.parent = otherTransform;
			attached = true;

			if (alertKraken) {
				TentacleBehaviour[] tentacles = FindObjectsOfType(typeof(TentacleBehaviour)) as TentacleBehaviour[];
				foreach (TentacleBehaviour tentacle in tentacles) {
					tentacle.hide();
				}

				CameraBehaviour camera = FindObjectOfType(typeof(CameraBehaviour)) as CameraBehaviour;
				camera.HeavyShakeTime(3f);
			}
		}
	}
}
