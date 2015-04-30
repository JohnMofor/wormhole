using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
	private int collected;
	public Vector3 spawnValues;
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
		gameOverText.text = "Good Job!";
		gameOver = true;
	}
	
	// Use this for initialization
	void Start ()
	{
		score = 0;
		UpdateScore ();

		collected = 0;

		restartText.text = "";
		gameOverText.text = "";
		gameOver = false;
		restart = false;
	}

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

	public void collectCollectible(GameObject collectible) {
		collected += 1;
	}

	public bool allCollected() {
		if (collected == 3) {
			return true;
		} else
			return false;
	}

	/*
	public void ResumeGame() {
		(GameObject.Find("Player").GetComponent("PlayerController") as MonoBehaviour).enabled = true;
		pauseCanvas.enabled = false;
		pauseOptionsCanvas.enabled = false;
		pauseAdvancedOptionsCanvas.enabled = false;
		Time.timeScale = 1;
	}

	public void PauseOptions() {
		pauseCanvas.enabled = !pauseCanvas.enabled;
		pauseOptionsCanvas.enabled = !pauseOptionsCanvas.enabled;
		inSettings = !inSettings;
	}

	public void PauseAdvancedOptions() {
		pauseOptionsCanvas.enabled = !pauseOptionsCanvas.enabled;
		pauseAdvancedOptionsCanvas.enabled = !pauseAdvancedOptionsCanvas.enabled;
	}

	public void QuitGame() {
		Application.LoadLevel ("startMenu");
		pauseCanvas.enabled = false;
		paused = false;
		Time.timeScale = 1;
	}
	*/
}
