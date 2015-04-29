using UnityEngine;
using System.Collections;

public class FixRotation : MonoBehaviour
{

	public float x = 180f;
	public float z = 35f;
	Rigidbody rb;
	// Use this for initialization
	void Start ()
	{
		rb = GetComponent<Rigidbody> ();
	}

	// Update is called once per frame
	void Update ()
	{
		rb.transform.rotation = Quaternion.Euler (new Vector3 (x, rb.transform.rotation.y, z));
	}
}
