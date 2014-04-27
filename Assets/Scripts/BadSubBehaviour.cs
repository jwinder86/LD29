using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(Collider))]
public class BadSubBehaviour : MonoBehaviour {

	public float maxSpeed;
	public float subAccel;
	public float rotateTime = 0.4f;
	public float moveTime = 3f;
	public bool startRight;

	public ExplosionBehaviour explosionPrefab;
	public RocketBehaviour rocketPrefab;
	public LootBehaviour lootPrefab;

	public float respawnDistance;
	public float respawnTime = 5f;
	private float respawnTimer;

	public float playerFireDistance;
	public float rocketStartDistance;
	public float rocketSpeed;
	public float fireDelay;
	private float fireTimer;
	
	private bool moveRight;
	private bool alive;
	private Renderer[] renderers;
	private Vector3 startPos;

	private Transform playerLocation;

	// Use this for initialization
	void Start () {
		playerLocation = (FindObjectOfType(typeof(MovementStationBehaviour)) as MovementStationBehaviour).GetComponent<Transform>();

		renderers = GetComponentsInChildren<Renderer>();
		startPos = transform.position;

		StartMoving();
	}
	
	// Update is called once per frame
	void Update () {
		if (alive) {
			if (moveRight && rigidbody.velocity.x < maxSpeed) {
				rigidbody.AddForce(new Vector3(subAccel, 0f, 0f), ForceMode.Acceleration);
			} else if (!moveRight && rigidbody.velocity.x > -maxSpeed) {
				rigidbody.AddForce(new Vector3(-subAccel, 0f, 0f), ForceMode.Acceleration);
			}

			if (fireTimer <= 0f && (transform.position - playerLocation.position).magnitude < playerFireDistance) {
				FireAtPlayer();
			}
		} else {
			if (respawnTimer <= 0f && (playerLocation.position - startPos).magnitude > respawnDistance) {
				StartMoving();
			}
		}

		if (respawnTimer > 0f) {
			respawnTimer -= Time.deltaTime;
		}

		if (fireTimer > 0f) {
			fireTimer -= Time.deltaTime;
		}
	}

	private void StartMoving() {
		foreach (Renderer renderer in renderers) {
			renderer.enabled = true;
		}
		rigidbody.isKinematic = false;
		collider.enabled = true;
		alive = true;
		respawnTimer = 0f;
		fireTimer = 0f;

		moveRight = startRight;
		transform.position = startPos;
		transform.rotation = Quaternion.Euler(new Vector3(0f, moveRight ? 0f : 180f , 0f));
		StopAllCoroutines();
		StartCoroutine(ChangeDirection());
	}

	public void Destroy() {
		ExplosionBehaviour explosion = (ExplosionBehaviour) Instantiate(explosionPrefab, transform.position, Quaternion.identity);
		explosion.ExplodeAndInstantiate(lootPrefab.gameObject);

		foreach (Renderer renderer in renderers) {
			renderer.enabled = false;
		}
		rigidbody.isKinematic = true;
		collider.enabled = false;
		alive = false;

		respawnTimer = respawnTime;
		StopAllCoroutines();
	}

	private void FireAtPlayer() {
		Vector3 direction = (playerLocation.position - transform.position).normalized;

		if (Vector3.Angle(direction, transform.right) > 60) {
			return;
		}

		float angle = Mathf.Rad2Deg * Mathf.Atan2(direction.y, direction.x);
		RocketBehaviour rocket = (RocketBehaviour) Instantiate(rocketPrefab, transform.position + direction * rocketStartDistance, Quaternion.Euler(new Vector3(0f, 0f, angle)));
		rocket.rigidbody.velocity = direction * rocketSpeed;

		fireTimer = fireDelay;
	}

	private IEnumerator ChangeDirection() {
		while (true) {
			moveRight = !moveRight;
			
			for (float t = 0; t <= rotateTime; t += Time.deltaTime) {
				if (moveRight) {			
					transform.rotation = Quaternion.Slerp(Quaternion.Euler(new Vector3(0f, 180f, 0f)), Quaternion.Euler(new Vector3(0f, 0f, 0f)), t / rotateTime);
				} else {
					transform.rotation = Quaternion.Slerp(Quaternion.Euler(new Vector3(0f, 0f, 0f)), Quaternion.Euler(new Vector3(0f, 180f, 0f)), t / rotateTime);
				}
				
				yield return null;
			}
			
			if (moveRight) {			
				transform.rotation = Quaternion.Euler(new Vector3(0f, 0f, 0f));
			} else {
				transform.rotation = Quaternion.Euler(new Vector3(0f, 180f, 0f));
			}
			
			yield return new WaitForSeconds(moveTime);
			
		}
	}
}
