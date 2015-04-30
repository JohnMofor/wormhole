using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class Collectible : MonoBehaviour
{
	public GameController gameController;
	public AudioClip collectionSound;

	void OnTriggerEnter (Collider other)
	{
		if (other.transform.root == transform.root) {
			return; /* Don't kill yourself */
		}
		if (other.tag == "Boundary") {
			return;
		}
		if (other.tag == "Player") {
			gameController.AddCollectible();
			AudioSource.PlayClipAtPoint(collectionSound, new Vector3(5, 1, 2), 0.15f);
			Destroy (gameObject);
		}
	}
}
