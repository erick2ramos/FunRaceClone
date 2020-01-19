using UnityEngine;
using System.Collections;
using BaseSystems.Managers;
using GameMechanics;

public class LoaderInitializer : MonoBehaviour
{
    [SerializeField]
    PlayerConfig _playerConfig;
    [SerializeField]
    ManagerHandler _handler;
    [SerializeField]
    LevelController _level;

    IEnumerator Start()
    {
        _handler.Init();
        yield return new WaitForSeconds(0.5f);
        _level.Init(_playerConfig);
        _level.gameObject.SetActive(true);
    }
}
