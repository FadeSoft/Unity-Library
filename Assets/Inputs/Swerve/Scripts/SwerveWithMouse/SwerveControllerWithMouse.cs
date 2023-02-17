using UnityEngine;
using UnityEngine.Events;

namespace Fade.Inputs.Swerve.WithMouse
{
    [System.Serializable] public class SwerveEvent : UnityEvent<float> { }

    public class SwerveControllerWithMouse : MonoBehaviour
    {
        public SwerveEvent OnSwerveChanged = new SwerveEvent();

        public float swerveSpeed;
        public bool canSwerve;
        public float clampX = 6;

        private float firstPos, currentPos;

        private void Update()
        {
            Swerve(Input.GetMouseButtonDown(0), Input.GetMouseButton(0), Input.mousePosition);
        }
        private void Swerve(bool isDown, bool isPressed, Vector3 mousePosition)
        {
            if (!canSwerve) return;

            if (isDown)
            {
                firstPos = mousePosition.x;
            }
            else if (isPressed)
            {
                float currentX = mousePosition.x;
                float deltaX = currentX - firstPos;
                float targetX = deltaX * swerveSpeed * Time.deltaTime;

                currentPos += (targetX);
                currentPos = Mathf.Clamp(currentPos, -clampX, clampX);
                firstPos = mousePosition.x;

                OnSwerveChanged.Invoke(currentPos);
            }
        }
    }
}