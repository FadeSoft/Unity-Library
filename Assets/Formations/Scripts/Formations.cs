using System.Collections.Generic;
using UnityEngine;

namespace Fade.Formations
{
    internal class Formations
    {
        internal List<Vector3> SquareFormation(float widht, float depth, bool isNoise)
        {
            List<Vector3> points = new List<Vector3>();
            Vector3 pos;

            float xAxis = widht / 2f;
            float zAxis = depth / 2f;

            for (float x = -xAxis; x <= xAxis; x++)
            {
                for (float z = -zAxis; z <= zAxis; z++)
                {
                    pos = new Vector3(x, 0, z);
                    if (isNoise) pos += GetNoise(.6f);
                    points.Add(pos);
                }
            }
            return points;
        }
        internal List<Vector3> CubeFormation(int edgeLength, bool isNoise)
        {
            List<Vector3> points = new List<Vector3>();
            Vector3 pos;

            float xAxis = edgeLength / 2f;
            float yAxis = edgeLength / 2f;
            float zAxis = edgeLength / 2f;

            for (float x = -xAxis; x <= xAxis; x++)
            {
                for (float z = -yAxis; z <= yAxis; z++)
                {
                    for (float y = -zAxis; y <= zAxis; y++)
                    {
                        pos = new Vector3(x, y, z);
                        if (isNoise) pos += GetNoise(.6f);
                        points.Add(pos);
                    }
                }
            }
            return points;
        }
        internal List<Vector3> TrapezoidFormation(float widht, float depth, bool isNoise)
        {
            List<Vector3> points = new List<Vector3>();
            Vector3 pos;

            float xAxis = widht / 2f;
            float zAxis = depth / 2f;

            for (float z = -zAxis; z <= zAxis; z++)
            {
                for (float x = -xAxis; x <= xAxis; x++)
                {
                    pos = new Vector3(x, 0, z);
                    if (isNoise) pos += GetNoise(.6f);
                    points.Add(pos);
                }
                xAxis++;
            }
            return points;
        }

        internal List<Vector3> PyramidFormation(float widht, float height, float depth, bool isNoise)
        {
            List<Vector3> points = new List<Vector3>();
            Vector3 pos;

            float xAxis = widht / 2f;
            float zAxis = depth / 2f;

            for (int y = 0; y < height; y++)
            {
                for (float x = -xAxis; x <= xAxis; x++)
                {
                    for (float z = -zAxis; z <= zAxis; z++)
                    {
                        pos = new Vector3(x, y, z);
                        if (isNoise) pos += GetNoise(.6f);
                        points.Add(pos);
                    }
                }
                xAxis--;
                zAxis--;
            }
            return points;
        }

        internal List<Vector3> TriangleFormation(float widht, float depth, bool isNoise)
        {
            List<Vector3> points = new List<Vector3>();
            Vector3 pos;

            float xAxis = widht / 2f;
            float zAxis = depth / 2f;

            for (float x = -xAxis; x <= xAxis; x++)
            {
                for (float z = -zAxis; z <= zAxis; z++)
                {
                    pos = new Vector3(x, 0, z);
                    if (isNoise) pos += GetNoise(.6f);
                    points.Add(pos);
                }
                zAxis -= 1;
            }
            return points;
        }

        internal List<Vector3> TrianglePrismFormation(float widht, float height, float depth, bool isNoise)
        {
            List<Vector3> points = new List<Vector3>();
            Vector3 pos;

            float xAxis = widht / 2f;
            float yAxis = height / 2f;
            float zAxis = depth / 2f;

            for (float x = -xAxis; x <= xAxis; x++)
            {
                for (float z = -zAxis; z <= zAxis; z++)
                {
                    for (int y = 0; y < yAxis; y++)
                    {
                        pos = new Vector3(x, y, z);
                        if (isNoise) pos += GetNoise(.6f);
                        points.Add(pos);
                    }
                }
                zAxis -= 1;
            }
            return points;
        }

