using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
	
}

[RequireComponent (typeof (Rigidbody))]
public class PlayerController : MonoBehaviour
{

	// Public members.
	public float speed = 1F;
	public float tilt = 2F;
	public float thrust = 50F;
	public float topSpeed = 50F;
	public float rotationSpeed = 4.0F;
	public float horizontalPadding = 5F;
	public float verticalPadding = 5F;
	public bool enableTilt = true;
	public bool followMouse = true;
	public Quaternion shipRotateTowards;
	public GameObject playerExplosion;
	public GameObject backgroundImage;

	// Private
	private Boundary boundary = new Boundary ();
	private Rigidbody rb;
	private float previousY;
	private Renderer backgroundRenderer;

	void Start ()
	{
		checkDependencies();
		rb = GetComponent<Rigidbody> ();
		shipRotateTowards = rb.rotation;
		previousY = shipRotateTowards.eulerAngles.y;

		// Setting boundaries
		backgroundRenderer = backgroundImage.GetComponent<Renderer> ();
		boundary.xMin = backgroundRenderer.bounds.center.x - backgroundRenderer.bounds.extents.x + horizontalPadding;
		boundary.xMax = backgroundRenderer.bounds.center.x + backgroundRenderer.bounds.extents.x - horizontalPadding;
		boundary.zMin = backgroundRenderer.bounds.center.z - backgroundRenderer.bounds.extents.z + verticalPadding;
		boundary.zMax = backgroundRenderer.bounds.center.z + backgroundRenderer.bounds.extents.z - verticalPadding;
	}

	private void checkDependencies(){
		if (backgroundImage == null) {
			throw new UnityException (gameObject.name + " --Background image not set!");
		}
		if (playerExplosion == null) {
			throw new UnityException (gameObject.name + " --playerExplosion Prefab not set!");
		}
	}
	
	// Update For non-phsycis updates.
	void Update ()
	{
		Vector3 mouseLocation = CastRayToWorld (Input.mousePosition);
		int direction = followMouse ? 1 : -1;
		Vector3 shipPointTowards = direction * (mouseLocation - transform.position);
		shipRotateTowards = Quaternion.LookRotation (shipPointTowards);
	}

	// Update for physics.
	void FixedUpdate ()
	{
		if (Input.GetKey (KeyCode.Space) || Input.GetMouseButton (0 /*left*/)) {
			Vector3 forwardXZ = new Vector3 (transform.forward.x, 0, transform.forward.z);
			rb.AddForce (forwardXZ * thrust, ForceMode.Force);
		}


		rb.velocity = Vector3.ClampMagnitude (rb.velocity, topSpeed);

		// Get the plain rotation components.
		Quaternion slerpComp = Quaternion.Slerp (rb.rotation, shipRotateTowards, Time.deltaTime * rotationSpeed);
		// Get the tilt componenets
		float tiltComp = enableTilt ? tilt * (slerpComp.eulerAngles.y - previousY) : 0;
		// Act on rb
		rb.rotation = Quaternion.Euler (tiltComp, slerpComp.eulerAngles.y, -tiltComp);
		// Prepare for next update.
		previousY = slerpComp.eulerAngles.y;
		// Lateral motion?
		//rb.rotation = Quaternion.Euler(rb.rotation.eulerAngles.x, rb.rotation.eulerAngles.y, rb.velocity.x * -tilt);

		// Impose physical limits
		float x = Mathf.Clamp (transform.position.x, boundary.xMin, boundary.xMax);
		float z = Mathf.Clamp (transform.position.z, boundary.zMin, boundary.zMax);
		transform.position = new Vector3 (x, transform.position.y, z);

	}

	Vector3 CastRayToWorld (Vector3 targetPosition)
	{
		Ray ray = Camera.main.ScreenPointToRay (targetPosition); 
		Vector3 point = ray.origin + (ray.direction * 1.0f); 
		//Instantiate (playerExplosion, point, Quaternion.identity); // was cool
		//Debug.DrawRay (ray.origin, ray.direction * 10);
		return point;
	}

}
