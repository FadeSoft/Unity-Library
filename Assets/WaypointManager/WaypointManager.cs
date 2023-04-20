using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad]
public class WaypointManager : MonoBehaviour
{
    public List<Waypoint> waypoints = new List<Waypoint>();
    private Vector3 widht = new Vector3(0.3f, 0, 0);

   // [DrawGizmo(GizmoType.NonSelected | GizmoType.Selected | GizmoType.Pickable)]
    //private void OnDrawGizmos()
    //{
    //    for (int i = 0; i < transform.childCount - 1; i++)
    //    {
    //        Vector3 childTransform = transform.GetChild(i).position;
    //        Vector3 nextChildTransform = transform.GetChild(i + 1).position;
    //        Gizmos.DrawSphere(childTransform, 1f);

    //        Gizmos.DrawLine(childTransform + Vector3.one, childTransform - Vector3.one);
    //        Gizmos.DrawLine(childTransform + Vector3.one, childTransform - Vector3.one);

    //        Gizmos.color = Color.red;

    //        Gizmos.DrawLine(childTransform + Vector3.one, nextChildTransform + Vector3.one);

    //        Gizmos.color = Color.green;

    //        Gizmos.DrawLine(childTransform - Vector3.one, nextChildTransform - Vector3.one);
    //        Gizmos.color = Color.white;
    //    }
    //}
}
