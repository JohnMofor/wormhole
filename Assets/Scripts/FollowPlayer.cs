using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{
	public GameObject backgroundImage;
	public Transform playerTransform;


	// Private
	private Renderer backgroundRenderer;
	private Boundary boundary = new Boundary ();

	void Start ()
	{
		if (backgroundImage) {
			backgroundRenderer = backgroundImage.GetComponent<Renderer> ();
			float halfVerticalViewSize = Camera.main.orthographicSize;
			float halfHorizontalViewSize = halfVerticalViewSize * Screen.width / Screen.height;
			boundary.xMin = backgroundRenderer.bounds.center.x - backgroundRenderer.bounds.extents.x + halfHorizontalViewSize;
			boundary.xMax = backgroundRenderer.bounds.center.x + backgroundRenderer.bounds.extents.x - halfHorizontalViewSize;
			boundary.zMin = backgroundRenderer.bounds.center.z - backgroundRenderer.bounds.extents.z + halfVerticalViewSize;
			boundary.zMax = backgroundRenderer.bounds.center.z + backgroundRenderer.bounds.extents.z - halfVerticalViewSize;
		} else {
			throw new UnityException(gameObject.name+ " --Background image not set!");
		}
	}

	// Update is called once per frame
	void Update ()
	{
		if (playerTransform == null) {
			return;
		}
		float x = Mathf.Clamp (playerTransform.position.x, boundary.xMin, boundary.xMax);
		float z = Mathf.Clamp (playerTransform.position.z, boundary.zMin, boundary.zMax);
		transform.position = new Vector3 (x, transform.position.y, z);
	}


}
