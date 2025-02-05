﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

// For debugging
using System;

[System.Serializable]
public class Boundary
{
	public float xMin, xMax, zMin, zMax;
	
}

[RequireComponent (typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

	//// Public members.
	public float speed = 1F;
	// /*not in 2d*/ public float tilt = 2F;
	public float thrust = 50F;
	public float topSpeed = 50F;
	public float rotationSpeed = 4.0F;
	public float horizontalPadding = 5F;
	public float verticalPadding = 5F;
	public float scaleInWormhole = 0.1F;
	// /*not in 2d*/ public bool enableTilt = true;
	public bool followMouse = true;
	public Quaternion shipRotateTowards;
	public GameObject playerExplosion;
	public GameObject backgroundImage;

	// Public GameObjects
	public Slider speedSlider;
	public Slider topSpeedSlider;
	public Slider rotationSpeedSlider;
	public Toggle followMouseToggle;
	public Slider tiltSlider;
	public Toggle tiltToggle;
	public Slider thrustSlider;

	//// Private
	private Boundary boundary = new Boundary ();
	private Rigidbody rb;
	private ParticleRenderer particleRenderer;
	private Renderer backgroundRenderer;
	private float increment = 0.003f;
	// /*not in 2d*/ private float previousY;
	private Vector3 startScale;

	// Teleportation logic.
	private enum TeleportationState
	{
		ToDestination1,
		AtDestination1,
		ToDestination2,
		AtDestination2,
		Done}
	;
	private TeleportationState teleportationState = TeleportationState.Done;

	void Start ()
	{
		checkDependencies ();
		rb = GetComponent<Rigidbody> ();
		shipRotateTowards = rb.rotation;
		// /*not in 2d*/ previousY = shipRotateTowards.eulerAngles.y;

		// Setting boundaries
		backgroundRenderer = backgroundImage.GetComponent<Renderer> ();
		boundary.xMin = backgroundRenderer.bounds.center.x - backgroundRenderer.bounds.extents.x + horizontalPadding;
		boundary.xMax = backgroundRenderer.bounds.center.x + backgroundRenderer.bounds.extents.x - horizontalPadding;
		boundary.zMin = backgroundRenderer.bounds.center.z - backgroundRenderer.bounds.extents.z + verticalPadding;
		boundary.zMax = backgroundRenderer.bounds.center.z + backgroundRenderer.bounds.extents.z - verticalPadding;

		// Setting sliders for settings.
		speedSlider.value = speed;
		topSpeedSlider.value = topSpeed;
		rotationSpeedSlider.value = rotationSpeed;
		followMouseToggle.isOn = followMouse;
//		tiltSlider.value = 0f;
		thrustSlider.value = thrust;
		tiltToggle.isOn = false;

		// Saving game initial state
		startScale = transform.localScale;

	}

	private void checkDependencies ()
	{
		if (backgroundImage == null) {
			throw new MissingComponentException (gameObject.name + " --Background image not set!");
		}
		if (playerExplosion == null) {
			throw new MissingComponentException (gameObject.name + " --playerExplosion Prefab not set!");
		}
		particleRenderer = GetComponentInChildren<ParticleRenderer> ();
		if (particleRenderer == null) {
			throw new MissingComponentException (gameObject.name + " --no particle renderer found!");
		}
	}
	
	// Update For non-phsycis updates.
	void Update ()
	{
		if (teleportationState != TeleportationState.Done) {
			return;
		}
		Vector3 mouseLocation = CastRayToWorld (Input.mousePosition);
		int direction = followMouse ? 1 : -1;
		Vector3 shipPointTowards = direction * (mouseLocation - transform.position);
		shipRotateTowards = Quaternion.LookRotation (shipPointTowards);
	}

	// Update for physics.
	void FixedUpdate ()
	{
		if (teleportationState != TeleportationState.Done) {
			return;
		}
		if (Input.GetKey (KeyCode.Space) || Input.GetMouseButton (0 /*left*/)) {
			Vector3 forwardXZ = new Vector3 (transform.forward.x, 0, transform.forward.z);
			rb.AddForce (forwardXZ * thrust, ForceMode.Force);
		}


		rb.velocity = Vector3.ClampMagnitude (rb.velocity, topSpeed);

		// Get the plain rotation components.
		Quaternion slerpComp = Quaternion.Slerp (rb.rotation, shipRotateTowards, Time.deltaTime * rotationSpeed);
		// Get the tilt componenets
		// /*not in 2d*/ float tiltComp = enableTilt ? tilt * (slerpComp.eulerAngles.y - previousY) : 0;
		// Act on rb
		rb.rotation = Quaternion.Euler (0f, slerpComp.eulerAngles.y, 0f);
		// Prepare for next update.
		// /*not in 2d*/ previousY = slerpComp.eulerAngles.y;
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
		return point;
	}

	//////////////////
	///// Call-backs
	/////////////////
	public void updateMainSettings (String setting, float value, bool boolean)
	{
		switch (setting) {
		case "Speed":
			speed = value;
			break;
		case "Top Speed":
			topSpeed = value;
			break;
		case "Rotation Speed":
			rotationSpeed = value;
			break;
		case "Follow Mouse":
			followMouse = boolean;
			break;
		case "Thrust":
			thrust = value;
			break;
		}
	}

	public void updateAdvancedSettings (String setting, float value, bool boolean)
	{
		switch (setting) {
		case "Thrust":
			thrust = value;
			break;
		case "Tilt":
			// /*not in 2d*/ enableTilt = boolean;
			break;
		}
	}

	/////////////////////////
	///// Rotation Logic
	////////////////////////

	IEnumerator translate (Vector3 to, float duration, Vector3 endScale)
	{
		float progress = 0;
		//float increment = duration / wormholeAnimationSteps; //The amount of change to apply.

		while (progress <= 0.25) {
			transform.position = Vector3.Lerp (transform.position, to, progress);
			transform.localScale = Vector3.Lerp (transform.localScale, endScale, progress);
			progress += increment;
			//Debug.Log ("Progress: " + progress);
			yield return new WaitForSeconds (0.01F);
		}
		transform.position = to;
		transform.localScale = endScale;
		yield return null;
	}

	IEnumerator _teleport (Vector3 from, Vector3 to)
	{

		// Freeze all inputs
		this.teleportationState = TeleportationState.ToDestination1;

		// Freeze player
		rb.velocity = Vector3.zero;
		rb.angularVelocity = Vector3.zero;

		// Scale info.
		Vector3 wormholeScale = startScale * scaleInWormhole;

		// Compute all timing requirements
		float timeToDestination1 = 1 / 4;
		float timeAtDestination1 = 1 / 4;
		float timeToDestination2 = 1 / 4;
		float timeAtDestination2 = 1 / 4;
		// Go to destination 1.
		this.particleRenderer.enabled = false;
		yield return StartCoroutine (translate (from, timeToDestination1, wormholeScale));
		// this.teleportationState = TeleportationState.AtDestination1;
		// At destination 1. stop thrusters and reduce the scale.
		//yield return StartCoroutine (translate (transform.position, timeAtDestination1, wormholeScale));
		this.teleportationState = TeleportationState.ToDestination2;

		// Go to destination 2.
		yield return StartCoroutine (translate (to, timeToDestination2, transform.localScale));
		this.teleportationState = TeleportationState.AtDestination2;
		// At destination 2. return scale, turn on thrusters, and we're done.
		yield return StartCoroutine (translate (transform.position, timeAtDestination2, startScale));
		this.particleRenderer.enabled = true;
		this.teleportationState = TeleportationState.Done;
		yield return null;
	}

	public void teleport (Vector3 from, Vector3 to)
	{
		StartCoroutine (_teleport (from, to));
	}

}
