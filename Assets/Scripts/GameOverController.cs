using UnityEngine;
using System.Collections;

public class GameOverController : MonoBehaviour {

	public Transform cam;
	public Transform marker;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log("in GO Controller");
		if(Input.GetKeyDown("g")){
			Debug.Log ("trying to move cam");
			goToDeadGameOver();
		}

	}


	public void goToDeadGameOver(){

			StopAllCoroutines();

		cam.position = marker.position;

//		cam.position = Vector3.Lerp (cam.transform.position, marker.position, 5);
		Debug.Log (marker.position);
		Debug.Log (cam.position);
				//Transform cam = (CameraBehaviour) FindObjectOfType(typeof(CameraBehaviour));
			//cam.position = Vector3.Lerp(start.position, end.position, t / transitionTime);
			
			//StartCoroutine(ExitLevel(false));
		}
}
