using System.Collections.Generic;
using UnityEngine;

namespace Fade.WaypointSystem
{
    public class Waypoint : MonoBehaviour
    {
        public enum NodeType {normal,final,branch}
        public NodeType nodeType;
        public Waypoint nextNode;
        public Waypoint previousNode;

        public List<Waypoint> connections;
        public float widht = 1f;
        public bool isFinal;
        public bool isBranch;
    }
}

