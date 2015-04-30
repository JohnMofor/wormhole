using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
	private int collected;
	public int totalNumberOfCollectibles = 3;
	public Text collectibleText;
	public Text restartText;
	public Text gameOverText;

	private bool restart;
	private bool gameOver;
	private bool victory;
	private bool reachedDestination;
	private int score;

	public GUISkin tryAgainSkin;
	public GUISkin exitSkin;
	public GUISkin restartSkin;
	public GUISkin backToMenuSkin;
	public GUISkin nextLevelSkin;
	public GUISkin greatJobSkin;

	// Use this for initialization
	void Start ()
	{
		collected = 0;

		restartText.text = "";
		gameOverText.text = "";
		gameOver = false;
		victory = false;
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

	void OnGUI ()
	{
		/*if (reachedDestination) { 
			GUIStyle titleStyle = new GUIStyle ();
			titleStyle.fontSize = 30;
			titleStyle.normal.textColor = Color.white;

			GUI.BeginGroup (new Rect (Screen.width / 2 - 175, Screen.height / 2 - 225, 350, 450));
			GUI.Box (new Rect (0, 0, 350, 450), "");
			GUI.Label (new Rect (125, 50, 100, 20), "You need the rest of the collectibles!", titleStyle);
			GUI.EndGroup ();
		} else*/
		if (gameOver == true && victory == false) {
			GUIStyle titleStyle = new GUIStyle ();
			titleStyle.fontSize = 30;
			titleStyle.normal.textColor = Color.white;

			GUI.BeginGroup (new Rect (Screen.width / 2 - 175, Screen.height / 2 - 225, 350, 450));
			GUI.Box (new Rect (0, 0, 350, 450), "");
			GUI.Label (new Rect (100, 75, 100, 50), "Game Over!", titleStyle);
			GUI.skin = tryAgainSkin;
			if (GUI.Button (new Rect (15, 150, 320, 40), "")) {
				Application.LoadLevel (Application.loadedLevel);
			}
			GUI.skin = exitSkin;
			if (GUI.Button (new Rect (75, 200, 200, 100), "")) {
				Application.LoadLevel ("startMenu");
			}
			GUI.EndGroup ();
		} else if (gameOver == true && victory == true) {
			GUI.BeginGroup (new Rect (Screen.width / 2 - 175, Screen.height / 2 - 125, 350, 250));
			GUI.Box (new Rect (0, 0, 350, 250), "");
			GUI.skin = greatJobSkin;
			GUI.Button (new Rect (75, 25, 200, 50), "");
			GUI.skin = backToMenuSkin;
			if (GUI.Button (new Rect (25, 100, 100, 100), "")) {
				Application.LoadLevel ("startMenu");
			}
			GUI.skin = restartSkin;
			if (GUI.Button (new Rect (125, 100, 100, 100), "")) {
				Application.LoadLevel (Application.loadedLevel);
			}
			GUI.skin = nextLevelSkin;
			if (GUI.Button (new Rect(225, 100, 100, 100), "")) {
				Application.LoadLevel (Application.loadedLevel);
			}
			GUI.EndGroup ();
		}
	}

	public int collectiblesRemaining() {
		return totalNumberOfCollectibles - collected;
	}

	public void AddCollectible ()
	{
		collected += 1;
	}
	
	public void GameOver ()
	{
		gameOverText.text = "Game Over!";
		gameOver = true;
		
	}
	
	public bool ReachedDestination (GameObject player)
	{
		if (allCollected () == true) {
			gameOverText.text = "Good Job!";
			gameOver = true;
			victory = true;

			string currentLevel = Application.loadedLevelName;
			int nextLevelNumber = LevelSelectionManager.NextLevelOf(currentLevel);
			if (nextLevelNumber !=-1)
			{
				LevelSelectionManager.UnlockNextLevel(nextLevelNumber);
			}


			return true;
		} /*else {
			float currX = player.transform.position.x;
			float currY = player.transform.position.y;
			Debug.Log( Mathf.Abs(player.transform.position.x - currX));
			while (Mathf.Abs(player.transform.position.x - currX) < 10 || Mathf.Abs(player.transform.position.y - currY) < 10) {
				reachedDestination = true;
			}
			return false;
		}*/
		return false;
	}

	public bool allCollected ()
	{
		return collected == totalNumberOfCollectibles;
	}	
}
