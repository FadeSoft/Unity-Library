using UnityEngine;
using UnityEditor;
namespace Fade.WaypointSystem
{
    [CustomEditor(typeof(Waypoint)), CanEditMultipleObjects]
    public class WaypointEditor : Editor
    {
        private SerializedProperty
                _nodeType,
                _isFinal,
                _isBranch,
                _connections,
                _nextNode,
                _widht;
        private void OnEnable()
        {
            _nodeType = serializedObject.FindProperty("nodeType");
            _isFinal = serializedObject.FindProperty("isFinal");
            _isBranch = serializedObject.FindProperty("isBranch");
            _widht = serializedObject.FindProperty("widht");
            _nextNode = serializedObject.FindProperty("nextNode");

            _connections = serializedObject.FindProperty("connections");
        }
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            EditorGUILayout.PropertyField(_nodeType);

            Waypoint.NodeType nodeType = (Waypoint.NodeType)_nodeType.enumValueIndex;

            EditorGUILayout.PropertyField(_nextNode, new GUIContent("Next Node"));
            EditorGUILayout.PropertyField(_widht, new GUIContent("Widht"));
            switch (nodeType)
            {
                case Waypoint.NodeType.final:
                    EditorGUILayout.PropertyField(_isFinal, new GUIContent("Is Final"));
                    break;

                case Waypoint.NodeType.branch:
                    EditorGUILayout.PropertyField(_isBranch, new GUIContent("Is Branch"));
                    EditorGUILayout.PropertyField(_connections, new GUIContent("Connections"));
                    break;

                default:
                    break;
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
}