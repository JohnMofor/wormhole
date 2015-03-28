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
	public float baseSpeed = 2f;
	public float verticalSensitivity = 10F;
	public float horizontalSensitivity = 10F;
	public float tilt = 2F;
	//public float stabilitySpeed = 20f;
	public float stability = 1F;
	//public float fireRate = 0.25f;
	public float thrust = 5;
	public float topSpeed = 50;
	public float rotationSpeed = 4.0F;
	public float maxAngle = 89;

	//public GameObject shot;
	public Boundary boundary;
	public bool invertX = false;
	public bool invertY = false;
	public bool enableTilt = true;
	//public bool followMouse = true;

	public Transform shotSpawn;

	// Debug
	public GameObject playerExplosion;


	// Private
	private Quaternion baseRotation;
	private Rigidbody rb;

	
	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		baseRotation = rb.rotation;
		//previousY = shipRotateTowards.eulerAngles.y;
	}
	
	// Update For non-phsycis updates.
	void Update ()
	{
//		Vector3 mouseLocation = CastRayToWorld (Input.mousePosition);
//		int direction = followMouse ? 1 : -1;
//		Vector3 shipPointTowards = direction * (mouseLocation - transform.position);
//		shipRotateTowards = Quaternion.LookRotation (shipPointTowards);
		//if (Input.GetKey(KeyCode.Space)) {

		//}
		//CastRayToWorld(Input.mousePosition);
		
	}
	private float rotationZ = 0f;

	// Update for physics.
	void FixedUpdate ()
	{

//
//		// Detect Movement
//		
		float h = Input.GetAxis ("Horizontal") * horizontalSensitivity * Time.deltaTime;
		float v = Input.GetAxis ("Vertical") * verticalSensitivity * Time.deltaTime;

		int xDir = invertX ? -1 : 1;
		int yDir = invertY ? -1 : 1;
		//rb.rotation = Quaternion.Euler(rb.transform.eulerAngles.x + v, rb.transform.eulerAngles.y + h, rb.transform.eulerAngles.z);

		// Unity uses Left hand grip rule (LHR)!!
		rb.AddTorque (xDir * transform.up /*left to right*/ * h);
		rb.AddTorque (-yDir * transform.right /*up to down*/ * v);
		//rb.transform.rotation = Mathf.Clamp (rb.transform.rotation.z, -90, 90);
		//transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, -rotationZ);

		//Quaternion rotationZ = Mathf.Clamp (rotationZ, -90, 90);


		//rb.AddTorque (transform.forward /* slanting */ * Mathf.Cos(Vector3.Angle(transform.up, transform.right)));

//		Vector3 predictedUp = Quaternion.AngleAxis(
//			rb.angularVelocity.magnitude * Mathf.Rad2Deg * stability / (Time.deltaTime*horizontalSensitivity),
//			rb.angularVelocity
//			) * transform.up;
//		
//		Vector3 torqueVector = Vector3.Cross(predictedUp, Vector3.up);
//		// Uncomment the next line to stabilize on only 1 axis.
//		torqueVector = Vector3.Project(torqueVector, transform.forward);
//		rb.AddTorque(torqueVector * 100);
//
//		if (Quaternion.Angle(rb.rotation, baseRotation) <= maxAngle ) {
//			//rb.rotation;
//		}


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
		if (Input.GetKey (KeyCode.Space)) {
			rb.AddForce (transform.forward * thrust, ForceMode.Force);
		}
		rb.velocity = Vector3.ClampMagnitude (rb.velocity, topSpeed);
		
		// Get the plain rotation components.
		//Quaternion slerpComp = Quaternion.Slerp (rb.rotation, Quaternion.LookRotation(CastRayToWorld(Input.mousePosition)), Time.deltaTime * rotationSpeed);
		//rb.rotation = Quaternion.Euler(rb.transform.eulerAngles.x, rb.transform.eulerAngles.y, rb.transform.eulerAngles.z);
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
		//Debug.Log (point);
		//Instantiate (playerExplosion, point, Quaternion.identity); // was cool
		//Debug.DrawRay (ray.origin, ray.direction * 10);
		return point;
	}
	
}
