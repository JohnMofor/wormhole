using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

public class LevelSelectionManager : MonoBehaviour {

	public int numberOfLevels;

	void Awake (){

		LockAllLevels ();

		int[] unlockedLevels = PlayerPrefsX.GetIntArray ("unlockedLevels",1,1);
		Debug.Log (unlockedLevels[0]);

		for (int i = 1; i <= numberOfLevels; i++) {


			string buttonName = string.Format("/Canvas/Level{0}Button",i);
			string lockedButtonName = buttonName+"Locked";
			Debug.Log(buttonName);
			Debug.Log (lockedButtonName);

			GameObject levelButton = GameObject.Find(buttonName);
			GameObject lockedLevelButton = GameObject.Find(lockedButtonName);

			Debug.Log (levelButton);
			Debug.Log (lockedLevelButton);

			if (Array.IndexOf(unlockedLevels,i)!=-1){
				levelButton.gameObject.SetActive(true);
				lockedLevelButton.gameObject.SetActive(false);
			}
			else
			{
				levelButton.gameObject.SetActive(false);
				lockedLevelButton.gameObject.SetActive(true);
			}

		}
	}

	public static void UnlockNextLevel(int nextLevelNumber)
	{
		//Only call this function on levels that have a next level


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
}
