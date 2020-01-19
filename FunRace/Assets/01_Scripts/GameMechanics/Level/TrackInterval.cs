using UnityEngine;
using System.Collections;

namespace GameMechanics
{
    // Path interval for the player to interpolate its position
    public class TrackInterval : MonoBehaviour
    {
        public Transform Start { get { return transform; } }
        public Transform Finish { get; set; }
        public Transform CameraAnchor;
        public bool IsCheckpoint;
    }
}