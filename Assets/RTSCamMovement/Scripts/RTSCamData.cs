using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RTSCamDatas")]
public class RTSCamData : ScriptableObject
{
    public float movementSpeed;
    public float rotationSpeed;
    public int screenBorderPx;
    public Vector3 minBorder, maxBorder;
    public Vector3 minZoomBorder, maxZoomBorder;
    public float zoomAmount;
    public float normalSpeed = 5f;
    public float fastestSpeed = 15f;
}
