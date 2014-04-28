using UnityEngine;
using System.Collections;

public class PeriscopeBehaviour : MonoBehaviour {

	public Rigidbody subBody;
	public float rotateTime = 1f;

	private bool lookingRight;

	// Use this for initialization
	void Start () {
		lookingRight = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (subBody.velocity.x > 0 && !lookingRight) {
			Rotate(true);
		} else if (subBody.velocity.x < 0 && lookingRight) {
			Rotate(false);
		}
	}

	private void Rotate(bool lookRight) {
		StopAllCoroutines();
		StartCoroutine(RotateCoroutine(lookRight));
	}
	
	private IEnumerator RotateCoroutine(bool lookRight) {
		lookingRight = lookRight;
		Quaternion startingRotation = transform.rotation;
		
		for (float t = 0; t <= rotateTime; t += Time.deltaTime) {
			if (lookRight) {
				transform.rotation = Quaternion.Slerp(startingRotation, Quaternion.Euler(new Vector3(0f, 180f, 0f)), t / rotateTime);
			} else {
				transform.rotation = Quaternion.Slerp(startingRotation, Quaternion.Euler(new Vector3(0f, 0f, 0f)), t / rotateTime);
			}
			
			yield return null;
		}
		
		if (lookRight) {
			transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
		} else {
			transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
		}
	}
}
