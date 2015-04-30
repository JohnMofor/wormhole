using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PauseMenuController : MonoBehaviour {

	public PlayerController playerController;
	public GameController gameController;

	private bool paused;
	private bool inSettings;
	private bool inAdvancedSettings;

	public float defaultTopSpeed;
	public float defaultSpeed;
	public float defaultRotationSpeed;
	public bool defaultFollowMouse;
	public float defaultThrust;

	private float topSpeed;
	private float speed;
	private float rotationSpeed;
	private bool followMouse;
	private float thrust;

	public GUISkin pauseSkin;
	public GUISkin resumeSkin;
	public GUISkin settingsSkin;
	public GUISkin quitSkin;
	public GUISkin backSkin;
	public GUISkin advancedSkin;
	public GUISkin starsRemainingSkin;
	public GUISkin oneStar;
	public GUISkin twoStar;
	public GUISkin threeStar;
	public GUISkin fourStar;
	public GUISkin fiveStar;

	private Dictionary<int, GUISkin> stars;

	void Start() {
		paused = false;
		inSettings = false;

		topSpeed = defaultTopSpeed;
		speed = defaultSpeed;
		rotationSpeed = defaultRotationSpeed;
		followMouse = defaultFollowMouse;
		thrust = defaultThrust;

		stars = new Dictionary<int, GUISkin>();
		stars.Add (0, quitSkin);
		stars.Add(1, oneStar);
		stars.Add(2, twoStar);
		stars.Add(3, threeStar);
		stars.Add(4, fourStar);
		stars.Add(5, oneStar);
		stars.Add(6, twoStar);
		stars.Add(7, threeStar);
		stars.Add(8, fourStar);
		stars.Add(9, fourStar);
	}

	void Update ()
	{
		if (playerController != null) {
			if (Input.GetKeyDown (KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)) {
				if (!paused) {
					(playerController.GetComponent ("PlayerController") as MonoBehaviour).enabled = false;
					Time.timeScale = 0;
					inSettings = false;
					paused = true;
				} else {
					(playerController.GetComponent ("PlayerController") as MonoBehaviour).enabled = true;
					Time.timeScale = 1;
					inSettings = false;
					paused = false;
				}
			}
		}
	}

	void OnGUI() {

		// Setting up styles for labels and titles
		GUIStyle titleStyle = new GUIStyle ();
		titleStyle.fontSize = 30;
		titleStyle.normal.textColor = Color.white;
		GUIStyle labelStyle = new GUIStyle ();
		labelStyle.fontSize = 20;
		labelStyle.normal.textColor = Color.white;

		// In core menu page
		if (paused && !inSettings) {
			GUI.BeginGroup (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 275, 400, 550));
			GUI.Box (new Rect (0, 0, 350, 450), "");
			GUI.Label (new Rect (125, 50, 100, 20), "Pause", titleStyle);
			GUI.skin = resumeSkin;
			if (GUI.Button (new Rect (125, 100, 100, 100), "")) {
				resumeGame ();
			}
			GUI.skin = settingsSkin;
			if (GUI.Button (new Rect (125, 200, 100, 100), "")) {
				inSettings = true;
			}
			GUI.skin = quitSkin;
			if (GUI.Button (new Rect (125, 300, 100, 100), "")) {
				quitGame();
			}
			if (GUI.Button (new Rect(300, 10, 40, 40), "")) { 
				resumeGame ();
			}
			GUI.EndGroup ();

		// In main settings page
		} else if (inSettings) {
			GUI.BeginGroup (new Rect (Screen.width / 2 - 175, Screen.height / 2 - 250, 350, 500));
			GUI.Box (new Rect (0, 0, 350, 500), "");
			GUI.Label (new Rect (125, 35, 100, 20), "Settings", titleStyle);
			GUI.Label (new Rect(25, 95, 100, 20), "Top Speed", labelStyle);
			topSpeed = GUI.HorizontalSlider(new Rect (175, 100, 150, 30), topSpeed, 0.0f, 100.0f);
			if (topSpeed != defaultTopSpeed) {
				playerController.updateMainSettings("Top Speed", topSpeed, true);
			}
			GUI.Label (new Rect(25, 145, 100, 20), "Speed", labelStyle);
			speed = GUI.HorizontalSlider(new Rect (175, 150, 150, 30), speed, 0.0f, 2.0f);
			if (speed != defaultSpeed) {
				playerController.updateMainSettings("Speed", speed, true);
			}
			GUI.Label (new Rect(25, 195, 100, 20), "Rotation Speed", labelStyle);
			rotationSpeed = GUI.HorizontalSlider(new Rect (175, 200, 150, 30), rotationSpeed, 0.0f, 10.0f);
			if (rotationSpeed != defaultRotationSpeed) {
				playerController.updateMainSettings("Rotation Speed", rotationSpeed, true);
			}
			GUI.Label (new Rect(25, 245, 100, 20), "Thrust", labelStyle);
			thrust = GUI.HorizontalSlider(new Rect (175, 250, 150, 30), thrust, 0.0f, 100.0f);
			if (thrust != defaultThrust) {
				playerController.updateMainSettings("Thrust", thrust, true);
			}
			GUI.Label (new Rect(25, 295, 100, 20), "Follow Mouse", labelStyle);
			bool newFollowMouse = GUI.Toggle(new Rect (250, 300, 50, 50), followMouse, "");
			if (newFollowMouse != followMouse) {
				playerController.updateMainSettings("Follow Mouse", 0.0f, newFollowMouse);
				followMouse = newFollowMouse;
			}

			GUI.skin = settingsSkin;
			if (GUI.Button(new Rect(145, 325, 60, 60), "")) {
				resetToDefaults();
			}

			GUI.skin = backSkin;
			if (GUI.Button (new Rect (125, 387.5f, 100, 100), "")) {
				inSettings = false;
			}
			GUI.skin = quitSkin;
			if (GUI.Button (new Rect(300, 10, 40, 40), "")) { 
				resumeGame ();
			}
			GUI.EndGroup ();

		// In regular gameplay (no pause)
		} else if (playerController) {
			GUI.skin = pauseSkin;
			if (GUI.Button(new Rect(Screen.width - 60, 10, 50, 50),"")) {
				pauseGame ();
			}
			GUI.skin = starsRemainingSkin;
			GUI.Button(new Rect (20, 10, 100, 50), "");
			GUI.skin = updateStarCount();
			GUI.Button (new Rect(110, 10, 50, 50), "");
		}
	}

	private GUISkin updateStarCount() {
		GUISkin star = stars[gameController.collectiblesRemaining()];
		return star;
	}

	private void resetToDefaults() {
		topSpeed = defaultTopSpeed;
		speed = defaultSpeed;
		rotationSpeed = defaultRotationSpeed;
		followMouse = defaultFollowMouse;
		thrust = defaultThrust;
	}

	private void resumeGame() {
		(playerController.GetComponent ("PlayerController") as MonoBehaviour).enabled = true;
		Time.timeScale = 1;
		inSettings = false;
		paused = false;
	}

	private void pauseGame() {
		(playerController.GetComponent("PlayerController") as MonoBehaviour).enabled = false;
		Time.timeScale = 0;
		paused = true;
	}

	private void quitGame() {
		Application.LoadLevel ("startMenu");
		paused = false;
		Time.timeScale = 1;
	}
}