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
			GUI.Label (new Rect (25, 150, 100, 20), "Created By: Hannah Harris, John Mofor, Kevin Wang, Miguel Rodriguez", textStyle);
			GUI.Label (new Rect (25, 225, 100, 20), "This game uses sounds from freesound made by the following users:", textStyle);
			GUI.Label (new Rect (75, 250, 100, 20), "sergeeo, fins, Robinhood76", textStyle);
			GUI.Label (new Rect (25, 325, 100, 20), "This game uses music from Purple Planet Royalty Free Music.", textStyle);
			GUI.Label (new Rect (75, 350, 100, 20), "http://www.purple-planet.com", textStyle);
			GUI.Label (new Rect (25, 425, 100, 20), "This game uses images from NASA, ESA, and The Hubble Heritage Team", textStyle);
			GUI.Label (new Rect (75, 450, 100, 20), "(STScI/AURA) Images recolored and edited by Hannah Harris", textStyle);
			GUI.Label (new Rect (25, 550, 100, 20), "© 2015  Attribution-NonCommercial-NoDerivs CC BY-NC-ND", textStyle);

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
			GUI.Box(new Rect(Screen.width - 500, Screen.height - 550, 500, 500), "");
		}

	}
}
