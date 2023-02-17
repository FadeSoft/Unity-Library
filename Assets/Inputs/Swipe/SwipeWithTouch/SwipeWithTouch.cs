using UnityEngine;
using UnityEngine.Events;

//This class need Device Simulator to work on PC
namespace Fade.Inputs.Swipe.WithTouch
{
    [System.Serializable] public class SwipeEvent : UnityEvent<Vector3> { }

    public class SwipeWithTouch : MonoBehaviour
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
            if (Input.touchCount > 0)
            {
                Touch t = Input.GetTouch(0);
                if (t.phase == TouchPhase.Began)
                {
                    firstPressPos = new Vector2(t.position.x, t.position.y);
                }
                if (t.phase == TouchPhase.Ended)
                {
                    secondPressPos = new Vector2(t.position.x, t.position.y);

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
    }
}