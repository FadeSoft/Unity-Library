using UnityEngine;

namespace Fade.RTS.Movement
{
    public class RtsCameraMovement : MonoBehaviour
    {
        private float speed;
        public RTSCamData rTSCamData;
        private Vector3 startPos;
        private Vector3 currentPos;
        private Camera cam;
        private Plane plane;
        private Transform childCam;

        private void Awake()
        {
            cam = Camera.main;
            speed = rTSCamData.normalSpeed;
            childCam = transform.GetChild(0).transform;
            plane = new Plane(Vector3.up, Vector3.zero);
        }
        private void CamMovement()
        {
            Vector3 pos = transform.position;
            Vector3 childPos = childCam.localPosition;

            //Camera rotation
            if (Input.GetKey(KeyCode.E))
            {
                transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y + (rTSCamData.rotationSpeed * Time.deltaTime), 0f);
            }
            if (Input.GetKey(KeyCode.Q))
            {
                transform.rotation = Quaternion.Euler(0f, transform.rotation.eulerAngles.y - (rTSCamData.rotationSpeed * Time.deltaTime), 0f);
            }

            //Zoom in-out with scrollWheel
            if (Input.GetAxisRaw("Mouse ScrollWheel") > 0)
            {
                childPos.y -= rTSCamData.zoomAmount;
                childPos.z += rTSCamData.zoomAmount;
            }
            if (Input.GetAxisRaw("Mouse ScrollWheel") < 0)
            {
                childPos.y += rTSCamData.zoomAmount;
                childPos.z -= rTSCamData.zoomAmount;
            }

            //Movement
            //Increase speed while pressing left shift
            if (Input.GetKey(KeyCode.LeftShift)) speed = rTSCamData.fastestSpeed;
            //Back normal speed while pressing left shift
            if (Input.GetKeyUp(KeyCode.LeftShift)) speed = rTSCamData.normalSpeed;
            //Movement with key and screen border
            if (Input.GetKey(KeyCode.W) || Input.mousePosition.y >= Screen.height - rTSCamData.screenBorderPx) pos += speed * Time.deltaTime * transform.forward;
            if (Input.GetKey(KeyCode.S) || Input.mousePosition.y <= rTSCamData.screenBorderPx) pos += -1 * speed * Time.deltaTime * transform.forward;
            if (Input.GetKey(KeyCode.A) || Input.mousePosition.x <= rTSCamData.screenBorderPx) pos -= speed * Time.deltaTime * transform.right;
            if (Input.GetKey(KeyCode.D) || Input.mousePosition.x >= Screen.width - rTSCamData.screenBorderPx) pos += speed * Time.deltaTime * transform.right;

            //Movement with slide
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                if (plane.Raycast(ray, out float entry))
                {
                    startPos = ray.GetPoint(entry);
                }
            }

            if (Input.GetMouseButton(0))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                if (plane.Raycast(ray, out float entry))
                {
                    currentPos = ray.GetPoint(entry);
                    pos += startPos - currentPos;

                }
            }
            //Clamp Camera position between min and max(map area) border.
            pos.x = Mathf.Clamp(pos.x, rTSCamData.minBorder.x, rTSCamData.maxBorder.x);
            pos.y = Mathf.Clamp(pos.y, rTSCamData.minBorder.y, rTSCamData.maxBorder.y);
            pos.z = Mathf.Clamp(pos.z, rTSCamData.minBorder.z, rTSCamData.maxBorder.z);

            childPos.x = Mathf.Clamp(childPos.x, rTSCamData.minZoomBorder.x, rTSCamData.maxZoomBorder.x);
            childPos.y = Mathf.Clamp(childPos.y, rTSCamData.minZoomBorder.y, rTSCamData.maxZoomBorder.y);
            childPos.z = Mathf.Clamp(childPos.z, rTSCamData.minZoomBorder.z, rTSCamData.maxZoomBorder.z);

            transform.position = pos;
            childCam.localPosition = childPos;
        }

        private void Update()
        {
            CamMovement();
        }
    }
}