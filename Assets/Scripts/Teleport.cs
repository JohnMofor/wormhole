using UnityEngine;
using System.Collections;

public class Teleport : MonoBehaviour
{
	public float waitTimeToReactivate = 3f;
	public float teleportationDuration = 2f;
	public GameObject exitWormhole;
	public PlayerController playerController;
	private Teleport teleportDestination;
	private Vector3 destination;
	private bool nextExitIsTeleport = true;
	private bool nextEnterIsTeleport = false;

	// Use this for initialization
	void Start ()
	{
		if (playerController == null) {
			throw new MissingComponentException ("PlayerController missing");
		}
		destination = exitWormhole.GetComponent<Transform> ().position;
		teleportDestination = exitWormhole.GetComponent<Teleport> ();
		if (teleportDestination == null) {
			throw new MissingComponentException ("Exit Wormhole doesn't have Teleport component");
		}
	}
	
	public void deactivate ()
	{
		this.nextEnterIsTeleport = true;
	}

	void OnTriggerEnter (Collider other)
	{
		if (!other.gameObject.tag.Equals ("Player")) {
			return;
		}

		if (this.nextEnterIsTeleport) {
			nextEnterIsTeleport = false;
			return;
		}


		teleportDestination.deactivate ();
		this.nextExitIsTeleport = true;
		playerController.teleport (transform.position, destination, teleportationDuration);
	}

	void OnTriggerExit (Collider other)
	{

		if (!other.gameObject.tag.Equals ("Player")) {
			return;
		}

		if (this.nextExitIsTeleport) {
			nextExitIsTeleport = false;
			return;
		}
	}
}
