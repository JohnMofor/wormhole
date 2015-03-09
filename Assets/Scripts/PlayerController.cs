using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary {
	public float xMin, xMax, zMin, zMax;

}

public class PlayerController : MonoBehaviour {

	// Public members.
	public float speed;
	public float tilt;
	public float fireRate;

	public Boundary boundary;

	public GameObject shot;
	public Transform shotSpawn;

	// Private members.
	private float nextFire = 0.0F;

	
	// Update is called once per frame
	void Update () {

		if (Input.GetButton("Fire1") && Time.time > nextFire) {
			nextFire = Time.time + fireRate;
			Instantiate (shot, shotSpawn.position, shotSpawn.rotation);
			GetComponent<AudioSource>().Play();
		}


	
	}

	// Update for physics.
	void FixedUpdate() {
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Rigidbody rb = GetComponent<Rigidbody>();

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		rb.velocity = movement * speed;

		rb.position = new Vector3
		(
			Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
		);

		rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);


	}
}
