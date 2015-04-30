using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathController : MonoBehaviour
{
	public float pointSeparation = 0.01f;
	public BezierSpline spline;

	private List<Vector3> points;
	private LineRenderer lineRenderer;

	// Use this for initialization
	void Start ()
	{
		lineRenderer = GetComponent<LineRenderer> ();
		Render();
	}
	
	// Update is called once per frame
	void Update ()
	{

	}

	private void Render ()
	{
		RenderBezier ();
	}

	private void RenderBezier ()
	{
		List<Vector3> drawingPoints = new List<Vector3>();
		for (float i = 0; i <= 1; i += pointSeparation) {
			drawingPoints.Add(spline.GetPoint(i));
		}
		SetLinePoints (drawingPoints);
	}

	private void SetLinePoints (List<Vector3> drawingPoints)
	{
		lineRenderer.SetVertexCount (drawingPoints.Count);
		
		for (int i = 0; i < drawingPoints.Count; i++) {
			lineRenderer.SetPosition (i, drawingPoints [i]);
		}
	}

}
