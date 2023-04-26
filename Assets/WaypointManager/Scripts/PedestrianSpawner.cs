using UnityEngine;
using Fade.WaypointSystem;
public class PedestrianSpawner : MonoBehaviour
{
    public GameObject npc;
    public WaypointManager wp;
  
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject npc = Instantiate(this.npc);
            npc.transform.GetComponent<Pedestrian>().waypointManager = wp;
            npc.transform.GetComponent<Pedestrian>().currentNode = wp.waypoints[0];

            var speed = Random.Range(.5f, 2.5f);
            npc.transform.GetComponent<Pedestrian>().speed = speed;
            npc.transform.GetComponent<Pedestrian>().rotSpeed = (speed * 45) + Random.Range(0, 20);
        }
    }
}
