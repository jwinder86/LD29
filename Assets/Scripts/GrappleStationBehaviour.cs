using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Collider))]
[RequireComponent (typeof (Renderer))]
[RequireComponent (typeof(AudioSource))]
[RequireComponent (typeof (LineRenderer))]
public class GrappleStationBehaviour : MovementStationBehaviour {

	public Transform submarine;

	public LootCounterBehaviour lootCounter;

	//public CameraBehaviour subCamera;
	public SpotlightBehaviour spotlight;

	public ParticleSystem grappleParticles;

	public AudioClip fireSound;
	public AudioClip lootSound;

	public float moveSpeed;
	public float moveTime;
	public float startDistance;

	private bool firing;
	private float moveTimer;

	private LineRenderer line;

	private LootBehaviour currentLoot;

	// Use this for initialization
	void Start () {
		currentLoot = null;
		firing = false;
		renderer.enabled = false;
		line = GetComponent<LineRenderer>();
		line.SetVertexCount(2);
	}
	
	// Update is called once per frame
	void Update () {
		directionalInput(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));

		if (engaged) {
			Vector3 fireDirection = getRelativeMouse().normalized;
			if (!firing && Input.GetButtonDown("Fire1")) {
				StartCoroutine(FireCoroutine(fireDirection));
			}

			spotlight.RotateTo(Mathf.Atan2(fireDirection.y, fireDirection.x) * Mathf.Rad2Deg);
		}
	}

	void LateUpdate() {
		if (firing) {
			line.SetPosition(0, transform.position);
			line.SetPosition(1, subPosition());
		}
	}

	IEnumerator FireCoroutine(Vector3 direction) {
		firing = true;
		moveTimer = 0f;
		float angle = Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x) - 90f;
		transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
		transform.position = subPosition() + direction * startDistance;
		renderer.enabled = true;
		line.enabled = true;
		transform.parent = null;

		audio.PlayOneShot(fireSound);
		grappleParticles.Emit(20);

		// move out
		while (moveTimer < moveTime && currentLoot == null) {
			transform.position += direction * moveSpeed * Time.deltaTime;
			moveTimer += Time.deltaTime;

			yield return null;
		}

		Vector3 farthestPosition = transform.position;
		float maxTimer = moveTimer;

		while (moveTimer > 0f) {
			transform.position = Vector3.Lerp(farthestPosition, subPosition(), 1f - moveTimer / maxTimer);
			moveTimer -= Time.deltaTime;

			if ((transform.position - subPosition()).magnitude < startDistance) {
				break;
			}
			
			yield return null;
		}

		firing = false;
		moveTimer = 0f;
		renderer.enabled = false;
		line.enabled = false;

		if (currentLoot != null) {
			lootCounter.addLoot(currentLoot.getValue());
			Destroy(currentLoot.gameObject);
			currentLoot = null;
		}
	}

	private Vector3 subPosition() {
		return new Vector3(submarine.position.x, submarine.position.y, transform.position.z);
	}

	private void attachLoot(LootBehaviour loot) {
		this.currentLoot = loot;
		loot.attachTo(transform);
		audio.PlayOneShot(lootSound);
	}

	void OnTriggerEnter(Collider other) {
		LootBehaviour loot = other.GetComponent<LootBehaviour>();
		if (firing && loot != null && currentLoot == null) {
			attachLoot(loot);
		}
	}

	public void directionalInput(Vector2 moveVector) {
		if (engaged) {
			if(!audio.isPlaying){
				Common.playSound(this.audio, movementSound);
			}			
			if (submarine.rigidbody.velocity.magnitude < this.maxSpeed) {
				submarine.rigidbody.AddForce(moveVector * this.movementAccel, ForceMode.Acceleration);
			}
		}	
		
	}
}
