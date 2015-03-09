using UnityEngine;
using System.Collections;

public class DestroyByContact : MonoBehaviour {

	public GameObject explosion;
	public GameObject playerExplosion;

	public int scoreValue;
	private GameController gameController;

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Boundary") {
			return;
		}


		if (other.tag == "Player") {
			Instantiate (playerExplosion, other.transform.position, other.transform.rotation);
			gameController.GameOver();
		} else {
			Instantiate (explosion, transform.position, transform.rotation);
			gameController.AddScore(scoreValue);

		}

		Destroy(other.gameObject);
		Destroy (gameObject);
	}

	// Use this for initialization
	void Start () {
		GameObject gameControllerObject = GameObject.FindWithTag("GameController");
		if (gameControllerObject != null){
			gameController = gameControllerObject.GetComponent<GameController>();
		}
		if (gameController == null) {
			Debug.Log("Cannot find 'GameController' script");
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
