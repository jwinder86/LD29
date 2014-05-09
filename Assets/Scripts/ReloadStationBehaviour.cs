using UnityEngine;
using System.Collections;

[RequireComponent (typeof (AudioSource))]
public class ReloadStationBehaviour : StationBehaviour {

	public int maxRockets;
	public float reloadTime;
	public TextMesh counter;

	public ReloadAnimBehaviour reloadAnim;
	public AudioClip addRocket;
	public AudioClip lastRocket;

	private int currentRockets;
	private float reloadTimer;
	private Color originalTextColor;

	// Use this for initialization
	void Start () {
		currentRockets = maxRockets;
		reloadTimer = 0f;
		originalTextColor = counter.color;
	}
	
	// Update is called once per frame
	void Update () {
		if (engaged) {
			reloadTimer += Time.deltaTime;

			if (reloadTimer > reloadTime) {
				loadOne();
				reloadTimer -= reloadTime;
			}
		}

		counter.text = "Ammo: " + currentRockets.ToString();
		if (currentRockets <= maxRockets / 4) {
			counter.color = Color.red;
		} else {
			counter.color = originalTextColor;
		}
	}

	private void loadOne() {
		if (currentRockets < maxRockets) {
			currentRockets++;
			reloadAnim.AnimateReload();
			if (currentRockets == maxRockets) {
				audio.PlayOneShot(lastRocket);
			} else {
				audio.PlayOneShot(addRocket);
			}
		}
	}

	public int GetRocketCount() {
		return currentRockets;
	}

	public bool UseRocket() {
		if (currentRockets > 0) {
			currentRockets--;
			return true;
		} else {
			return false;
		}
	}

	public override void UseStation(bool engage, PigBehaviour player) {
		base.UseStation(engage, player);
		reloadTimer = 0f;
	}
}
