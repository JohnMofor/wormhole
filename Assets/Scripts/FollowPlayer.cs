using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour
{

	// public
	public Transform playerTransform;

	// Update is called once per frame
	void Update ()
	{

		transform.position = new Vector3(playerTransform.position.x, transform.position.y, playerTransform.position.z);
	
	}


}
