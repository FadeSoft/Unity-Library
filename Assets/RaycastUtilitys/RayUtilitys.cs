using UnityEngine;

namespace Fade.RayUtils
{
    internal class RayUtils
    {
        private RaycastHit hit;
        private readonly float range;
        internal RayUtils(float maxRange)
        {
            range = maxRange;
        }

        internal bool CheckHit(Transform startPoint, Transform targetPoint, string tag, LayerMask layer)
        {
            if (Physics.Raycast(startPoint.position, startPoint.TransformDirection(targetPoint.position), out hit, range, layer) && hit.transform.CompareTag(tag))
                return true;
            else return false;
        }
        internal Vector3 GetMouseWorldPosition(Vector3 mousePos, Vector3 playerPos)
        {
            Ray ray = Camera.main.ScreenPointToRay(mousePos);

            if (Physics.Raycast(ray, out hit, range))
                return hit.point;
            else return playerPos;
        }
        internal Vector3 GetMouseWorldPosition(Vector3 mousePos, Vector3 playerPos, LayerMask layer)
        {
            Ray ray = Camera.main.ScreenPointToRay(mousePos);

            if (Physics.Raycast(ray, out hit, range, layer))
                return hit.point;
            else return playerPos;
        }
        internal T SelectObjectWithMouse<T>(Vector3 mousePos, LayerMask layer)
        {
            Ray ray = Camera.main.ScreenPointToRay(mousePos);

            if (Physics.Raycast(ray, out hit, range, layer))
                return hit.transform.gameObject.GetComponent<T>();
            else return default(T);
        }
    }
}

