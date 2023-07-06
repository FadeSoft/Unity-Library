using UnityEngine;
using UnityEditor;

namespace Fade.EditorTools.GroupObjects
{
    public class GroupObjectsEditor : EditorWindow
    {
        private string newGroupName;
        private int selectionCount;
        private bool showPositionFoldOut = false;

        [MenuItem("Window/EditorTools/GroupObjects")]
        public static void ShowWindow()
        {
            GetWindow<GroupObjectsEditor>("Group Objects");
        }
        private void OnGUI()
        {
            selectionCount = Selection.count;

            EditorGUILayout.BeginHorizontal();
            {
                EditorGUILayout.Space();
                EditorGUILayout.BeginVertical();
                {
                    EditorGUILayout.Space();
                    EditorGUILayout.LabelField("Selected Count: " + selectionCount, EditorStyles.boldLabel);

                    EditorGUILayout.Space();

                    EditorGUILayout.BeginVertical();
                    {
                        EditorGUILayout.Space();
                        EditorGUILayout.LabelField("Group Name: ", EditorStyles.boldLabel);
                        newGroupName = EditorGUILayout.TextField("New Group Name: ", "Empty");
                        EditorGUILayout.Space();
                    }
                    EditorGUILayout.EndVertical();

                    EditorGUILayout.Space();

                    if (GUILayout.Button("Group", GUILayout.Height(40)))
                    {
                        Group(selectionCount, Selection.gameObjects);
                    }
                    EditorGUILayout.Space();
                }
                EditorGUILayout.EndVertical();
                EditorGUILayout.Space();

            }
            EditorGUILayout.EndHorizontal();

            showPositionFoldOut = EditorGUILayout.Foldout(showPositionFoldOut, "Need Help ?");

            if (showPositionFoldOut)
            {
                DrawInfo();
                EditorGUILayout.BeginHorizontal();
                {
                    if (GUILayout.Button("Send Mail For Help")) Application.OpenURL("mailto:softwarefade@gmail.com");
                }
                EditorGUILayout.EndHorizontal();
            }
            Repaint();
        }

        private void Group(int selectedObjCount, GameObject[] selectedObjects)
        {
            if (selectedObjCount > 0)
            {
                GameObject newGroup = new GameObject(newGroupName);

                foreach (var item in selectedObjects)
                {
                    item.transform.SetParent(newGroup.transform);
                }

            }
            else EditorUtility.DisplayDialog("System Message", "YOU HAVE TO CHOOSE ONE AND MORE THAN ONE OBJECT", "OK");
            Selection.activeGameObject = null;
        }

        private void DrawInfo()
        {
            GUILayout.BeginVertical();
            {
                EditorGUILayout.HelpBox("This package was created by FadeSoftware.\nVisit www.blog.fadesoftware.net for details.", MessageType.Info);
                if (GUILayout.Button("Visit Web Page"))
                    Application.OpenURL("https://blog.fadesoftware.net");
            }
            GUILayout.EndVertical();
        }
    }
}