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
	public float stability = 0.3f;
	public float stabilitySpeed = 2.0f;
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
	public float turbo = 100F;
	public float hyperSpeedForce = 1000F;
	public float hyperSpeedMaxSpeed = 10000F;
	public float hyperSpeedFOV_inSmooth = 2F;
	public float hyperSpeedFOV_outSmooth = 0.75F;
	public float hyperSpeedEngineParticleSize = 0.005f;
	public float hyperSpeedFOVMax = 100F;
	public float hyperSpeedFOVExit = 0.5F;
	public bool invertX = false;
	public bool invertY = false;
	public bool useMouse = true;
	public bool useForcesForRotation = false;

	//public GameObject shot;
	public Boundary boundary;
	public Transform shotSpawn;
	public AudioClip thrusterSFX;
	public AudioClip hyperSpeedSFX;
	public GameObject playerExplosion;

	//public GameObject engineObject;
	// Private
	private Rigidbody rb;
	private new AudioSource audio;
	public ParticleRenderer engine;
	private bool trackMouse = false;
	private bool inHyperSpeedMode = false;
	private float cameraBaseFOV = 34F;
	private float engineParticleBaseSize = 0.25F;
	private Vector3 initialMousePosition;
	
	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
		audio = GetComponent<AudioSource> ();
		cameraBaseFOV = Camera.main.fieldOfView;
		engineParticleBaseSize = engine.maxParticleSize;
		initialMousePosition = Input.mousePosition;
		//engine = engineObject.GetComponent<EllipsoidParticleEmitter> ();
	}

	void Update ()
	{
		// Just in case.
		if (!inHyperSpeedMode) {
			if (engine.maxParticleSize == hyperSpeedEngineParticleSize) {
				engine.maxParticleSize = engineParticleBaseSize;
			}
		}

		// Start Tracking Mouse?
		if (Input.mousePosition != initialMousePosition) {
			trackMouse = true;
		}
	}

	private void FixedUpdateHyperSpeed ()
	{
		// Player asking for more hyperspeed.
		if (Input.GetKey (KeyCode.H)) {
			rb.AddForce (transform.forward * hyperSpeedForce);
			Camera.main.fieldOfView = Mathf.Lerp (Camera.main.fieldOfView, hyperSpeedFOVMax, Time.deltaTime * hyperSpeedFOV_outSmooth);
		} else {
			Camera.main.fieldOfView = Mathf.Lerp (Camera.main.fieldOfView, cameraBaseFOV, Time.deltaTime * hyperSpeedFOV_inSmooth);
		}

		// Limit speed and Camera FOV
		// Impose Physical Limits.
		rb.velocity = Vector3.ClampMagnitude (rb.velocity, hyperSpeedMaxSpeed);

		// Set Camera FOV & Play sfx		
		Camera.main.fieldOfView = Mathf.Clamp (Camera.main.fieldOfView, cameraBaseFOV, hyperSpeedFOVMax);
		if (Mathf.Abs (Camera.main.fieldOfView - cameraBaseFOV) <= hyperSpeedFOVExit) {
			Camera.main.fieldOfView = cameraBaseFOV;
			inHyperSpeedMode = false;
			engine.maxParticleSize = engineParticleBaseSize;
			audio.Stop ();
			audio.pitch = 1F;
			audio.PlayOneShot (thrusterSFX, 4F);
			//audio.pitch = 1F;
		} else {
			// Adjust corresponding sound
			audio.PlayOneShot (hyperSpeedSFX, 0.5F);
			audio.pitch = 2F *  (Camera.main.fieldOfView / hyperSpeedFOVMax);
		}
		return;
	}

	private void FixedUpdateNonHyperSpeed ()
	{
		
		float horizontalForce = 0f;
		float verticalForce = 0f;
		
		// Look inversions.
		int xDir = invertX ? -1 : 1;
		int yDir = invertY ? -1 : 1;
		
		// Look.
		if (useMouse && trackMouse) {
			
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
			audio.PlayOneShot (thrusterSFX, 1F);
		}
		// Detect SpeedBoost Request.
		if (Input.GetKeyDown (KeyCode.E)) {
			rb.AddForce (transform.forward * thrust * turbo, ForceMode.Force);
			audio.PlayOneShot (thrusterSFX, 1F);
		}
		// Detect breaks request.
		if (Input.GetKey (KeyCode.B)) {
			rb.AddForce (-transform.forward * thrust, ForceMode.Force);
			audio.PlayOneShot (thrusterSFX, 1F);
		}
		// Impose Physical Limits.
		rb.velocity = Vector3.ClampMagnitude (rb.velocity, topSpeed);
		rb.maxAngularVelocity = topRotationSpeed;
	}
	
	// Update for physics.
	void FixedUpdate ()
	{
		if (Input.GetKeyDown (KeyCode.H)) {
			if (!inHyperSpeedMode) {
				engine.maxParticleSize = hyperSpeedEngineParticleSize;
				inHyperSpeedMode = true;
			}
		}


		// In hyperspeed, we do no accept any inputs.
		if (inHyperSpeedMode) {
			FixedUpdateHyperSpeed ();
		} else {
			FixedUpdateNonHyperSpeed ();
		}

		// Stabilize, regardless of hyperspeed or not.
		Vector3 predictedUp = Quaternion.AngleAxis (
			rb.angularVelocity.magnitude * Mathf.Rad2Deg * stability / stabilitySpeed,
			rb.angularVelocity
		) * transform.up;
		
		Vector3 torqueVector = Vector3.Cross (predictedUp, Vector3.up);
		torqueVector = Vector3.Project (torqueVector, transform.forward);
		rb.AddTorque (torqueVector * stabilitySpeed * stabilitySpeed);
	}
	
	public void setHyperSpeedMode (bool mode)
	{
		inHyperSpeedMode = mode;
	}
	
}
