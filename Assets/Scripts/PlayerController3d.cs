using UnityEngine;
using System.Collections;
//
//[System.Serializable]
//public class Boundary
//{
//	public float xMin, xMax, zMin, zMax;
//	
//}

public class PlayerController3d : MonoBehaviour
{
	
	// Public members.
	public float speed;
	public float verticalSensitivity = 1F;
	public float tilt;
	public bool enableTilt = true;
	public float fireRate;
	public Boundary boundary;
	public GameObject shot;
	public float thrust = 5;
	public float topSpeed = 50;
	public bool followMouse = true;
	public Transform shotSpawn;
	public Quaternion shipRotateTowards;
	public float rotationSpeed = 4.0F;
	private Rigidbody rb;
	private float previousY;


	private Vector3 lastMouse;

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
//		Vector3 mouseLocation = CastRayToWorld (Input.mousePosition);
//		int direction = followMouse ? 1 : -1;
//		Vector3 shipPointTowards = direction * (mouseLocation - transform.position);
//		shipRotateTowards = Quaternion.LookRotation (shipPointTowards);
		
		
		
	}
	
	// Update for physics.
	void FixedUpdate ()
	{

//
//		// Detect Movement
//		
//		float h = Input.GetAxis("Horizontal") * verticalSensitivity * Time.deltaTime;
//		float v = Input.GetAxis("Vertical") * verticalSensitivity * Time.deltaTime;
//
//		rb.AddTorque(transform.up * h);
//		rb.AddTorque(transform.right * v);


//		if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) {
//
//		}
//
//		if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) {
//			
//		}
//
//		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) {
//			
//		}
//
//		if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) {
//			
//		}
		
		// Detect SpeedBoost 
		//if (Input.GetKey (KeyCode.Space)) {
		//	rb.AddForce (transform.forward * thrust, ForceMode.Force);
		//}
		//rb.velocity = Vector3.ClampMagnitude (rb.velocity, topSpeed);
		
		// Get the plain rotation components.
		//Quaternion slerpComp = Quaternion.Slerp (rb.rotation, shipRotateTowards, Time.deltaTime * rotationSpeed);
		// Get the tilt componenets
		//float tiltComp = enableTilt ? tilt * (slerpComp.eulerAngles.y - previousY) : 0;
		// Act on rb
		//rb.rotation = Quaternion.Euler (tiltComp, slerpComp.eulerAngles.y, -tiltComp);
		// Prepare for next update.
		//previousY = slerpComp.eulerAngles.y;
		// Lateral motion?
		//rb.rotation = Quaternion.Euler(rb.rotation.eulerAngles.x, rb.rotation.eulerAngles.y, h * -tilt);
		
	}
	
	Vector3 CastRayToWorld (Vector3 targetPosition)
	{
		Ray ray = Camera.main.ScreenPointToRay (targetPosition); 
		Vector3 point = ray.origin + (ray.direction * 1.0f); 
		Instantiate (playerExplosion, point, Quaternion.identity); // was cool
		//Debug.DrawRay (ray.origin, ray.direction * 10);
		return point;
	}
	
}
