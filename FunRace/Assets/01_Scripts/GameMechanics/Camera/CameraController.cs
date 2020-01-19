using UnityEngine;
using System.Collections;
using GameMechanics;

public class CameraController : MonoBehaviour
{
    Transform _target;
    Vector3 _distanceToTarget;
    Transform _targetAnchor;
    private LevelController _levelController;

    public void Init(LevelController levelController, PlayerController player)
    {
        _levelController = levelController;
        SetTarget(player.transform);
    }

    private void Update()
    {
        

        //Follow target
        if (_target != null)
        {
            //Reanchor
            if (_targetAnchor != null)
            {
                transform.position = Vector3.Slerp(transform.position, _targetAnchor.position, Time.deltaTime * 5);
                if((_targetAnchor.position - transform.position).sqrMagnitude <= 0.5)
                {
                    _distanceToTarget = transform.position - _target.position;
                    _targetAnchor = null;
                }

            } else
            {
                transform.position = _target.position + _distanceToTarget;
            }

            transform.LookAt(_target);
        }
    }

    public void SetTarget(Transform target)
    {
        _target = target;
        _distanceToTarget = transform.position - _target.position;
    }

    public void ReAnchor(Transform anchor)
    {
        _targetAnchor = anchor;
    }
}
