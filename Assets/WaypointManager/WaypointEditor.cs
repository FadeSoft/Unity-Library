using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[InitializeOnLoad()]
public class WaypointEditor
{
    [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected | GizmoType.Pickable)]
    public static void OnDrawSceneGizmo(Waypoint waypoint, GizmoType gizmoType)
    {
        Gizmos.color = Color.yellow;

        Gizmos.DrawSphere(waypoint.transform.position, .2f);
        Gizmos.color = Color.white;
        Gizmos.DrawLine(waypoint.transform.position + (waypoint.transform.right * waypoint.widht / 2f),
            waypoint.transform.position - (waypoint.transform.right * waypoint.widht / 2f));


        if (waypoint.nextWaypoint != null)
        {
            Gizmos.color = Color.red;
            Vector3 lineStartPoint = waypoint.transform.right * waypoint.widht / 2f;
            Vector3 lineEndPoint = waypoint.nextWaypoint.transform.right * waypoint.nextWaypoint.widht / 2f;
            Gizmos.DrawLine(waypoint.transform.position + lineStartPoint, waypoint.nextWaypoint.transform.position + lineEndPoint);

        }
        if (waypoint.previousWaypoint != null)
        {
            Gizmos.color = Color.green;
            Vector3 lineStartPoint = waypoint.transform.right * -waypoint.widht / 2f;
            Vector3 lineEndPoint = waypoint.nextWaypoint.transform.right * -waypoint.nextWaypoint.widht / 2f;
            Gizmos.DrawLine(waypoint.transform.position + lineStartPoint, waypoint.nextWaypoint.transform.position + lineEndPoint);
        }
    }
}
