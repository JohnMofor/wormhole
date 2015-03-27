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
	public float fireRate;
	public Boundary boundary;
	public GameObject shot;
	public Transform shotSpawn;
	private bool haveClicked = false;
	public Quaternion shipRotateTowards;


	// Private members.
	public float rotationSpeed = 4.0F;

	// Debug
	public GameObject playerExplosion;

	
	// Update is called once per frame
	void Update ()
	{
		Rigidbody rb = GetComponent<Rigidbody> ();
		if (Input.GetButton ("Fire1")) {
			Vector3 mouseLocation = CastRayToWorld (Input.mousePosition);
			haveClicked = true;
			Vector3 shipPointTowards = transform.position - mouseLocation; //-(mouseLocation - transform.position);
			shipPointTowards.y = 0; // Cancel the y rotation.
			shipRotateTowards = Quaternion.LookRotation (shipPointTowards);
		}

		if (haveClicked) {
			rb.rotation = Quaternion.Slerp (rb.rotation, shipRotateTowards, Time.deltaTime * rotationSpeed);
		}


	
	}

	// Update for physics.
	void FixedUpdate ()
	{
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		Rigidbody rb = GetComponent<Rigidbody> ();

		Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);
		rb.velocity = movement * speed;

		rb.position = new Vector3
		(
			Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
		);

		//rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);


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
