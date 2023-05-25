using System;
using System.IO;
using UnityEngine;
public static class ScreenShot
{
    public static void TakeScreenshot(string loc, int width, int height, ScreenShootData.ScreenShotType screenShotType, bool isTransparent)
    {
        Camera cam = Camera.main;
        RenderTexture renderTexture = new RenderTexture(width, height, 24);
        cam.targetTexture = renderTexture;

        Texture2D screenTexture = new Texture2D(width, height, TextureFormat.ARGB32, false);

        if (isTransparent && screenShotType == ScreenShootData.ScreenShotType.PNG)
        {
            screenTexture.alphaIsTransparency = true;
            cam.clearFlags = CameraClearFlags.SolidColor;
        }

        cam.backgroundColor = Color.clear;
        cam.Render();

        RenderTexture.active = renderTexture;
        screenTexture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        screenTexture.Apply();
        RenderTexture.active = null;

        byte[] imageBytes = null;
        string fileName = DateTime.Now.ToString("MM-dd-yy _ HH-mm-ss-fff");

        switch (screenShotType)
        {
            case ScreenShootData.ScreenShotType.PNG:
                imageBytes = screenTexture.EncodeToPNG();
                fileName += " ScrenShot.png";
                break;
            case ScreenShootData.ScreenShotType.JPG:
                imageBytes = screenTexture.EncodeToJPG();
                fileName += " ScrenShot.jpg";
                break;
        }

        cam.targetTexture = null;
        cam.clearFlags = CameraClearFlags.Skybox;
        renderTexture.Release();

        File.WriteAllBytes(Path.Combine(loc, fileName), imageBytes);
    }
}
