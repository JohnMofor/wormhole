using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

	public Transform playerCameraPlaceholder;

	void FixedUpdate ()
	{
		transform.position = playerCameraPlaceholder.position;
		transform.rotation = Quaternion.Euler (playerCameraPlaceholder.rotation.eulerAngles.x,
		                                      playerCameraPlaceholder.rotation.eulerAngles.y,
		                                      0 * playerCameraPlaceholder.rotation.eulerAngles.z /* let the player bank like a boss */
		);
	}
}
