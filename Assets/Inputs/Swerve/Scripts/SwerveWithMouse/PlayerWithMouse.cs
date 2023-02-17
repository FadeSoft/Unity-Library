using UnityEngine;
using Fade.Inputs.Swerve.WithMouse;
public class PlayerWithMouse : MonoBehaviour
{
    public SwerveControllerWithMouse swerveController;
    private void Start()
    {
        swerveController.OnSwerveChanged.AddListener(Move);
    }

    private void Move(float x)
    {
        transform.position = new Vector3(x, 0, 0);
    }
}
