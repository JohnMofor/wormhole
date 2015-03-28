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
	public float keyboardVerticalSensitivity = 10F;
	public float keyboardHorizontalSensitivity = 10F;
	public float mouseVerticalSensitivity = 10F;
	public float mouseHorizontalSensitivity = 10F;
	public float mouseCenterBoxHeight = 100F;
	public float mouseCenterBoxWidth = 100F;
	//public float fireRate = 0.25f;
	public float thrust = 50F;
	public float topSpeed = 50F;
	public float topRotationSpeed = 10F;

	//public GameObject shot;
	public Boundary boundary;
	public bool invertX = false;
	public bool invertY = false;
	public bool useMouse = true;
	public bool useForcesForRotation = false;
	public Transform shotSpawn;
	public GameObject playerExplosion;


	// Private
	private Rigidbody rb;
	
	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
	}
	
	// Update for physics.
	void FixedUpdate ()
	{
		float horizontalForce = 0f;
		float verticalForce = 0f;

		// Look inversions.
		int xDir = invertX ? -1 : 1;
		int yDir = invertY ? -1 : 1;

		// Look.
		if (useMouse) {


			Vector3 mouseOffset = (Input.mousePosition - new Vector3 (Screen.width / 2f, Screen.height / 2f, 0));
			if ((Mathf.Abs (mouseOffset.x) - (mouseCenterBoxWidth / 2f)) > 0) {
				horizontalForce = (mouseOffset.x + (-Mathf.Sign (mouseOffset.x) * mouseCenterBoxWidth / 2f));
				horizontalForce *= Time.deltaTime * mouseHorizontalSensitivity;
			}
			if ((Mathf.Abs (mouseOffset.y) - (mouseCenterBoxHeight / 2f)) > 0) {
				verticalForce = (mouseOffset.y + (-Mathf.Sign (mouseOffset.y) * mouseCenterBoxHeight / 2f));
				verticalForce *= Time.deltaTime * mouseVerticalSensitivity;
			}
		} else {
			horizontalForce = Input.GetAxis ("Horizontal") * keyboardHorizontalSensitivity * Time.deltaTime;
			verticalForce = Input.GetAxis ("Vertical") * keyboardVerticalSensitivity * Time.deltaTime;
		}

		// Applying rotation.
		// NB: Unity uses Left hand grip rule (LHR)!!
		if (useForcesForRotation) {
			rb.AddTorque (xDir * transform.up /*left to right*/ * horizontalForce);
			rb.AddTorque (-yDir * transform.right /*up to down*/ * verticalForce);
		} else {
			rb.rotation *= Quaternion.Euler (-yDir * verticalForce, xDir * horizontalForce, 0f);
		}




		// Detect SpeedBoost Request.
		if (Input.GetKey (KeyCode.Space)) {
			rb.AddForce (transform.forward * thrust, ForceMode.Force);
		}
		// Impose Physical Limits.
		rb.velocity = Vector3.ClampMagnitude (rb.velocity, topSpeed);
		rb.maxAngularVelocity = topRotationSpeed;
		
	}
	
}
