using UnityEngine;
using System.Collections;

namespace GameMechanics {
    public class LevelController : MonoBehaviour
    {
        [SerializeField]
        private PlayerController _playerController;
        [SerializeField]
        private TrackController _trackController;
        [SerializeField]
        private CameraController _cameraController;

        public void Init(PlayerConfig playerConfig)
        {
            _trackController.Init(this);
            _playerController.Init(this, playerConfig, _trackController);
            _cameraController.Init(this, _playerController);
        }

        public void SetNewCameraAnchor(Transform newAnchor)
        {
            _cameraController.ReAnchor(newAnchor);
        }
    }
    public class LevelData { }
    public class LevelObstacles { }
}