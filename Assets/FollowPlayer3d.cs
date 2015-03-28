using UnityEngine;
using System.Collections;

public class FollowPlayer3d : MonoBehaviour
{


	public Transform playerTransform;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		transform.position = playerTransform.position;

		transform.rotation = playerTransform.rotation;

	}
}
