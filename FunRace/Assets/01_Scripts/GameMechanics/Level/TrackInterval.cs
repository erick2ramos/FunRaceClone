using UnityEngine;
using System.Collections;

namespace GameMechanics
{
    public class TrackInterval : MonoBehaviour
    {
        public Transform Start { get { return transform; } }
        public Transform Finish { get; set; }
        public bool IsCheckpoint;
    }
}