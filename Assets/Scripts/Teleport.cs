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
	private bool active = true;

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
		this.active = false;
	}

	void OnTriggerEnter (Collider other)
	{
		if (!other.gameObject.tag.Equals ("Player") || !this.active) {
			return;
		}
		teleportDestination.deactivate ();
		playerController.teleport(transform.position, destination, teleportationDuration);
	}

	void OnTriggerExit (Collider other)
	{
		if (!this.active && other.gameObject.tag.Equals ("Player")) {
			StartCoroutine (reactivate ());
		}
	}

	IEnumerator reactivate ()
	{
		if (!this.active) {
			yield return null;
		}
		yield return new WaitForSeconds (waitTimeToReactivate);
		this.active = true;
	}
}
