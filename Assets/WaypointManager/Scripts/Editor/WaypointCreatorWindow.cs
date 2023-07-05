using UnityEngine;
using UnityEditor;
using System;

namespace Fade.WaypointSystem
{
    //This package was created by FadeSoftware. Visit www.blog.fadesoftware.net for details.
    //Version 1.0 - FadeSoftware Waypoint Manager.
    public class WaypointCreatorWindow : EditorWindow
    {
        public Transform waypointRoot;
        public Transform nodeRoot;

        [MenuItem("Window/WaypointCreator/WaypointCreatorWindow")]
        public static void ShowWindow()
        {
            GetWindow<WaypointCreatorWindow>("Waypoint Creator");


        }

        private void OnGUI()
        {
            EditorGUILayout.HelpBox("This package was created by FadeSoftware. \nVisit www.blog.fadesoftware.net for details.", MessageType.Info);
            if (GUILayout.Button("Visit Web Page "))
                Application.OpenURL("https://blog.fadesoftware.net");

            EditorGUILayout.HelpBox("Version 1.0 - FadeSoftware Waypoint Manager.", MessageType.None);
            EditorGUILayout.Space(20);

            SerializedObject obj = new SerializedObject(this);
            EditorGUILayout.PropertyField(obj.FindProperty("waypointRoot"));
            EditorGUILayout.PropertyField(obj.FindProperty("nodeRoot"));

            if (waypointRoot == null) EditorGUILayout.HelpBox("Root transform must be selected ", MessageType.Error);
            EditorGUILayout.Space();
            if (GUILayout.Button("Create Waypoint Manager")) CreateWaypointManager();
            if (GUILayout.Button("Delete Waypoint Manager")) DeleteWaypointManager();
            EditorGUILayout.Space();
            if (GUILayout.Button("Create New Node")) CreateWaypoint();
            if (GUILayout.Button("Create Final Node")) CreateFinalNode();
            if (GUILayout.Button("Create New Branch")) CreateNewBranchNode();
            if (GUILayout.Button("Delete Selected Node")) DeleteSelectedNode();
            if (GUILayout.Button("Delete Last Node")) DeleteLastNode();
            if (GUILayout.Button("Sort The Nodes")) SortTheNodes();

            obj.ApplyModifiedProperties();
        }
        private void CreateWaypointManager()
        {
            if (waypointRoot != null) throw new Exception("Waypoint Manager already exist, there can only be one on the scene");
            GameObject waypointSystem = new GameObject("WaypointSystem", typeof(WaypointManager));
            waypointRoot = waypointSystem.transform;

            GameObject nodeRoots = new GameObject("NodeRoot" + waypointRoot.transform.childCount);
            nodeRoots.transform.parent = waypointSystem.transform;
            nodeRoot = nodeRoots.transform;
            Selection.activeObject = waypointSystem;

        }
        private void DeleteWaypointManager()
        {
            if (waypointRoot == null) throw new Exception("There is no Waypoint Manager to delete");
            DestroyImmediate(waypointRoot.gameObject);
        }
        private void CreateWaypoint()
        {
            if (waypointRoot == null) throw new Exception("Waypoint Manager must be assigned for create waypoints");

            GameObject waypointNode = new GameObject("waypointRoot" + waypointRoot.transform.childCount + " " + nodeRoot.childCount, typeof(Waypoint));
            waypointNode.transform.SetParent(nodeRoot);
            waypointNode.transform.SetPositionAndRotation(Selection.activeTransform.position, Selection.activeTransform.rotation);

            Waypoint waypoint = waypointNode.GetComponent<Waypoint>();
            waypoint.nodeType = Waypoint.NodeType.normal;

            waypointRoot.GetComponent<WaypointManager>().waypoints.Add(waypoint);
            Selection.activeObject = waypoint;

            SortTheNodes();
        }
        private void DeleteSelectedNode()
        {
            if (!Selection.activeGameObject.GetComponent<Waypoint>()) throw new Exception("The object you want to delete does not have the Waypoint class");

            Waypoint waypoint = Selection.activeTransform.GetComponent<Waypoint>();
            waypointRoot.GetComponent<WaypointManager>().waypoints.Remove(waypoint);

            if (waypoint.isBranch) waypoint.connections.Clear();
            DestroyImmediate(waypoint.gameObject);
        }
        private void DeleteLastNode()
        {
            if (waypointRoot.GetComponent<WaypointManager>().waypoints.Count <= 0) throw new Exception("There are no Nodes left to delete");
            WaypointManager waypointManager = waypointRoot.GetComponent<WaypointManager>();
            int lastIndex = waypointManager.waypoints.Count - 1;

            Waypoint waypoint = waypointManager.waypoints[lastIndex];
            waypointManager.waypoints.Remove(waypoint);
            if (waypoint.isBranch) waypoint.connections.Clear();

            DestroyImmediate(waypoint.gameObject);
        }
        private void CreateNewBranchNode()
        {
            if (waypointRoot == null) throw new Exception("Waypoint Manager must be assigned for create branch waypoints");

            GameObject nodeRoots = new GameObject("NodeRoot" + waypointRoot.transform.childCount);
            nodeRoots.transform.parent = waypointRoot.transform;
            nodeRoot = nodeRoots.transform;

            GameObject waypointNode = new GameObject("waypointRoot" + waypointRoot.transform.childCount + " " + nodeRoot.childCount, typeof(Waypoint));
            waypointNode.transform.SetParent(nodeRoot);

            Waypoint waypoint = waypointNode.GetComponent<Waypoint>();
            waypoint.nodeType = Waypoint.NodeType.normal;


            Waypoint branchedFrom = Selection.activeGameObject.GetComponent<Waypoint>();
            branchedFrom.connections.Add(waypoint);
            branchedFrom.isBranch = true;

            branchedFrom.nodeType = Waypoint.NodeType.normal;
            waypointRoot.GetComponent<WaypointManager>().waypoints.Add(waypoint);
            waypointNode.transform.SetPositionAndRotation(Selection.activeTransform.position, Selection.activeTransform.rotation);

            Selection.activeObject = waypointNode;
        }
        private void CreateFinalNode()
        {
            if (waypointRoot == null) throw new Exception("Waypoint Manager must be assigned for create branch waypoints");

            GameObject waypointNode = new GameObject("waypointRoot" + waypointRoot.transform.childCount + " " + nodeRoot.childCount, typeof(Waypoint));
            waypointNode.transform.SetParent(nodeRoot);
            waypointNode.transform.SetPositionAndRotation(Selection.activeTransform.position, Selection.activeTransform.rotation);

            Waypoint waypoint = waypointNode.GetComponent<Waypoint>();
            waypoint.nodeType = Waypoint.NodeType.final;
            waypoint.isFinal = true;

            waypointRoot.GetComponent<WaypointManager>().waypoints.Add(waypoint);
            Selection.activeObject = waypoint;

            SortTheNodes();
        }

        private void SortTheNodes()
        {
            if (waypointRoot == null) throw new Exception("Waypoint Manager must be assigned for sort the waypoints");

            WaypointManager waypointManager = waypointRoot.GetComponent<WaypointManager>();
            int countOfWaypoints = waypointManager.waypoints.Count - 1;
            for (int i = 0; i < countOfWaypoints; i++)
            {
                if (!waypointManager.waypoints[i].isFinal)
                    waypointManager.waypoints[i].nextNode = waypointManager.waypoints[i + 1];
            }
        }
    }
}
