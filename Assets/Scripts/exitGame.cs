using UnityEngine;
using System.Collections;

public class exitGame : MonoBehaviour {

	public void exit() {
		#if UNITY_EDITOR
		UnityEditor.EditorApplication.isPlaying = false;
		#elif UNITY_WEBPLAYER
		Application.OpenURL("http://google.com");
		#else
		Application.Quit();
		#endif
	}
}
