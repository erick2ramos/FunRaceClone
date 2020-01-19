using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

namespace GameMechanics {
    public class LevelController : MonoBehaviour
    {
        [SerializeField]
        private PlayerController _playerController;
        [SerializeField]
        private TrackController _trackController;
        [SerializeField]
        private CameraController _cameraController;
        [SerializeField]
        private WinPopup _winPopup;

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

        public void ShowWinPopup()
        {
            _winPopup.Open(new WinPopupModel()
            {
                OnAccept = () =>
                {
                    var scene = SceneManager.GetActiveScene();
                    SceneManager.LoadScene(scene.buildIndex);
                }
            });
        }
    }
}