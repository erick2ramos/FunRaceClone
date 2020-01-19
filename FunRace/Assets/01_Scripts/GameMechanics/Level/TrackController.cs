using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace GameMechanics
{
    public class TrackController : MonoBehaviour
    {
        [SerializeField]
        Transform TrackIntervalsParent;

        TrackInterval[] _trackPoints;
        int _checkpoint;
        int _currentTrackPointIndex;

        public void Init()
        {
            _currentTrackPointIndex = 0;
            List<TrackInterval> trackPoints = new List<TrackInterval>();

            int previous = -1;
            foreach (var child in TrackIntervalsParent.GetComponentsInChildren<TrackInterval>())
            {
                trackPoints.Add(child);
                if (previous >= 0)
                    trackPoints[previous].Finish = child.Start;
                previous++;
            }
            _trackPoints = trackPoints.ToArray();
            _checkpoint = 0;
        }

        public TrackInterval GetCurrentTrackPoint()
        {
            return _trackPoints[_currentTrackPointIndex];
        }

        public TrackInterval NextInterval()
        {
            _currentTrackPointIndex++;
            if (_trackPoints[_currentTrackPointIndex].IsCheckpoint)
            {
                _checkpoint = _currentTrackPointIndex;
            }
            return _trackPoints[_currentTrackPointIndex];
        }

        public TrackInterval ResetToCheckpoint()
        {
            _currentTrackPointIndex = _checkpoint;
            return _trackPoints[_checkpoint];
        }

        public bool IsLastInterval()
        {
            return _currentTrackPointIndex >= _trackPoints.Length - 2;
        }
    }
}