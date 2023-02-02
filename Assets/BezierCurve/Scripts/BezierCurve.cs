using System;
using System.Collections.Generic;
using UnityEngine;

namespace Fade.BezierCurves
{
    public static class BezierCurve
    {
        public enum CurveType { Linear, Quadratic, Cubic }
        public static CurveType curveType;
        private static List<Vector3> pList = new List<Vector3>();

        /// <summary>
        ///Creates curves between points you want.
        /// </summary>
        /// <param name="p">The points on the curve</param>
        /// <param name="curveType">Type of curve</param>
        /// <param name="pathDistance">Number of points on curve</param>
        /// <returns>Returns the coordinates of the curve according to your option.</returns>
        public static List<Vector3> DrawCurve(Vector3[] p, CurveType curveType, float pathDistance)
        {
            return curveType switch
            {
                CurveType.Linear => LinearBezierCurve(p, pathDistance),
                CurveType.Quadratic => QuadraticBezierCurve(p, pathDistance),
                CurveType.Cubic => CubicBezierCurve(p, pathDistance),
                _ => null
            };

        }
        #region Curve Generate Functions

        private static List<Vector3> LinearBezierCurve(Vector3[] p, float pathDistance)
        {
            if (p.Length != 2) throw new ArgumentOutOfRangeException();

            pList.Clear();
            for (float t = 0; t < 1; t += pathDistance)
            {
                pList.Add((1 - t) * p[0] + t * p[1]);

            }
            return pList;
        }
        private static List<Vector3> QuadraticBezierCurve(Vector3[] p, float pathDistance)
        {
            if (p.Length != 3) throw new ArgumentOutOfRangeException();

            pList.Clear();
            for (float t = 0; t < 1; t += pathDistance)
            {
                pList.Add(Multipler(1 - t, 2) * p[0] +
                    2 * Multipler(1 - t, 1) * t * p[1] +
                       Multipler(t, 2) * p[2]);
            }
            return pList;
        }

        private static List<Vector3> CubicBezierCurve(Vector3[] p, float pathDistance)
        {
            if (p.Length != 4) throw new ArgumentOutOfRangeException();

            pList.Clear();
            for (float t = 0; t < 1; t += pathDistance)
            {
                pList.Add(Multipler(1 - t, 3) * p[0] +
                    3 * Multipler(1 - t, 2) * t * p[1] +
                      3 * (1 - t) * Multipler(t, 2) * p[2] +
                      Multipler(t, 3) * p[3]);
            }
            return pList;
        }
        #endregion

        #region Power Of Number Function
        private static float Multipler(float a, float b)
        {
            float tmp = a;
            while (b > 1)
            {
                tmp *= a;
                b--;
            }
            return tmp;
        }
        #endregion
    }
}
