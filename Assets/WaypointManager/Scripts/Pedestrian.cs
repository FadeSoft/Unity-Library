using System.Collections;
using UnityEngine;
namespace Fade.WaypointSystem
{
    public class Pedestrian : MonoBehaviour
    {
        public Waypoint currentNode;
        public float speed, rotSpeed;
        public WaypointManager waypointManager;
        private IEnumerator getNextNodeIE;

        private Vector3 a;
        private void Start()
        {
            Application.targetFrameRate = 60;

            currentNode = waypointManager.waypoints[0];
            getNextNodeIE = GetNextNode();
            StartCoroutine(getNextNodeIE);
        }

        void Update()
        {
            Vector3 destinaDirection = currentNode.transform.position - transform.position;
            destinaDirection.y = 0;

            Quaternion targetRot = Quaternion.LookRotation(destinaDirection + a);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotSpeed * Time.deltaTime);
            transform.Translate(speed * Time.deltaTime * Vector3.forward);
        }

        private IEnumerator GetNextNode()
        {
            //Wait code block for changing to next node
            yield return new WaitUntil(() => currentNode != null && Vector3.Distance(transform.position, currentNode.transform.position) <= 2f);
            GetNexNode();

        }
        private void GetNexNode()
        {
            if (currentNode.isBranch)
            {
                int random = Random.Range(-1, currentNode.connections.Count);
                currentNode = random == -1 ? currentNode.nextNode : currentNode.connections[random];

            }
            else currentNode = currentNode.nextNode;

            a = new Vector3(Random.Range(-currentNode.widht / 2, currentNode.widht / 2), 0, 0);
            getNextNodeIE = GetNextNode();
            StartCoroutine(getNextNodeIE);
        }
        private void OnDisable()
        {
            //Clear getNextNodeIE coroutine
            StopCoroutine(getNextNodeIE);
        }
    }
}

