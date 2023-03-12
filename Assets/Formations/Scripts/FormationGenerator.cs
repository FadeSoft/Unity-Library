using System.Collections.Generic;
using UnityEngine;
using Fade.Formations;
public class FormationGenerator : MonoBehaviour
{
    private readonly Formations formations = new Formations();
    [SerializeField] private List<Vector3> points = new List<Vector3>();
    [SerializeField] private List<Quaternion> rots = new List<Quaternion>();
    public GameObject unit;
    void Start()
    {
        Application.targetFrameRate = 60;
        (points, rots) = formations.Cylinder(20, 2, Vector3.zero, true);
        for (int i = 0; i < points.Count; i++)
        {
            Instantiate(unit, points[i], rots[i]);
        }
    }
}
