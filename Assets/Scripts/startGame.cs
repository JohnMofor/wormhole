using UnityEngine;
using System.Collections;

public class startGame : MonoBehaviour {

	public void loadOpenWorld() {
		Application.LoadLevel("core3d");
	}

	public void loadTutorialLevel() {
		Application.LoadLevel("tutorial2d");
	}
}
