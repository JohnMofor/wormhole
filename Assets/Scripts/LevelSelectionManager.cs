using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class LevelSelectionManager : MonoBehaviour {

	public int numberOfLevels;

	public GUISkin binarySkin;
	public GUISkin blackholeSkin;
	public GUISkin solarSystemSkin;
	public GUISkin tutorialSkin;
	public GUISkin wormholeSkin;

	public GUISkin backSkin;

	private static Dictionary<string,int> nextLevelOf = new Dictionary<string,int >()
	{
		{"tutorial2d",2},
		{"level_solarSystem",4},
		{"level_wormholes",5},
		{"level_blackholes",-1}
	};

	void Awake (){

		/*int[] unlockedLevels = PlayerPrefsX.GetIntArray ("unlockedLevels",1,1);

		for (int i = 1; i <= numberOfLevels; i++) {


			string buttonName = string.Format("/Canvas/Level{0}Button",i);
			string lockedButtonName = buttonName+"Locked";

			GameObject levelButton = GameObject.Find(buttonName);
			GameObject lockedLevelButton = GameObject.Find(lockedButtonName);

			if (Array.IndexOf(unlockedLevels,i)!=-1){
				levelButton.gameObject.SetActive(true);
				lockedLevelButton.gameObject.SetActive(false);
			}
			else
			{
				levelButton.gameObject.SetActive(false);
				lockedLevelButton.gameObject.SetActive(true);
			}

		}*/
	}

	void OnGUI() {
		GUIStyle titleStyle = new GUIStyle ();
		titleStyle.fontSize = 60;
		titleStyle.normal.textColor = Color.white;

		GUI.Label (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 300, 400, 100), "Level Selection", titleStyle);

		GUI.skin = tutorialSkin;
		if (GUI.Button (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 200, 400, 100), "")) {
			Application.LoadLevel(1);
		}
		GUI.skin = solarSystemSkin;
		if (GUI.Button (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 100, 400, 100), "")) {
			Application.LoadLevel(3);
		}
		GUI.skin = binarySkin;
		if (GUI.Button (new Rect (Screen.width / 2 - 200, Screen.height / 2 - 0, 400, 100), "")) {
			Application.LoadLevel(2);
		}
		GUI.skin = wormholeSkin;
		if (GUI.Button (new Rect (Screen.width / 2 - 200, Screen.height / 2 + 100, 400, 100), "")) {
			Application.LoadLevel(2);
		}
		GUI.skin = blackholeSkin;
		if (GUI.Button (new Rect (Screen.width / 2 - 200, Screen.height / 2 + 200, 400, 100), "")) {
			Application.LoadLevel(4);
		}

		GUI.skin = backSkin;
		if (GUI.Button (new Rect (10, 10, 75, 75), "")) {
			Application.LoadLevel (0);
		}
	}

	public static void UnlockNextLevel(int nextLevelNumber)
	{
		int[] unlockedLevels = PlayerPrefsX.GetIntArray ("unlockedLevels",1,1);

		Array.Resize<int> (ref unlockedLevels,unlockedLevels.Length+1);
		unlockedLevels [unlockedLevels.Length-1] = nextLevelNumber;
		PlayerPrefsX.SetIntArray ("unlockedLevels",unlockedLevels);
	}

	public static void LockAllLevels()
	{
		PlayerPrefsX.SetIntArray ("unlockedLevels",new int[]{1});
	}

	public void LoadScene(string level)
	{
		Application.LoadLevel (level);
	}

	public int GetNextLevel(string currentLevel) {
		return 0;
	}

	public static int NextLevelOf(string level)
	{
		return nextLevelOf [level];
	}
}
