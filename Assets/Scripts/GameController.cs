using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameController : MonoBehaviour
{
	private int collected;

	public Text collectibleText;
	public Text restartText;
	public Text gameOverText;

	public GUISkin tryAgainSkin;
	public GUISkin exitSkin;

	private bool restart;
	private bool gameOver;
	private bool victory;
	private bool reachedDestination;
	private int score;

	// Use this for initialization
	void Start ()
	{
		collected = 0;
		UpdateCollectibleText ();

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

	void OnGUI() {
		/*if (reachedDestination) { 
			GUIStyle titleStyle = new GUIStyle ();
			titleStyle.fontSize = 30;
			titleStyle.normal.textColor = Color.white;

			GUI.BeginGroup (new Rect (Screen.width / 2 - 175, Screen.height / 2 - 225, 350, 450));
			GUI.Box (new Rect (0, 0, 350, 450), "");
			GUI.Label (new Rect (125, 50, 100, 20), "You need the rest of the collectibles!", titleStyle);
			GUI.EndGroup ();
		} else*/ if (gameOver == true && victory == false) {

			GUIStyle titleStyle = new GUIStyle ();
			titleStyle.fontSize = 30;
			titleStyle.normal.textColor = Color.white;

			GUI.BeginGroup (new Rect (Screen.width / 2 - 175, Screen.height / 2 - 225, 350, 450));
			GUI.Box (new Rect (0, 0, 350, 450), "");
			GUI.Label(new Rect(100, 75, 100, 50), "Game Over!", titleStyle);
			GUI.skin = tryAgainSkin;
			if (GUI.Button (new Rect (15, 150, 320, 40), "")) {
				Application.LoadLevel (Application.loadedLevel);
			}
			GUI.skin = exitSkin;
			if (GUI.Button(new Rect(75, 200, 200, 100), "")) {
				Application.LoadLevel ("startMenu");
			}
			GUI.EndGroup ();
		}
	}

	private void UpdateCollectibleText ()
	{
		collectibleText.text = "Collectibles Remaining: " + (3 - collected);
	}
	
	public void AddCollectible ()
	{
		collected += 1;
		UpdateCollectibleText ();
	}
	
	public void GameOver ()
	{
		gameOverText.text = "Game Over!";
		gameOver = true;
		
	}
	
	public bool ReachedDestination(GameObject player) {
		if (allCollected() == true) {
			gameOverText.text = "Good Job!";
			gameOver = true;
			victory = true;
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

	public bool allCollected() {
		if (collected == 3) {
			return true;
		} else
			return false;
	}
	
}
