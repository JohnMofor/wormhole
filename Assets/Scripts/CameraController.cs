using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public Transform playerCameraPlaceholder;

	void FixedUpdate() {
		transform.position = playerCameraPlaceholder.position;
		transform.rotation = playerCameraPlaceholder.rotation;
	}
}
