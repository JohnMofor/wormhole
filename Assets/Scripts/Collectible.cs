using UnityEngine;
using System.Collections;

public class Collectible : MonoBehaviour
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
			gameController.collectCollectible(gameObject);
			Destroy (gameObject);
			gameController.AddScore(10);
		}
	}
}
