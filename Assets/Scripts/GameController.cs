using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
	private int collected;
	public int totalNumberOfCollectibles;
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
		
		gameOver = false;
		victory = false;
		restart = false;
	}

	// Update is called once per frame
	void Update ()
	{
		if (gameOver) {
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
			//GUI.Label (new Rect (100, 75, 100, 50), "Game Over!", titleStyle);
			GUI.skin = tryAgainSkin;
			if (GUI.Button (new Rect (15, 105, 320, 100), "")) {
				Application.LoadLevel (Application.loadedLevel);
			}
			GUI.skin = exitSkin;
			if (GUI.Button (new Rect (75, 225, 200, 100), "")) {
				Application.LoadLevel ("startMenu");
			}
			GUI.EndGroup ();
		} else if (gameOver == true && victory == true) {
			GUI.BeginGroup (new Rect (Screen.width / 2 - 175, Screen.height / 2 - 125, 350, 350));
			//GUI.backgroundColor = new Color(0,0,0, 0.5f);
			GUI.Box (new Rect (0, 0, 350, 350), "");
			GUI.skin = greatJobSkin;
			GUI.Button (new Rect (35, 50, 275, 150), "");
			GUI.skin = backToMenuSkin;
			if (GUI.Button (new Rect (25, 225, 100, 100), "")) {
				Application.LoadLevel ("startMenu");
			}
			GUI.skin = restartSkin;
			if (GUI.Button (new Rect (125, 225, 100, 100), "")) {
				Application.LoadLevel (Application.loadedLevel);
			}
			GUI.skin = nextLevelSkin;
			if (GUI.Button (new Rect (225, 225, 100, 100), "")) {
				if (Application.loadedLevel < 6) {
					Application.LoadLevel (Application.loadedLevel + 1);
				} else {
					Application.LoadLevel (0);
				}
			}
			GUI.EndGroup ();
		}
	}

	public int collectiblesRemaining ()
	{
		return totalNumberOfCollectibles - collected;
	}

	public void AddCollectible ()
	{
		collected += 1;
	}
	
	public void GameOver ()
	{
		gameOver = true;
		
	}
	
	public bool ReachedDestination (GameObject player)
	{
		if (allCollected () == true) {
			gameOver = true;
			victory = true;

			string currentLevel = Application.loadedLevelName;
			int nextLevelNumber = LevelSelectionManager.NextLevelOf (currentLevel);
			if (nextLevelNumber != -1) {
				LevelSelectionManager.UnlockNextLevel (nextLevelNumber);
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
