using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;

}

public class PlayerController : MonoBehaviour
{

	// Public members.
	public float speed;
	public float tilt;
	public bool enableTilt = true;
	public float fireRate;
	public Boundary boundary;
	public GameObject shot;
	public float thrust;
	public float topSpeed = 50;
	public bool followMouse = true;
	public Transform shotSpawn;
	public Quaternion shipRotateTowards;
	public float rotationSpeed = 4.0F;
	private Rigidbody rb;
	private float previousY;
	
	// Debug
	public GameObject playerExplosion;

	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		shipRotateTowards = rb.rotation;
		previousY = shipRotateTowards.eulerAngles.y;
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
		if (Input.GetKey (KeyCode.Space)) {
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
