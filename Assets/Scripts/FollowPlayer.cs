using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{
	public GameObject backgroundImage;
	public Transform playerTransform;
	public float smooth = 3f;


	// Private
	private Renderer backgroundRenderer;
	private Boundary boundary = new Boundary ();

	private Vector3 clampToBoundary (Vector3 position)
	{
		float x = Mathf.Clamp (position.x, boundary.xMin, boundary.xMax);
		float z = Mathf.Clamp (position.z, boundary.zMin, boundary.zMax);
		return new Vector3 (x, transform.position.y, z);
	}

	void Start ()
	{
		if (backgroundImage == null) {
			throw new UnityException (gameObject.name + " --Background image not set!");
		}
		backgroundRenderer = backgroundImage.GetComponent<Renderer> ();
		float halfVerticalViewSize = Camera.main.orthographicSize;
		float halfHorizontalViewSize = halfVerticalViewSize * Screen.width / Screen.height;
		boundary.xMin = backgroundRenderer.bounds.center.x - backgroundRenderer.bounds.extents.x + halfHorizontalViewSize;
		boundary.xMax = backgroundRenderer.bounds.center.x + backgroundRenderer.bounds.extents.x - halfHorizontalViewSize;
		boundary.zMin = backgroundRenderer.bounds.center.z - backgroundRenderer.bounds.extents.z + halfVerticalViewSize;
		boundary.zMax = backgroundRenderer.bounds.center.z + backgroundRenderer.bounds.extents.z - halfVerticalViewSize;
		transform.position = clampToBoundary (playerTransform.position);
	}

	void FixedUpdate ()
	{
		if (playerTransform == null) {
			return;
		}
		transform.position = Vector3.Slerp (transform.position, clampToBoundary (playerTransform.position), Time.deltaTime * smooth);
		transform.position = clampToBoundary (transform.position);
	}


}
