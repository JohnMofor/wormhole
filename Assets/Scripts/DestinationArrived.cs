using UnityEngine;
using System.Collections;

public class DestinationArrived : MonoBehaviour
{
	public GameController gameController;
	
	void OnTriggerEnter (Collider other)
	{
		if (other.transform.root == transform.root) {
			return; /* Don't kill yourself */
		}
		if (other.tag == "Boundary") {
			return;
		}
		
		
		if (other.tag == "Player") {
			//Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
			gameController.ReachedDestination ();
		} 
		Destroy (other.gameObject);
	}
}
