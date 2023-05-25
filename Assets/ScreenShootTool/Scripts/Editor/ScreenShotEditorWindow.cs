// Created by Fadesoftware 2023
using UnityEngine;
using UnityEditor;
using System;

public class ScreenShotEditorWindow : EditorWindow
{
    private int toolbarValue = 0;
    private bool showPositionFoldOut = false;
    public ScreenShootData data;
    private readonly string[] toolbarStrings = { "PNG", "JPG" };
    [MenuItem("Window/FadeSoftware/Fade's Screen Shot Tool")]
    public static void ShowWindow()
    {
        GetWindow<ScreenShotEditorWindow>("Fade's Screen Shot Tool");
        Debug.Log("Welcome to Fade's Screen Shot Tool");
    }
    private void OnGUI()
    {
        SerializedObject obj = new SerializedObject(this);

        GetDataFile();
        DrawLocaitonProperty();
        DrawLine();
        DrawScreenShotProperty();
        DrawLine();
        DrawScreenShotTakeButton();
        DrawHelpPart();

        obj.ApplyModifiedProperties();
    }
    private void GetDataFile()
    {
        if (data == null)
        {         
            string[] guids = AssetDatabase.FindAssets("ScreenShootData t:ScriptableObject", new[] { "Assets/ScreenShootTool" });
            if (guids.Length > 0)
            {
                string path = AssetDatabase.GUIDToAssetPath(guids[0]);
                data = AssetDatabase.LoadAssetAtPath<ScreenShootData>(path);
            }
            else
            {
                Debug.LogError("ScriptableObject not found in project. You may have change the location. Please relocate it as Assests/ScreenShootTool/ScreenShootData.");
            }
            EditorGUILayout.ObjectField("My ScriptableObject", data, typeof(ScriptableObject), false);
        }
    }
    private void DrawLocaitonProperty()
    {
        if (data.path == string.Empty)
        {
            GUILayout.BeginVertical();
            {
                EditorGUILayout.HelpBox("Please select the location to be saved.", MessageType.Error);
               
            }
            GUILayout.EndVertical();
        }
        EditorGUILayout.TextField("Location", data.path);
        EditorGUILayout.Space(10);

        if (GUILayout.Button("Select Location")) SelectLocaiton();
    }
    private void DrawScreenShotProperty()
    {
        EditorGUILayout.BeginHorizontal();
        {
            EditorGUILayout.Space(10);
            if (GUILayout.Button("AddResolution")) AddResolution();
            if (GUILayout.Button("RemoveResolution")) RemoveResolution();
            EditorGUILayout.Space(10);

        }
        EditorGUILayout.EndHorizontal();

        for (int i = 0; i < data.resolutions.Count; i++)
        {
            EditorGUILayout.BeginHorizontal();
            {
                data.resolutions[i] = EditorGUILayout.Vector2IntField("Resolution", data.resolutions[i]);
            }
            EditorGUILayout.EndHorizontal();
        }

        EditorGUILayout.Space(10);

        EditorGUILayout.BeginHorizontal();
        {
            EditorGUILayout.Space(10);
            toolbarValue = (int)data.screenShotType;
            toolbarValue = GUILayout.Toolbar((int)data.screenShotType, toolbarStrings);
            data.screenShotType = (ScreenShootData.ScreenShotType)toolbarValue;
            EditorGUILayout.Space(10);
        }
        EditorGUILayout.EndHorizontal();

        if (data.screenShotType == ScreenShootData.ScreenShotType.PNG)
        {
            EditorGUILayout.BeginVertical();
            {
                EditorGUILayout.Space(10);
                data.isTransparent = EditorGUILayout.Toggle("Transparent", data.isTransparent);
            }
            EditorGUILayout.EndVertical();
        }
    }
    private void DrawScreenShotTakeButton()
    {
        if (GUILayout.Button("Take Screen Shot(s)")) TakeScreenshot();

        EditorUtility.SetDirty(data);
        EditorGUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("Open Save Path")) Application.OpenURL(data.path);
        }
        EditorGUILayout.EndHorizontal();
    }
    private void DrawHelpPart()
    {
        showPositionFoldOut = EditorGUILayout.Foldout(showPositionFoldOut, "Need Help ?");
        if (showPositionFoldOut)
        {
            DrawInfo();

            EditorGUILayout.BeginHorizontal();
            {
                if (GUILayout.Button("Send Mail For Help")) Application.OpenURL("mailto:softwarefade@gmail.com");
                if (GUILayout.Button("Open How To Use PDF")) Application.OpenURL("https://www.blog.fadesoftware.net/wp-content/uploads/2023/05/Fades-Screen-Shot-Tool.pdf");
            }
            EditorGUILayout.EndHorizontal();
        }
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
    private void DrawLine()
    {
        EditorGUILayout.Space(5);
        Rect rect = EditorGUILayout.GetControlRect(false, 1);
        rect.height = 1;
        EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1));
        EditorGUILayout.Space(5);
    }
    private void TakeScreenshot()
    {
        if(data.path==string.Empty) throw new Exception("Please select the location to be saved.");
        if (data.resolutions.Count==0) throw new Exception("Please add a resolution to take a screenshot.");

        int count = data.resolutions.Count;
        for (int i = 0; i < count; i++)
        {
            ScreenShot.TakeScreenshot(data.path, data.resolutions[i].x, data.resolutions[i].y, data.screenShotType, data.isTransparent);
        }
    }
    private void AddResolution()
    {
        data.resolutions.Add(Vector2Int.zero);
    }
    private void RemoveResolution()
    {
        data.resolutions.RemoveAt(data.resolutions.Count - 1);
    }
    private void SelectLocaiton()
    {
        data.path = EditorUtility.OpenFolderPanel("The location where the screenshot will be saved", "", "");
        data.path = data.path.Replace("/", "\\\\");
    }
}

