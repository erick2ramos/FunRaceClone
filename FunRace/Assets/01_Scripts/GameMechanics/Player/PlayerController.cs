using UnityEngine;
using System.Collections;
using BaseSystems.Input;

namespace GameMechanics
{
    public class PlayerController : InputListener
    {
        private PlayerConfig _playerConfig;
        private PlayerData _playerData;
        private TrackController _trackController;
        private TrackInterval _intervalStart;
        private LevelController _levelController;
        private Rigidbody _rb;
        private Transform _model;

        private bool _isReady = false;
        private bool _canMove = false;
        private bool _inputActive = false;

        public void Init(LevelController levelControler, PlayerConfig playerConfig, TrackController currentTrack)
        {
            _levelController = levelControler;
            _rb = GetComponent<Rigidbody>();
            _model = transform.Find("Model");
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
                _model.rotation = Quaternion.Lerp(_model.rotation, Quaternion.LookRotation(dir), _playerData.TimeAccelerated);

                float percent = InverseLerp(_intervalStart.Start.position, _intervalStart.Finish.position, _intervalStart.Start.position + (dir.normalized * distance));
                if (percent >= 1 && _trackController.IsLastInterval())
                {
                    // win
                    Debug.Log("WIN");
                    _levelController.ShowWinPopup();
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
            _rb.isKinematic = true;
            _playerData.TimeAccelerated = 0;
            _trackController.ResetToCheckpoint();
            _intervalStart = _trackController.GetCurrentTrackPoint();
            transform.position = _intervalStart.Start.position;
            transform.rotation = Quaternion.identity;
        }

        public void Die()
        {
            _canMove = false;
            _inputActive = false;
            _rb.isKinematic = false;
            Invoke("Respawn", 1);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Obstacle")
            {
                Die();
            }
        }
    }
}
