using UnityEngine;
using UnityEditor;
namespace Fade.WaypointSystem
{
    public class WaypointGizmosDrawer
    {
        [DrawGizmo(GizmoType.Selected | GizmoType.NonSelected | GizmoType.Pickable)]
        public static void OnDrawSceneGizmo(Waypoint waypoint, GizmoType gizmoType)
        {
            if ((gizmoType & GizmoType.Selected) != 0) Gizmos.color = Color.yellow;
            else Gizmos.color = Color.yellow * 0.7f;

            Gizmos.DrawSphere(waypoint.transform.position, .1f);
            Gizmos.color = Color.white;
            Gizmos.DrawLine(waypoint.transform.position + (waypoint.transform.right * waypoint.widht / 2f),
                waypoint.transform.position - (waypoint.transform.right * waypoint.widht / 2f));

            if (waypoint.nextNode != null)
            {
                Gizmos.color = Color.red;
                //RIGHT
                Vector3 lineStartPointR = waypoint.transform.right * waypoint.widht / 2f;
                Vector3 lineEndPointR = waypoint.nextNode.transform.right * waypoint.nextNode.widht / 2f;
                Gizmos.DrawLine(waypoint.transform.position + lineStartPointR, waypoint.nextNode.transform.position + lineEndPointR);

                Gizmos.color = Color.green;
                //LEFT
                Vector3 lineStartPointL = waypoint.transform.right * -waypoint.widht / 2f;
                Vector3 lineEndPointL = waypoint.nextNode.transform.right * -waypoint.nextNode.widht / 2f;
                Gizmos.DrawLine(waypoint.transform.position + lineStartPointL, waypoint.nextNode.transform.position + lineEndPointL);
            }

            if (waypoint.connections != null)
            {
                foreach (Waypoint branch in waypoint.connections)
                {
                    Gizmos.color = Color.blue;
                    Gizmos.DrawLine(waypoint.transform.position, branch.transform.position);

                }
            }
        }
    }
}

