using UnityEditor;
using System;// exception.

class BuildScript {
	static void BuildWebPlayer () {
		string buildTarget = System.Environment.GetEnvironmentVariable ("UNITY_BUILD_TARGET");
		if (buildTarget == null || buildTarget.Length == 0) {
			throw new Exception ("UNITY_BUILD_TARGET -system property not defined, aborting.");
		}

		string[] scenes = { "Assets/_Scenes/startMenu.unity", "Assets/_Scenes/tutorial2d.unity", "Assets/_Scenes/core3d.unity" };
		
		string error = BuildPipeline.BuildPlayer (
			scenes, 
			buildTarget, 
			BuildTarget.WebPlayer, 
			BuildOptions.None);
		
		if (error != null && error.Length > 0) {
			throw new Exception ("Build failed: " + error);
		}
	}
}