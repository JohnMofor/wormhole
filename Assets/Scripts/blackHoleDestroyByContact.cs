using UnityEngine;
using System.Collections;

public class blackHoleDestroyByContact : MonoBehaviour
{

	public GameObject explosion;
	public GameObject playerExplosion;
	private GameController gameController;

	void OnTriggerEnter (Collider other)
	{
		if (other.transform.root == transform.root) {
			return; /* Don't kill yourself */
		}
		if (other.tag == "Boundary") {
			return;
		}
		
		
		if (other.tag == "Player") {
			Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver ();
		} else {
			Instantiate (explosion, transform.position, transform.rotation);
			//gameController.AddScore(scoreValue);
			
		}
		Destroy (other.gameObject);
	}

	// Use this for initialization
	void Start ()
	{
		gameController = FindObjectOfType<GameController> ();
		if (gameController == null) {
			throw new MissingComponentException ("Could not find game controller");
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
}
