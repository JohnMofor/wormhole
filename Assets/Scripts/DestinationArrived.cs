using UnityEngine;
using System.Collections;

public class DestinationArrived : MonoBehaviour
{
	
	public GameObject explosion;
	public GameObject playerExplosion;
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
	
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		
	}
}
