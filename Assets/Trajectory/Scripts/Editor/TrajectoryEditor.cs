
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DrawTrajectory)), CanEditMultipleObjects]
public class TrajectoryEditor : Editor
{
    bool showPosition = false;
    private SerializedProperty
                _line,
                _source,
                _target,
                _isDraw,
                _launchableObject,
                _linePoints,
                _h,
                _hMultiplier;

    private void OnEnable()
    {
        _line = serializedObject.FindProperty("line");
        _source = serializedObject.FindProperty("source");
        _target = serializedObject.FindProperty("target");
        _isDraw = serializedObject.FindProperty("isDraw");
        _launchableObject = serializedObject.FindProperty("launchableObject");
        _linePoints = serializedObject.FindProperty("linePoints");
        _h = serializedObject.FindProperty("h");
        _hMultiplier = serializedObject.FindProperty("hMultiplier");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawInfo();
        DrawFields();

        showPosition = EditorGUILayout.Foldout(showPosition, "Show Trajectory Values");
        if (showPosition) DrawTrajectoryValues();

        serializedObject.ApplyModifiedProperties();
    }
    private void DrawInfo()
    {
        GUILayout.BeginVertical();
        {
            EditorGUILayout.HelpBox("This package was created by FadeSoftware. \nVisit www.blog.fadesoftware.net for details.", MessageType.Info);
            if (GUILayout.Button("Visit Web Page"))
                Application.OpenURL("https://blog.fadesoftware.net");
        }
        GUILayout.EndVertical();
    }
    private void DrawFields()
    {
        EditorGUILayout.BeginVertical();
        {
            EditorGUILayout.Space(10);
            EditorGUILayout.PropertyField(_line);
            EditorGUILayout.PropertyField(_source);
            EditorGUILayout.PropertyField(_target);
            EditorGUILayout.PropertyField(_launchableObject);
            EditorGUILayout.PropertyField(_isDraw);
            EditorGUILayout.Space(10);
        }
        GUILayout.EndVertical();
    }
    private void DrawTrajectoryValues()
    {
        EditorGUILayout.BeginVertical();
        {
            EditorGUILayout.PropertyField(_linePoints);
            EditorGUILayout.PropertyField(_h);
            EditorGUILayout.PropertyField(_hMultiplier);
            EditorGUILayout.Space(10);
        }
        GUILayout.EndVertical();
    }
}
