using UnityEngine;
using Fade.RayUtils;

public class RayUtilityExample : MonoBehaviour
{
    public Transform target;
    RayUtils rayUtility = new RayUtils(10000f);
    public MeshRenderer selectedObj;

    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            transform.position = rayUtility.GetMouseWorldPosition(Input.mousePosition, transform.position, 1 << 8);
            MeshRenderer selectedObj = rayUtility.SelectObjectWithMouse<MeshRenderer>(Input.mousePosition, 1 << 8);
        }
    }
}
