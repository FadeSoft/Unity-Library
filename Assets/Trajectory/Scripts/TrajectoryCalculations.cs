using System.Collections.Generic;
using UnityEngine;

namespace Fade.Trajectory
{
    public static class TrajectoryCalculations
    {
        /// <summary>
        /// Returns velocity between two points as Vector3.
        /// </summary>
        /// <param name="sourcePos">Trajectory's source position.</param>
        /// <param name="targetPos">Trajectory's final position.</param>
        /// <param name="h">Trajectory's Height.</param>
        /// <param name="g">Trajectory's Gravity Multiplier.</param>
        /// <returns>Returns calculated velocity between two points as Vector3.</returns>
        public static Vector3 CalculateInitialVelocity(Vector3 sourcePos, Vector3 targetPos, float h, float g)
        {
            float dispY = targetPos.y - sourcePos.y;
            Vector3 dispXZ = new Vector3(targetPos.x - sourcePos.x, 0, targetPos.z - sourcePos.z);
            Vector3 velY = Vector3.up * Mathf.Sqrt(-2 * g * h);
            Vector3 velXZ = dispXZ / (Mathf.Sqrt(-2 * h / g) + Mathf.Sqrt(2 * (dispY - h) / g));
            return velXZ + velY;
        }
        /// <summary>
        /// Returns the points on the trajectory for the line renderer.
        /// </summary>
        /// <param name="sourcePos">LineRenderer's source position.</param>
        /// <param name="targetPos">LineRenderer's final position.</param>
        /// <param name="linePoint">LineRenderer's point count.</param>
        /// <param name="timeBetweenPoints">LineRenderer's resolutions.</param>
        /// <returns>Returns calculated velocity between two points as Vector3.</returns>
        public static List<Vector3> Draw3DLine(Vector3 sourcePos, Vector3 targetPos, int linePoint, float timeBetweenPoints, float h, float g)
        {
            List<Vector3> linePoints = new List<Vector3>();
            Vector3 vel = CalculateInitialVelocity(sourcePos, targetPos, h, g);

            for (int i = 0; i < linePoint; i++)
            {
                float time = i * timeBetweenPoints;
                Vector3 result = sourcePos + (vel) * time;
                float sY = (-0.5f * Mathf.Abs(Physics.gravity.y) * (time * time)) + (vel.y * time) + sourcePos.y;
                result.y = sY;
                linePoints.Add(result);
            }
            return linePoints;
        }
        /// <summary>
        /// Returns the points on the 2D trajectory for the line renderer.
        /// </summary>
        /// <param name="target">Trajectory's source position.</param>
        /// <param name="v0">Initial Velocity.</param>
        /// <param name="angle">Angle.</param>
        /// <param name="linePoint">LineRenderer's point count.</param>
        /// <param name="timeBetweenPoints">LineRenderer's resolutions.</param>
        /// <returns>Returns calculated velocity between two points as Vector3.</returns>
        public static List<Vector3> Draw2DLine(Vector3 target, float v0, float angle, int linePoint, float timeBetweenPoints)
        {
            List<Vector3> linePoints = new List<Vector3>();

            for (float i = 0; i < linePoint; i += timeBetweenPoints)
            {
                float x = (float)(v0 * i * Mathf.Cos(angle));
                float y = (float)(v0 * i * Mathf.Sin(angle) - (5 * Mathf.Pow(i, 2)));
                Vector3 result = target + new Vector3(x, y, 0);

                linePoints.Add(result);
            }
            return linePoints;
        }
    }
}