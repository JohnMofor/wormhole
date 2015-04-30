using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource), typeof(Rigidbody))]
public class Collectible : MonoBehaviour
{
	public GameController gameController;
	public AudioClip collectionSound;

	public float turn = 5f;

	void Start() {
		GetComponent<Rigidbody>().angularVelocity = new Vector3(0f, turn, 0f);
	}

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
