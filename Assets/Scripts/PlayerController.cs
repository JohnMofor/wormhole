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

	
	// Update is called once per frame
	void Update ()
	{

		if (Input.GetButton ("Fire1")) {

			Vector2 shipLocation = new Vector2 (transform.position.x, transform.position.z);
			Vector2 mouseLocation = CastRayToWorld ();
			haveClicked = true;
			Vector2 shipToMouse = mouseLocation - shipLocation;
			Vector3 shipPointTowards = new Vector3 (-(shipToMouse.x - shipLocation.x), 0, -(shipToMouse.y - shipLocation.y));
			//transform.LookAt(shipPointTowards);
			shipRotateTowards = Quaternion.LookRotation (shipPointTowards);
		}

		if (haveClicked) {
			GetComponent<Rigidbody> ().rotation = Quaternion.Slerp (GetComponent<Rigidbody> ().rotation, shipRotateTowards, Time.deltaTime * rotationSpeed);
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

	Vector2 CastRayToWorld ()
	{
		Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);  
		Vector3 point = ray.origin + (ray.direction * 4.5f);    
		return new Vector2 (point.x, point.z);
	}

}
