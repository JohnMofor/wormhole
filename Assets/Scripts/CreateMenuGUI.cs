using UnityEngine;
using System.Collections;

public class CreateMenuGUI : MonoBehaviour {

	public GUISkin PlaySkin;
	public GUISkin TutorialSkin;
	public GUISkin ExitSkin;
	public GUISkin CreditsSkin;
	public GUISkin QuitSkin;
	public GUISkin Logo;
	public GUISkin Platypus;

	private bool inCredits;

	void OnGUI() {
		if (inCredits) {
			GUIStyle titleStyle = new GUIStyle ();
			titleStyle.fontSize = 30;
			titleStyle.normal.textColor = Color.white;
			GUIStyle textStyle = new GUIStyle();
			textStyle.fontSize = 20;
			textStyle.normal.textColor = Color.white;
			
			GUI.BeginGroup (new Rect (Screen.width / 2 - 350, Screen.height / 2 - 300, 700, 600));
			GUI.Box (new Rect (0, 0, 700, 600), "");
			GUI.Label (new Rect (300, 50, 100, 20), "Credits", titleStyle);
			GUI.Label (new Rect (25, 100, 100, 20), "Producer: Hannah Harris", textStyle);
			GUI.Label (new Rect (25, 150, 100, 20), "Programmers: John Mofor, Kevin Wang, Miguel Rodriguez", textStyle);
			GUI.Label (new Rect (25, 200, 100, 20), "Art & Music: Hannah Harris", textStyle);
			GUI.Label (new Rect (25, 300, 100, 20), "Sounds courtesy of: ", textStyle);
			GUI.Label (new Rect (25, 350, 100, 20), "Images courtesy of: NASA, ESA, and The Hubble Heritage Team", textStyle);
			GUI.Label (new Rect (207, 400, 100, 20), "Images recolored and edited by Hannah Harris", textStyle);
			GUI.Label (new Rect (25, 550, 100, 20), "[copyright stuff] © 2015  All Rights Reserved", textStyle);


			GUI.skin = QuitSkin;
			if (GUI.Button (new Rect(650, 10, 40, 40), "")) { 
				inCredits = false;
			}
			GUI.EndGroup ();
		} else {
			GUI.skin = Logo;
			GUI.Box (new Rect(Screen.width /2 - 250, Screen.height / 2 - 300, 500, 250), "");
			GUI.skin = PlaySkin;
			if (GUI.Button (new Rect (Screen.width / 2 - 75, Screen.height / 2 + 25, 150, 75), "")) {
				Application.LoadLevel ("tutorial2d");
			}
			GUI.skin = ExitSkin;
			if (GUI.Button (new Rect (Screen.width / 2 - 75, Screen.height / 2 + 150, 150, 75), "")) {
				Application.Quit ();
			}
			GUI.skin = CreditsSkin;
			if (GUI.Button (new Rect (40, Screen.height - 60, 80, 30), "")) {
				Debug.Log ("credits");
				inCredits = true;
			}
			GUI.skin = Platypus;
			GUI.Box(new Rect(Screen.width - 600, Screen.height - 600, 500, 500), "");
		}

	}
}
