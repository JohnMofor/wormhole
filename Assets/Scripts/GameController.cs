using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{


	public GameObject hazard;
	public Vector3 spawnValues;
	public int hazardCount;
	public float startWait;
	public float spawnWait;
	public float waveWait;
	public Text scoreText;
	public Text restartText;
	public Text gameOverText;
	private bool restart;
	private bool gameOver;
	private int score;
	
	void UpdateScore ()
	{
		scoreText.text = "Score: " + score.ToString ();
	}

	public void AddScore (int newScoreValue)
	{
		score += newScoreValue;
		UpdateScore ();
	}

	public void GameOver ()
	{
		gameOverText.text = "Game Over!";
		gameOver = true;

	}

	public void ReachedDestination() {
		gameOverText.text = "Congratulations!";
		gameOver = true;
	}
	
	// Use this for initialization
	void Start ()
	{
		score = 0;
		UpdateScore ();

		restartText.text = "";
		gameOverText.text = "";
		gameOver = false;
		restart = false;

		//StartCoroutine (SpawnWaves());
	}

//
//	IEnumerator SpawnWaves() {
//		yield return new WaitForSeconds(startWait);
//		while(true) {
//			for (int i = 0; i < hazardCount; i++) {
//				Vector3 spawnPosition = new Vector3(
//					Random.Range(-spawnValues.x, spawnValues.x), 
//					spawnValues.y, 
//					spawnValues.z);
//				Quaternion spawnRotation = Quaternion.identity;
//				Instantiate (hazard, spawnPosition, spawnRotation);
//				yield return new WaitForSeconds(spawnWait);
//			}
//			yield return new WaitForSeconds(waveWait);
//
//			if (gameOver) {
//				restartText.text = "Press 'r' to Restart";
//				restart = true;
//				break;
//			}
//		}
//	}

	// Update is called once per frame
	void Update ()
	{
		if (gameOver) {
			restartText.text = "Press 'r' to Restart";
			restart = true;
		}
		if (restart) {
			if (Input.GetKeyDown (KeyCode.R)) {
				Application.LoadLevel (Application.loadedLevel);
			}
		}
	
	}
}
