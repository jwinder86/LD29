using UnityEngine;
using System.Collections;

public class MaxHeight : MonoBehaviour {

	public Transform follow;
	public float maxHeight;

	public bool fixedHeight = false;

	private Vector3 offset;

	// Use this for initialization
	void Start () {
		offset = transform.position - follow.position;
		transform.parent = null;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 newPos = follow.position + offset;
		if (fixedHeight || newPos.y > maxHeight) {
			transform.position = new Vector3(newPos.x, maxHeight, newPos.z);
		} else {
			transform.position = newPos;
		}
	}
}
