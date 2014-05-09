using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Transform))]
[RequireComponent (typeof(AudioSource))]
public class MovementStationBehaviour : StationBehaviour {

	public CameraBehaviour subCamera;

	public float movementAccel = 50f;
	public float maxSpeed = 15f;
	public AudioClip movementSound;

	public ParticleSystem particles;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		directionalInput(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
	}

	public void directionalInput(Vector2 moveVector) {
		if (engaged) {
			if(!audio.isPlaying){
				Common.playSound(this.audio, movementSound);
			}			
			if (rigidbody.velocity.magnitude < maxSpeed) {
				rigidbody.AddForce(moveVector * movementAccel, ForceMode.Acceleration);
			}
		}	

	}

	public override void UseStation(bool engage, PigBehaviour player) {
		base.UseStation(engage, player);

		if (engage) {
			subCamera.zoomCamera(true);
			particles.Play();
		} else {
			subCamera.zoomCamera(false);
			particles.Stop();
		}

	}

	public void sinkSub(){
		engaged = false;
		subCamera.zoomCamera(true);
		subCamera.HeavyShakeTime(1.5f);
		//rigidbody.isKinematic = false;
		rigidbody.useGravity = true;
		rigidbody.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationX;
		rigidbody.AddTorque(new Vector3(0f, 0f, Random.Range(-1f, 1f)) * 10f, ForceMode.Acceleration);
		subCamera.transform.parent = null;
	}


}
