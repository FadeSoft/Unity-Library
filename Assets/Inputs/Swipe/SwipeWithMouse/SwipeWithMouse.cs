using UnityEngine;
using UnityEngine.Events;

namespace Fade.Inputs.Swipe.WithMouse
{
    [System.Serializable] public class SwipeEvent : UnityEvent<Vector3> { }

    public class SwipeWithMouse : MonoBehaviour
    {
        private SwipeEvent OnSwipeChanged = new SwipeEvent();

        private Vector2 firstPressPos;
        private Vector2 secondPressPos;
        private Vector2 currentSwipe;

        private void Update()
        {
            Swipe();
        }
        private void Swipe()
        {
            if (Input.GetMouseButtonDown(0)) firstPressPos = Input.mousePosition;
            if (!Input.GetMouseButtonUp(0)) return;

            secondPressPos = Input.mousePosition;
            currentSwipe = new Vector2(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
            currentSwipe.Normalize();

            if (currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
            {
                OnSwipeChanged.Invoke(Vector3.up);
                //Up
            }
            if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
            {
                OnSwipeChanged.Invoke(Vector3.down);
                //Down
            }
            if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                OnSwipeChanged.Invoke(Vector3.left);
                //Left
            }
            if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
            {
                OnSwipeChanged.Invoke(Vector3.right);
                //Right
            }
        }
    }
}