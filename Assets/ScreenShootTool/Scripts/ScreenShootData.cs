using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Fade's Screen Shoot Tool", menuName = "FadeSoftware/ScreenShoot Tool", order = 3)]
public class ScreenShootData : ScriptableObject
{
    public string path;
    public List<Vector2Int> resolutions = new List<Vector2Int>();
    public enum ScreenShotType { PNG = 0, JPG = 1 }
    public ScreenShotType screenShotType;
    public bool isTransparent;
}