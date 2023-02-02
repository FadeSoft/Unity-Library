using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fade.BezierCurves;

public class GenerateCurve : MonoBehaviour
{
    public Vector3[] points = new Vector3[] { };
    public List<Vector3> dpoints = new List<Vector3>();
    public GameObject obj;
    private void Start()
    {
        dpoints = Fade.BezierCurves.BezierCurve.DrawCurve(points, BezierCurve.CurveType.Quadratic, .04f);

        for (int i = 0; i < dpoints.Count; i++)
        {
            Instantiate(obj, dpoints[i], Quaternion.identity,transform);
        }
    }
}
