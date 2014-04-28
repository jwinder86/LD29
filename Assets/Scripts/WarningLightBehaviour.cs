using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Light))]
public class WarningLightBehaviour : MonoBehaviour {

	private float originalIntensity;
	private Color originalColor;

	private bool isWarning;

	// Use this for initialization
	void Start () {
		isWarning = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void enableWarning(bool enable) {
		if (enable && !isWarning) {
			StopAllCoroutines();
			StartCoroutine(WarningCoroutine());
			isWarning = true;
		} else if (!enable && isWarning) {
			StopAllCoroutines();
			light.intensity = originalIntensity;
			light.color = originalColor;
			isWarning = false;
		}
	}

	private IEnumerator WarningCoroutine() {
		while (true) {
			light.intensity = 1f;
			light.color = Color.red;

			yield return new WaitForSeconds(0.4f);

			light.intensity = originalIntensity;
			light.color = originalColor;

			yield return new WaitForSeconds(0.4f);
		}
	}
}
