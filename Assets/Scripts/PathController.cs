using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathController : MonoBehaviour
{
	public float pointSeparation = 0.01f;
	public int updateSpeed = 6;
	public BezierSpline spline;
	private List<Vector3> points;
	private LineRenderer lineRenderer;

	// Use this for initialization
	void Start ()
	{
		lineRenderer = GetComponent<LineRenderer> ();
		StartCoroutine (render ());
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	private IEnumerator render ()
	{
		int numPoints = Mathf.RoundToInt (1f / pointSeparation);
		float currentPoint = 0f;
		for (int i = 0; i < numPoints; i++, currentPoint+=this.pointSeparation) {
			lineRenderer.SetVertexCount (i + 1);
			lineRenderer.SetPosition (i, spline.GetPoint (currentPoint));
			if (i % updateSpeed == 0) {
				yield return new WaitForFixedUpdate ();
			}
		}
		yield return null;
	}
}
