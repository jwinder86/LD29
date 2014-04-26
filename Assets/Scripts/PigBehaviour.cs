using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(Collider))]
[RequireComponent (typeof(AudioSource))]
public class PigBehaviour : MonoBehaviour {
	
	private static Plane plane = new Plane(new Vector3(0f, 0f, 1f), Vector3.zero);
	private static Vector3 shoulderPos = new Vector3(0f, 1.5f, 0f);
	private static float rotateTime = 0.4f;
	
	//private static FollowTransform screen;
	
	public float runSpeed;
	
	public FeetCollider feetCollider;
	
	public AudioClip deathSound;
	public AudioClip hitSound;

	private bool onGround;

	
	private RigidbodyConstraints initialConstraints;

	private Vector3 screenCenter;

	private Animation animation;
	
	private bool facingRight;

	private Station currentStation;
	private float stationTimer;
	
	// Use this for initialization
	void Start () {
		Screen.showCursor = false;
		
		screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0f);
	
		initialConstraints = rigidbody.constraints;
		
		animation = GetComponentInChildren<Animation>();

		
		facingRight = true;

		currentStation = null;
		stationTimer = 0f;
	}
	
	void LateUpdate() {

	}
	
	// Update is called once per frame
	void Update () {
		if (currentStation == null) {
			rigidbody.WakeUp();

			// move left and right
			if (feetCollider.OnGround()) {
				if (Input.GetAxis("Horizontal") < 0f) {
					rigidbody.velocity = new Vector3(-runSpeed, rigidbody.velocity.y, 0f);
					animation.Play("RunAnimation", PlayMode.StopAll);
					
					if (facingRight) {
						Rotate(false);
					}
					
				} else if (Input.GetAxis("Horizontal") > 0f) {
					rigidbody.velocity = new Vector3(runSpeed, rigidbody.velocity.y, 0f);
					animation.Play("RunAnimation", PlayMode.StopAll);
					
					if (!facingRight) {
						Rotate(true);
					}
					
				} else {
					animation.Play("StandAnimation", PlayMode.StopAll);
				}
				
			}
		} else {
			if (Input.GetButtonDown("Jump") && stationTimer <= 0f) {
				currentStation.useStation(false, this);
			}

			if (stationTimer > 0f) {
				stationTimer -= Time.deltaTime;
			}
		}
	}
	

	
	private void Rotate(bool faceRight) {
		StopAllCoroutines();
		StartCoroutine(RotateCoroutine(faceRight));
	}
	
	private IEnumerator RotateCoroutine(bool faceRight) {
		facingRight = faceRight;
		
		for (float t = 0; t <= rotateTime; t += Time.deltaTime) {
			if (faceRight) {			
				transform.rotation = Quaternion.Slerp(Quaternion.Euler(new Vector3(0f, 270f, 0f)), Quaternion.Euler(new Vector3(0f, 90f, 0f)), t / rotateTime);
			} else {
				transform.rotation = Quaternion.Slerp(Quaternion.Euler(new Vector3(0f, 90f, 0f)), Quaternion.Euler(new Vector3(0f, 270f, 0f)), t / rotateTime);
			}
			
			yield return null;
		}
		
		if (faceRight) {			
			transform.rotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
		} else {
			transform.rotation = Quaternion.Euler(new Vector3(0f, 270f, 0f));
		}
	}

	public void useStation(Station station) {
		Debug.Log("Use Station: " + station);
		if (station != null) {
			rigidbody.isKinematic = true;
			collider.enabled = false;
			stationTimer = 0.2f; // wait 2 seconds
		} else {
			rigidbody.isKinematic = false;
			collider.enabled = true;
		}

		currentStation = station;
	}
}
