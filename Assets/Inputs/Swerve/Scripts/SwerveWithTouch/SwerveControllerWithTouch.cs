using UnityEngine;
using UnityEngine.Events;

//This class need Device Simulator to work on PC

namespace Fade.Inputs.Swerve.WithTouch
{
    [System.Serializable]  public class SwerveEvent : UnityEvent<float> { }

    public class SwerveControllerWithTouch : MonoBehaviour
    {
        public SwerveEvent OnSwerveChanged = new SwerveEvent();

        public float swerveSpeed;
        public bool canSwerve;
        public float clampX = 6;

        private float firstPos, currentPos;

        private void Update()
        {
            Swerve();
        }
        private void Swerve()
        {
            if (Input.touchCount > 0)
            {
                Touch t = Input.GetTouch(0);

                if (t.phase == TouchPhase.Began)
                {
                    firstPos = t.position.x;
                }
                else if (t.phase == TouchPhase.Moved)
                {
                    float currentX = t.position.x;
                    float deltaX = currentX - firstPos;
                    float targetX = deltaX * swerveSpeed * Time.deltaTime;

                    currentPos += (targetX);
                    currentPos = Mathf.Clamp(currentPos, -clampX, clampX);
                    firstPos = t.position.x;

                    OnSwerveChanged.Invoke(currentPos);
                }
            }
        }
    }
}