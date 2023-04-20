using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedastrians : MonoBehaviour
{
    public Waypoint currentNode;
    public float speed, rotSpeed;
    public WaypointManager waypointManager;
    public int nodeIndex;
    private IEnumerator getNextNodeIE;
    public Vector3 a;
    private void Start()
    {
        Application.targetFrameRate = 60;
        getNextNodeIE = GetNextNode();
        StartCoroutine(getNextNodeIE);
    }

    void Update()
    {
        Vector3 destinaDirection = currentNode.transform.position - transform.position;
        destinaDirection.y = 0;

        Quaternion targetRot = Quaternion.LookRotation(destinaDirection + a);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotSpeed * Time.deltaTime);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private IEnumerator GetNextNode()
    {
        //Wait code block for changing to next node
        yield return new WaitUntil(() => currentNode != null && Vector3.Distance(transform.position, currentNode.transform.position) <= 1f);
        GetNexNode();

    }
    private void GetNexNode()
    {
        nodeIndex++;
        if (nodeIndex == 7) nodeIndex = 0;
        currentNode = waypointManager.waypoints[nodeIndex];
        a = new Vector3(Random.Range(-waypointManager.waypoints[nodeIndex].widht / 2, waypointManager.waypoints[nodeIndex].widht / 2), 0, 0);
        getNextNodeIE = GetNextNode();
        StartCoroutine(getNextNodeIE);
    }
    private void OnDisable()
    {
        //Clear coroutine
        StopCoroutine(getNextNodeIE);
    }
}
