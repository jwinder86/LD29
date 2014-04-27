using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Collider))]
[RequireComponent (typeof (Renderer))]
[RequireComponent (typeof(AudioSource))]
[RequireComponent (typeof (LineRenderer))]
public class GrappleStationBehaviour : MonoBehaviour, Station {

	public Transform submarine;

	public LootCounterBehaviour lootCounter;

	public CameraBehaviour subCamera;

	public ParticleSystem particles;

	public Texture2D crosshairTex;
	public Color crosshairColor;
	public int texSize = 64;

	public AudioClip fireSound;
	public AudioClip activateStationSound;
	public AudioClip lootSound;

	public float moveSpeed;
	public float moveTime;
	public float startDistance;

	private bool engaged;
	private bool firing;
	private float moveTimer;

	private LineRenderer line;

	private LootBehaviour currentLoot;

	// Use this for initialization
	void Start () {
		engaged = false;
		currentLoot = null;
		firing = false;
		renderer.enabled = false;
		line = GetComponent<LineRenderer>();
		line.SetVertexCount(2);
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

	void LateUpdate() {
		if (firing) {
			line.SetPosition(0, transform.position);
			line.SetPosition(1, subPosition());
		}
	}

	// draw the crosshairs
	void OnGUI() {
		if (engaged) {
			Rect rect = new Rect(
				Input.mousePosition.x - texSize / 2,
				Screen.height - Input.mousePosition.y - texSize / 2,
				texSize, texSize);

			GUI.color = crosshairColor;
			GUI.DrawTexture(rect, crosshairTex);
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
		particles.Emit(20);

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

	public void useStation(bool engage, PigBehaviour other) {
		audio.PlayOneShot(activateStationSound);
		if (engage) {
			engaged = true;
			other.useStation(this);
			subCamera.zoomCamera(true);
		} else {
			engaged = false;
			other.useStation(null);
			subCamera.zoomCamera(false);
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
		audio.PlayOneShot(lootSound);
	}

	void OnTriggerEnter(Collider other) {
		LootBehaviour loot = other.GetComponent<LootBehaviour>();
		if (firing && loot != null && currentLoot == null) {
			attachLoot(loot);
		}
	}
}
