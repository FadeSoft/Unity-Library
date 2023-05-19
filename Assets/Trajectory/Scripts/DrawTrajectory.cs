using System.Collections.Generic;
using UnityEngine;
using Fade.Trajectory;
using Fade.RayUtils;

public class DrawTrajectory : MonoBehaviour
{
    #region Fields
    [Tooltip("LineRenderer Component"), SerializeField]
    private LineRenderer line;
    [Tooltip("Source Position For 3D"), SerializeField]
    private Transform source;
    [Tooltip("Target Position For 3D"), SerializeField]
    private Transform target;
    [Tooltip("Determines how many points will be located on the trajectory"), SerializeField]
    private int linePoints;
    [Tooltip("Line Resolution")]
    private readonly float timeBetweenPoints = .1f;
    [Tooltip("Trajectory's Height"), SerializeField]
    private float h;
    [Tooltip("Gravity Multiplier, Do Not Change"), SerializeField]
    private float g;
    [Tooltip("Height Multiplier"), SerializeField]
    private float hMultiplier = .2f;
    //rayUtility is my another ready made tool. To use RayUtils you should add the "using Fade.RayUtils;".
    private RayUtils rayUtility = new RayUtils(10000f);
    public GameObject launchableObject;
    [Tooltip("Should It Be Drawn"), SerializeField]
    private bool isDraw=true;
    #endregion

    private void Update()
    {
        if (isDraw)
        {
            if (Input.GetMouseButton(0))
            {
                //LineRenderer component enabled equal to true
                line.enabled = true;
                //The target position is moved by raycast.
                target.transform.position = rayUtility.GetMouseWorldPosition(Input.mousePosition, transform.position);
                //Some calculations for the height of the trajectory
                h = (target.position - source.position).magnitude * hMultiplier;
                //We assign values of type Vector3 returned from the Draw3DLine function to a list and give this list to the linerenderer
                List<Vector3> LinePoints = TrajectoryCalculations.Draw3DLine(source.position, target.position, linePoints, timeBetweenPoints, h, g);
                line.positionCount = LinePoints.Count;
                line.SetPositions(LinePoints.ToArray());
            }

            if (Input.GetMouseButtonUp(0))
            {
                //LineRenderer component enabled equal to false
                line.enabled = false;
                //When we lift our hand from the mouse, we create the object
                GameObject obj = Instantiate(launchableObject, source.position, Quaternion.identity);
                //Calculate the velocity with CalculateInitialVelocity and give it to the object as an add force.
                obj.GetComponent<Rigidbody>().AddForce(TrajectoryCalculations.CalculateInitialVelocity(source.position, target.position, h, g), ForceMode.Impulse);
            }
        }
    }
}
