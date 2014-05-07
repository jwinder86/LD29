using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(Collider))]
[RequireComponent (typeof(AudioSource))]
public class PigBehaviour : MonoBehaviour {
	
	private static Plane plane = new Plane(new Vector3(0f, 0f, 1f), Vector3.zero);
	private static Vector3 shoulderPos = new Vector3(0f, 1.5f, 0f);
	private static float rotateTime = 0.4f;
	
	public float runSpeed;

	public FeetCollider feetCollider;

	public Rigidbody submarineBody;
	public LadderCollider ladderCollider;
	public Collider ladderFloorPanel;

	public AudioClip deathSound;
	public AudioClip hitSound;

	private bool onGround;
	private bool isDead;
	private RigidbodyConstraints initialConstraints;

	private Vector3 screenCenter;

	private Animation animation;
	
	private Facing facingDirection;

	private StationBehaviour currentStation;
	private float stationTimer;

	private WaterClock waterClock;

	private enum Facing {Left, Right, Back};
	
	// Use this for initialization
	void Start () {
		Screen.showCursor = false;
		
		screenCenter = new Vector3(Screen.width / 2, Screen.height / 2, 0f);
	
		initialConstraints = rigidbody.constraints;
		
		animation = GetComponentInChildren<Animation>();

		waterClock = (WaterClock)FindObjectOfType(typeof(WaterClock));

		
		facingDirection = Facing.Right;
		isDead = false;
		currentStation = null;
		stationTimer = 0f;
	}
	
	void LateUpdate() {

	}
	
	// Update is called once per frame
	void Update () {
		if (!isDead) {
			if (currentStation == null) {
				rigidbody.WakeUp();

				Vector3 startVel = rigidbody.velocity;

				// default to floor enabled
				ladderFloorPanel.enabled = true;

				// move left and right
				if (feetCollider.OnGround() || ladderCollider.OnLadder()) {
					if (Input.GetAxisRaw("Horizontal") < 0f) {
						fixKinematic();
						rigidbody.velocity = new Vector3(submarineBody.velocity.x - runSpeed, rigidbody.velocity.y, 0f);
						animation.Play("RunAnimation", PlayMode.StopAll);
						
						if (facingDirection != Facing.Left) {
							Rotate(Facing.Left);
						}
						
					} else if (Input.GetAxisRaw("Horizontal") > 0f) {
						fixKinematic();
						rigidbody.velocity = new Vector3(submarineBody.velocity.x + runSpeed, rigidbody.velocity.y, 0f);
						animation.Play("RunAnimation", PlayMode.StopAll);
						
						if (facingDirection != Facing.Right) {
							Rotate(Facing.Right);
						}
						
					} else {
						animation.Play("StandAnimation", PlayMode.StopAll);
					}
					
				}
				//climb ladder
				if (ladderCollider.OnLadder()){
					if (Input.GetAxisRaw("Vertical") < 0f) {
						fixKinematic();
						rigidbody.velocity = new Vector3(submarineBody.velocity.x, submarineBody.velocity.y - runSpeed , 0f);
						ladderFloorPanel.enabled = false;
						animation.Play("StandAnimation", PlayMode.StopAll);

						if (facingDirection != Facing.Back) {
							Rotate(Facing.Back);
						}
						
					} else if (Input.GetAxisRaw("Vertical") > 0f) {
						fixKinematic();
						rigidbody.velocity = new Vector3(submarineBody.velocity.x, submarineBody.velocity.y + runSpeed,  0f);
						ladderFloorPanel.enabled = false;
						animation.Play("StandAnimation", PlayMode.StopAll);

						if (facingDirection != Facing.Back) {
							Rotate(Facing.Back);
						}
					}
				}

				//Debug.Log("Pig Start: " + startVel + ",  End: " + rigidbody.velocity + ",  Sub: " + submarineBody.velocity);

			} else {
				if (Input.GetButtonDown("Jump") && stationTimer <= 0f) {
					currentStation.UseStation(false, this);
				}

				if (stationTimer > 0f) {
					stationTimer -= Time.deltaTime;
				}
			}
		
		}

		//Debug.Log("Sub: " + submarineBody.velocity + ",  Pig: " + rigidbody.velocity);
	}
	

	
	private void Rotate(Facing direction) {
		StopAllCoroutines();
		StartCoroutine(RotateCoroutine(direction));
	}
	
	private IEnumerator RotateCoroutine(Facing direction) {
		facingDirection = direction;
		Quaternion startingRotation = transform.rotation;
		
		for (float t = 0; t <= rotateTime; t += Time.deltaTime) {
			switch (facingDirection) {
			case Facing.Left:
				transform.rotation = Quaternion.Slerp(startingRotation, Quaternion.Euler(new Vector3(0f, 270f, 0f)), t / rotateTime);
				break;
			case Facing.Right:
				transform.rotation = Quaternion.Slerp(startingRotation, Quaternion.Euler(new Vector3(0f, 90f, 0f)), t / rotateTime);
				break;
			case Facing.Back:
				transform.rotation = Quaternion.Slerp(startingRotation, Quaternion.Euler(new Vector3(0f, 0f, 0f)), t / rotateTime);
				break;
			}
			
			yield return null;
		}
		
		switch (facingDirection) {
		case Facing.Left:
			transform.rotation = Quaternion.Euler(new Vector3(0f, 270f, 0f));
			break;
		case Facing.Right:
			transform.rotation = Quaternion.Euler(new Vector3(0f, 90f, 0f));
			break;
		case Facing.Back:
			transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
			break;
		}
	}

	public void useStation(StationBehaviour station) {
		Debug.Log("Use Station: " + station);
		if (station != null) {
			rigidbody.isKinematic = true;
			collider.enabled = false;
			stationTimer = 0.2f; // wait 0.2 seconds
		} else {
			collider.enabled = true;
		}

		currentStation = station;
	}

	public void Die() {
		if (!isDead && waterClock.isGameRunning()) {
			isDead = true;
			Common.playSound(this.audio, deathSound);
			transform.localRotation = Quaternion.Euler(new Vector3(0f, -180f, 0f));

			useStation(null);

			animation.Play("StandAnimation", PlayMode.StopAll);
			transform.rigidbody.constraints = RigidbodyConstraints.FreezePositionZ;
			rigidbody.AddTorque(Random.rotation.eulerAngles, ForceMode.VelocityChange);
			
			waterClock.GameOver();
			
			//getScreen().HeavyShakeTime(0.3f);
		}
	}

	private void fixKinematic() {
		if (rigidbody.isKinematic) {
			rigidbody.isKinematic = false;
			rigidbody.WakeUp();
			rigidbody.velocity = submarineBody.velocity;
		}
	}

//	private FollowTransform getScreen() {
//		if (screen == null) {
//			screen = (FollowTransform) FindObjectOfType(typeof(FollowTransform));
//		}
//		
//		return screen;
//	}

}