        internal (List<Vector3>, List<Quaternion>) Circle(float numberOfObjects, float radius, Vector3 circleCenter, bool isNoise)
        {
            List<Vector3> points = new List<Vector3>();
            List<Quaternion> rots = new List<Quaternion>();
            Vector3 pos;

            for (int i = 0; i < numberOfObjects; i++)
            {
                float angle = i * Mathf.PI * 2 / numberOfObjects;
                float x = Mathf.Cos(angle) * radius;
                float z = Mathf.Sin(angle) * radius;
                float angleDegrees = -angle * Mathf.Rad2Deg;

                pos = circleCenter + new Vector3(x, 0, z);
                Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);

                if (isNoise) pos += GetNoise(.6f);

                rots.Add(rot);
                points.Add(pos);
            }
            return (points, rots);
        }

        internal (List<Vector3>, List<Quaternion>) Arena(float numberOfObjects, float height, float radius, Vector3 circleCenter, bool isNoise)
        {
            List<Vector3> points = new List<Vector3>();
            List<Quaternion> rots = new List<Quaternion>();
            Vector3 pos;

            for (int y = 0; y < height; y++)
            {
                for (int i = 0; i < numberOfObjects; i++)
                {
                    float angle = i * Mathf.PI * 2 / numberOfObjects;
                    float x = Mathf.Cos(angle) * radius;
                    float z = Mathf.Sin(angle) * radius;
                    float angleDegrees = -angle * Mathf.Rad2Deg;

                    pos = circleCenter + new Vector3(x, y, z);
                    Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);

                    if (isNoise) pos += GetNoise(.6f);

                    rots.Add(rot);
                    points.Add(pos);
                }
                radius += 1;
            }
            return (points, rots);
        }

        internal List<Vector3> Sphere(float numberOfObjects, float height, float radius, Vector3 circleCenter, bool isNoise)
        {
            List<Vector3> points = new List<Vector3>();
            Vector3 pos;

            for (int y = 0; y < height; y++)
            {
                for (int i = 0; i < numberOfObjects; i++)
                {
                    float angle = i * Mathf.PI * 2 / numberOfObjects;
                    float x = Mathf.Cos(angle) * radius;
                    float z = Mathf.Sin(angle) * radius;

                    pos = circleCenter + new Vector3(x, y, z);

                    if (isNoise) pos += GetNoise(.6f);
                    points.Add(pos);
                }
                if (y >= 5) radius--;
                else radius++;
            }
            return points;
        }

        internal (List<Vector3>, List<Quaternion>) Cypiral(float numberOfObjects, float radius, Vector3 circleCenter, bool isNoise)
        {
            List<Vector3> points = new List<Vector3>();
            List<Quaternion> rots = new List<Quaternion>();
            Vector3 pos;

            for (int i = 0; i < numberOfObjects; i++)
            {
                float angle = i * Mathf.PI * 2 / numberOfObjects;
                float x = Mathf.Cos(angle) * radius;
                float z = Mathf.Sin(angle) * radius;
                float angleDegrees = -angle * Mathf.Rad2Deg;

                pos = circleCenter + new Vector3(x, i, z);
                Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);

                if (isNoise) pos += GetNoise(.6f);

                rots.Add(rot);
                points.Add(pos);
            }
            return (points, rots);
        }

        internal (List<Vector3>, List<Quaternion>) Cylinder(float numberOfObjects, float radius, Vector3 circleCenter, bool isNoise)
        {
            List<Vector3> points = new List<Vector3>();
            List<Quaternion> rots = new List<Quaternion>();
            Vector3 pos;

            for (int i = 0; i < numberOfObjects; i++)
            {
                for (int y = 0; y < 10; y++)
                {
                    float angle = i * Mathf.PI * 2 / numberOfObjects;
                    float x = Mathf.Cos(angle) * radius;
                    float z = Mathf.Sin(angle) * radius;
                    float angleDegrees = -angle * Mathf.Rad2Deg;

                    pos = circleCenter + new Vector3(x, y, z);
                    Quaternion rot = Quaternion.Euler(0, angleDegrees, 0);

                    if (isNoise) pos += GetNoise(.6f);

                    rots.Add(rot);
                    points.Add(pos);
                }
            }
            return (points, rots);
        }
        internal Vector3 GetNoise(float noiseMultiplier)
        {
            float x = Random.Range(-noiseMultiplier, noiseMultiplier);
            return new Vector3(x, x, x);
        }
    }
}