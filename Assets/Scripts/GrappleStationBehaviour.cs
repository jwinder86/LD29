using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Collider))]
[RequireComponent (typeof (Renderer))]
public class GrappleStationBehaviour : MonoBehaviour, Station {

	public Transform submarine;

	public float moveSpeed;
	public float moveTime;

	private bool engaged;
	private bool firing;
	private float moveTimer;

	private LootBehaviour currentLoot;

	// Use this for initialization
	void Start () {
		engaged = false;
		currentLoot = null;
		firing = false;
		renderer.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (engaged) {
			if (!firing && Input.GetButtonDown("Fire1")) {
				Vector3 fireDirection = relativeMouse(Input.mousePosition).normalized;
				StartCoroutine(FireCoroutine(fireDirection));
			}
		}
	}

	IEnumerator FireCoroutine(Vector3 direction) {
		firing = true;
		moveTimer = 0f;
		transform.position = subPosition();
		renderer.enabled = true;

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
			
			yield return null;
		}

		firing = false;
		moveTimer = 0f;
		renderer.enabled = false;

		if (currentLoot != null) {
			Destroy(currentLoot.gameObject);
			currentLoot = null;
		}
	}

	public void useStation(bool engage, PigBehaviour other) {
		if (engage) {
			engaged = true;
			other.useStation(this);
		} else {
			engaged = false;
			other.useStation(null);
		}
	}

	private Vector3 relativeMouse(Vector3 mousePos) {
		float x = mousePos.x - (Screen.width / 2f);
		float y = mousePos.y - (Screen.height / 2f);
		return new Vector3(x, y, 0f);
	}

	private Vector3 subPosition() {
		return new Vector3(submarine.position.x, submarine.position.y, transform.position.z);
	}

	private void attachLoot(LootBehaviour loot) {
		this.currentLoot = loot;
		loot.attachTo(transform);
	}

	void OnTriggerEnter(Collider other) {
		LootBehaviour loot = other.GetComponent<LootBehaviour>();
		if (firing && loot != null && currentLoot == null) {
			attachLoot(loot);
		}
	}
}
