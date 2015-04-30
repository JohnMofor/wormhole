using UnityEngine;
using System.Collections;

public class CreateMenuGUI : MonoBehaviour {

	public GUISkin PlaySkin;
	public GUISkin TutorialSkin;
	public GUISkin ExitSkin;
	
	void OnGUI() {
		GUI.skin = PlaySkin;
		if (GUI.Button (new Rect (Screen.width / 2 - 75, Screen.height / 2 - 100, 150, 75), "")) {
			Application.LoadLevel("tutorial2d");
		}
		GUI.skin = TutorialSkin;
		if (GUI.Button (new Rect (Screen.width / 2 - 75, Screen.height / 2, 150, 75), "")) {
			Application.LoadLevel ("tutorial2d");
		}
		GUI.skin = ExitSkin;
		if (GUI.Button (new Rect (Screen.width / 2 - 75, Screen.height / 2 + 100, 150, 75), "")) {
			Debug.Log("quitting");
			Application.Quit ();
		}
	}
}
