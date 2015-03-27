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
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetButton ("Fire1")) {
			Vector3 mouseLocation = CastRayToWorld (Input.mousePosition);
			Vector3 shipPointTowards = transform.position - mouseLocation; //-(mouseLocation - transform.position);
			shipRotateTowards = Quaternion.LookRotation (shipPointTowards);
		}



	}

	// Update for physics.
	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.velocity = movement * speed;

		rb.position = new Vector3
		(
			Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
		);

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
