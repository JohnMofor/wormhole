using UnityEngine;
using System.Collections;

public class RotateAround : MonoBehaviour {

	public float angle = 10f;
	public Vector3 center = Vector3.zero;

	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.RotateAround(center, Vector3.up, angle * Time.deltaTime);
	}
}
