using UnityEngine;
using System.Collections;

public interface Station {

	void directionalInput(Vector2 moveVector);

	void mouseInputDown(Vector3 mousePos, bool leftButtonDown, bool rightButtonDown);

	void mouseInputHeld(Vector3 mousePos, bool leftButtonHeld, bool rightButtonHeld);

	void useStation(bool engage, PigBehaviour other);
}
