using UnityEngine;
using UnityEditor;

public class WaypointCreatorWindow : EditorWindow
{
    public Transform waypointRoot;
    [MenuItem("Window/WaypointCreator/WaypointCreatorWindow")]
    public static void ShowWindow()
    {
        GetWindow<WaypointCreatorWindow>("My Window");
    }

    private void OnGUI()
    {
        SerializedObject obj = new SerializedObject(this);
        EditorGUILayout.PropertyField(obj.FindProperty("waypointRoot"));

        if (waypointRoot == null) EditorGUILayout.HelpBox("Root transform must be selected ", MessageType.Error);
        EditorGUILayout.Space();

        if (GUILayout.Button("Create Waypoint Manager"))
        {
            CreateWaypointManager();
        }
        if (GUILayout.Button("Delete Waypoint Manager"))
        {
            DeleteWaypointManager();
        }
        EditorGUILayout.Space();

        if (GUILayout.Button("Create Waypoint Node"))
        {
            CreateWaypoint();
        }

        if (GUILayout.Button("Delete Selected Node"))
        {
            DeleteSelectedNode();
        }
        if (GUILayout.Button("Delete Last Node"))
        {
            DeleteLastNode();
        }
        if (GUILayout.Button("Sort The Nodes"))
        {
            SortTheNodes();
            Debug.Log("obs");
        }
        obj.ApplyModifiedProperties();
    }
    private void CreateWaypointManager()
    {
        if (waypointRoot != null) return;
        GameObject waypointSystem = new GameObject("WaypointSystem", typeof(WaypointManager));
        waypointRoot = waypointSystem.transform;
        Selection.activeObject = waypointSystem;

    }
    private void DeleteWaypointManager()
    {
        if (waypointRoot == null) return;
        DestroyImmediate(waypointRoot.gameObject);

    }
    private void CreateWaypoint()
    {
        GameObject waypointNode = new GameObject("waypointRoot" + waypointRoot.childCount, typeof(Waypoint));
        waypointNode.transform.SetParent(waypointRoot);

        Waypoint waypoint = waypointNode.GetComponent<Waypoint>();
        waypointRoot.GetComponent<WaypointManager>().waypoints.Add(waypoint);

        Selection.activeObject = waypoint;

    }
    private void DeleteSelectedNode()
    {
        if (!Selection.activeGameObject.GetComponent<Waypoint>()) return;

        Waypoint waypoint = Selection.activeTransform.GetComponent<Waypoint>();
        waypointRoot.GetComponent<WaypointManager>().waypoints.Remove(waypoint);
        DestroyImmediate(waypoint.gameObject);
    }
    private void DeleteLastNode()
    {
        int lastIndex = waypointRoot.GetComponent<WaypointManager>().waypoints.Count - 1;

        Waypoint waypoint = waypointRoot.GetComponent<WaypointManager>().waypoints[lastIndex];
        waypointRoot.GetComponent<WaypointManager>().waypoints.Remove(waypoint);
        DestroyImmediate(waypoint.gameObject);
    }
    private void SortTheNodes()
    {
        WaypointManager waypointManager = waypointRoot.GetComponent<WaypointManager>();
        Debug.Log(waypointManager.waypoints.Count);
        for (int i = 0; i <= waypointManager.waypoints.Count; i++)
        {
            waypointManager.waypoints[i].nextWaypoint = waypointManager.waypoints[i + 1];
            if (i == waypointManager.waypoints.Count)
            {
                Debug.Log("a");
                waypointManager.waypoints[i].nextWaypoint = waypointManager.waypoints[0];
            }
        }
    }

}