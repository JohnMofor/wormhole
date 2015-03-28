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
	void FixedUpdate ()
	{
		transform.position = playerTransform.position;

		transform.rotation = Quaternion.Euler (playerTransform.rotation.eulerAngles.x,
		                                      playerTransform.rotation.eulerAngles.y,
		                                      playerTransform.rotation.eulerAngles.z);
		//transform.rotation = playerTransform.rotation;

	}
}
