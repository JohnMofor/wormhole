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
	public Canvas pauseCanvas;
	public Canvas pauseOptionsCanvas;
	public Canvas pauseAdvancedOptionsCanvas;
	public GameObject pausePanel;
	public GameObject pauseOptionsPanel;
	public GameObject pauseAdvancedOptionsPanel;
	private bool restart;
	private bool gameOver;
	private int score;
	private bool paused;
	private bool inSettings;

	public GUISkin pauseSkin;
	public GUISkin resumeSkin;
	public GUISkin advancedSkin;

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

		collected = 0;

		restartText.text = "";
		gameOverText.text = "";
		gameOver = false;
		restart = false;
		paused = false;
		inSettings = false;
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.P)) {
			if (!paused) {
				(GameObject.Find("Player").GetComponent("PlayerController") as MonoBehaviour).enabled = false;
				pauseCanvas.enabled = true;
				Time.timeScale = 0;
				paused = true;
			}
			else if (!inSettings) {
				(GameObject.Find("Player").GetComponent("PlayerController") as MonoBehaviour).enabled = true;
				pauseCanvas.enabled = false;
				Time.timeScale = 1;
				paused = false;
			}
		}
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

	void OnGUI() {
		if (paused) {
			GUI.BeginGroup(new Rect(Screen.width/2-150,Screen.height/2-150,350,450));
			GUI.Box(new Rect(0,0,300,300),"");
			GUI.Label (new Rect (0, 50, 100, 20), "Settings");
			GUI.skin = resumeSkin;
			GUI.Button(new Rect(0,25,100,20),"Resume");
			GUI.EndGroup ();
		} else {
			GUI.skin = pauseSkin;
			if (GUI.Button(new Rect(Screen.width, 0, 20, 20),"")) {
				Time.timeScale = 0;
				paused = true;
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

	public void PauseOnClick() {
//		RectTransform pauseTransform = (RectTransform)pausePanel.transform;
//		RectTransform canvasTransform = (RectTransform)pauseCanvas.transform;
//		Debug.Log (pauseTransform.rect.position);
//		Debug.Log (pauseTransform.rect.size);
//		Debug.Log (Input.mousePosition);
//		Debug.Log ("Canvas" + canvasTransform.rect.Contains (Input.mousePosition));
//		if (pauseTransform.rect.Contains(Input.mousePosition)) {
//			Debug.Log (true);
//			ResumeGame();
//		}
//		else {
//			Debug.Log (false);
//		}
//		if (pauseCanvas.enabled) {
//			
//		} else if (pauseOptionsCanvas.enabled) {
//
//		} else {
//
//		}
	}
}
