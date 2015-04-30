using UnityEngine;
using System.Collections;

public class PauseMenuController : MonoBehaviour {

	public PlayerController playerController;

	private bool paused;
	private bool inSettings;
	private bool inAdvancedSettings;

	public float defaultTopSpeed;
	public float defaultSpeed;
	public float defaultRotationSpeed;
	public bool defaultFollowMouse;
	public float defaultThrust;
	public bool defaultTilt;
	
	private float topSpeed;
	private float speed;
	private float rotationSpeed;
	private bool followMouse;
	private float thrust;
	private bool tilt;

	public GUISkin pauseSkin;
	public GUISkin resumeSkin;
	public GUISkin settingsSkin;
	public GUISkin quitSkin;
	public GUISkin backSkin;
	public GUISkin advancedSkin;

	void Start() {
		paused = false;
		inSettings = false;
		inAdvancedSettings = false;

		topSpeed = defaultTopSpeed;
		speed = defaultSpeed;
		rotationSpeed = defaultRotationSpeed;
		followMouse = defaultFollowMouse;
		thrust = defaultThrust;
		tilt = defaultTilt;
	}

	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.P)) {
			if (!paused) {
				(GameObject.Find("Player").GetComponent("PlayerController") as MonoBehaviour).enabled = false;
				Time.timeScale = 0;
				paused = true;
			}
			else if (!inSettings) {
				(GameObject.Find("Player").GetComponent("PlayerController") as MonoBehaviour).enabled = true;
				Time.timeScale = 1;
				paused = false;
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
		if (paused && !inSettings && !inAdvancedSettings) {
			GUI.BeginGroup (new Rect (Screen.width / 2 - 175, Screen.height / 2 - 225, 350, 450));
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
			GUI.BeginGroup (new Rect (Screen.width / 2 - 175, Screen.height / 2 - 225, 350, 450));
			GUI.Box (new Rect (0, 0, 350, 450), "");
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
			GUI.Label (new Rect(25, 245, 100, 20), "Follow Mouse", labelStyle);
			bool newFollowMouse = GUI.Toggle(new Rect (250, 250, 50, 50), followMouse, "");
			if (newFollowMouse != followMouse) {
				playerController.updateMainSettings("Follow Mouse", 0.0f, newFollowMouse);
				followMouse = newFollowMouse;
			}
			
			GUI.skin = advancedSkin;
			if (GUI.Button (new Rect (175, 300, 100, 100), "")) {
				inAdvancedSettings = true;
				inSettings = false;
			}
			GUI.skin = backSkin;
			if (GUI.Button (new Rect (75, 300, 100, 100), "")) {
				inSettings = false;
			}
			GUI.skin = quitSkin;
			if (GUI.Button (new Rect(300, 10, 40, 40), "")) { 
				resumeGame ();
			}
			GUI.EndGroup ();

		// In Advanced Settings Page
		} else if (inAdvancedSettings) {
			GUI.BeginGroup (new Rect (Screen.width / 2 - 175, Screen.height / 2 - 225, 350, 450));
			GUI.Box (new Rect (0, 0, 350, 450), "");
			GUI.Label (new Rect (125, 50, 100, 20), "Advanced", titleStyle);
			GUI.Label (new Rect(25, 145, 100, 20), "Thrust", labelStyle);
			thrust = GUI.HorizontalSlider(new Rect (175, 150, 150, 30), thrust, 0.0f, 100.0f);
			if (thrust != defaultThrust) {
				playerController.updateAdvancedSettings("Thrust", thrust, true);
			}
			GUI.Label (new Rect(25, 195, 100, 20), "Tilt", labelStyle);
			bool newTilt = GUI.Toggle(new Rect (250, 200, 50, 50), tilt, "");
			if (newTilt != followMouse) {
				playerController.updateAdvancedSettings("Tilt", 0.0f, newTilt);
				tilt = newTilt;
			}
			GUI.skin = backSkin;
			if (GUI.Button (new Rect (125, 250, 100, 100), "")) {
				inAdvancedSettings = false;
				inSettings = true;
			}
			GUI.skin = quitSkin;
			if (GUI.Button (new Rect(300, 10, 40, 40), "")) { 
				resumeGame ();
			}
			GUI.EndGroup ();

		// In regular gameplay (no pause)
		} else {
			GUI.skin = pauseSkin;
			if (GUI.Button(new Rect(Screen.width - 60, 10, 50, 50),"")) {
				pauseGame ();
			}
		}
	}

	private void resumeGame() {
		(GameObject.Find ("Player").GetComponent ("PlayerController") as MonoBehaviour).enabled = true;
		Time.timeScale = 1;
		inSettings = false;
		inAdvancedSettings = false;
		paused = false;
	}

	private void pauseGame() {
		(GameObject.Find("Player").GetComponent("PlayerController") as MonoBehaviour).enabled = false;
		Time.timeScale = 0;
		paused = true;
	}

	private void quitGame() {
		Application.LoadLevel ("startMenu");
		paused = false;
		Time.timeScale = 1;
	}
}
