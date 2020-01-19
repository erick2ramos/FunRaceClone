using UnityEngine;
using System.Collections;
using BaseSystems.Input;

namespace GameMechanics
{
    public class PlayerController : InputListener
    {
        PlayerConfig _playerConfig;
        PlayerData _playerData;
        TrackController _trackController;
        TrackInterval _intervalStart;
        Rigidbody _rb;
        bool _isReady = false;
        bool _canMove = false;
        bool _inputActive = false;

        public void Init(PlayerConfig playerConfig, TrackController currentTrack)
        {
            _rb = GetComponent<Rigidbody>();
            _trackController = currentTrack;
            _playerConfig = playerConfig;
            _playerData = new PlayerData();
            _playerData.TimeAccelerated = 0;
            _intervalStart = _trackController.GetCurrentTrackPoint();
            transform.position = _intervalStart.Start.position;
            _isReady = true;
            _canMove = true;
        }

        private void FixedUpdate()
        {
            if (_isReady && _canMove && _inputActive)
            {
                _playerData.TimeAccelerated += Time.deltaTime;
                float distance = _playerData.TimeAccelerated * _playerConfig.Speed;
                Vector3 dir = _intervalStart.Finish.position - _intervalStart.Start.position;
                Vector3 velocity = dir.normalized * _playerConfig.Speed * Time.deltaTime;
                transform.Translate(velocity);

                float percent = InverseLerp(_intervalStart.Start.position, _intervalStart.Finish.position, _intervalStart.Start.position + (dir.normalized * distance));
                if (percent >= 1 && _trackController.IsLastInterval())
                {
                    // win
                    Debug.Log("WIN");
                    _canMove = false;
                } else if (percent >= 1)
                {
                    // Player passed the interval get next interval
                    _intervalStart = _trackController.NextInterval();
                    _playerData.TimeAccelerated = 0;
                }
            }
        }

        public static float InverseLerp(Vector3 a, Vector3 b, Vector3 value)
        {
            Vector3 AB = b - a;
            Vector3 AV = value - a;
            return Vector3.Dot(AV, AB) / Vector3.Dot(AB, AB);
        }

        protected override void OnTouchStart(TouchInputEvent input)
        {
            if(_canMove)
                _inputActive = true;
        }

        protected override void OnTouchRelease(TouchInputEvent input)
        {
            _inputActive = false;
        }

        public void Respawn()
        {
            _canMove = true;
        }

        public void Die()
        {
            _canMove = false;
            _inputActive = false;
            Invoke("Respawn", 1);
        }
    }
}
