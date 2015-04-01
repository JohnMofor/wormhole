using UnityEngine;
using System.Collections;

public class blackHolePullOnContact : MonoBehaviour
{

	public float blackHolePullForce = 1000F;

	void OnTriggerStay (Collider other)
	{

		Rigidbody otherRb = other.gameObject.GetComponent<Rigidbody> ();
		Vector3 pull = (transform.position - otherRb.transform.position).normalized * blackHolePullForce * (Mathf.Max (0.01f, 1F / Vector3.Magnitude (transform.position - otherRb.transform.position)));
		otherRb.AddForce (pull);
		
	}

	// Update is called once per frame
	void Update ()
	{
	
	}
}
