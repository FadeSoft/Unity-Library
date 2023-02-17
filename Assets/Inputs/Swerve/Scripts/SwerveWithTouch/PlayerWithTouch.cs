using UnityEngine;
using Fade.Inputs.Swerve.WithTouch;
public class PlayerWithTouch : MonoBehaviour
{
    public SwerveControllerWithTouch swerveController;
    private void Start()
    {
        swerveController.OnSwerveChanged.AddListener(Move);
    }

    private void Move(float x)
    {
        transform.position = new Vector3(x, 0, 0);
    }
}
