using UnityEngine;
using System.Collections;

namespace GameMechanics {
    public class LevelController : MonoBehaviour
    {
        [SerializeField]
        private PlayerController _playerController;
        [SerializeField]
        private TrackController _trackController;

        public void Init(PlayerConfig playerConfig)
        {
            _trackController.Init();
            _playerController.Init(playerConfig, _trackController);
        }

    }
    public class LevelData { }
    public class LevelObstacles { }
}