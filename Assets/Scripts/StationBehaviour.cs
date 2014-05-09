using UnityEngine;
using System.Collections;

[RequireComponent (typeof(AudioSource))]
public class StationBehaviour : MonoBehaviour {

	public AudioClip activateStationSound;
	public Texture2D crosshairTex;
	public Color crosshairColor = Color.cyan;
	public int crosshairSize = 64;

	protected bool engaged = false;
	protected int crosshairLabel = -1;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	// draw the crosshairs
	void OnGUI() {
		if (engaged && crosshairTex != null) {
			Rect rect = new Rect(
				Input.mousePosition.x - crosshairSize / 2,
				Screen.height - Input.mousePosition.y - crosshairSize / 2,
				crosshairSize, crosshairSize);
			
			GUI.color = crosshairColor;
			GUI.DrawTexture(rect, crosshairTex);

			if (crosshairLabel >= 0) {
				rect = new Rect(Input.mousePosition.x + crosshairSize / 3,
				                Screen.height - Input.mousePosition.y + crosshairSize / 3, 100, 100);
				GUI.Label(rect, crosshairLabel.ToString());
			}
		}
	}

	public virtual void UseStation(bool engage, PigBehaviour player) {
		if (engage) {
			engaged = true;
			player.useStation(this);
		} else {
			engaged = false;
			player.useStation(null);
		}

		audio.PlayOneShot(activateStationSound);
	}

	protected Vector3 getRelativeMouse() {
		float x = Input.mousePosition.x - (Screen.width / 2f);
		float y = Input.mousePosition.y - (Screen.height / 2f);
		return new Vector3(x, y, 0f);
	}
}
