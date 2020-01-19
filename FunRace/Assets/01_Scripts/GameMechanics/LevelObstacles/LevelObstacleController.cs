using UnityEngine;
using System.Collections;

public class LevelObstacleController : MonoBehaviour
{
    [SerializeField]
    LayerMask _collisionMask;

    private void OnCollisionEnter(Collision collision)
    {
        if((collision.transform.gameObject.layer & _collisionMask) > 0)
        {
            
        }
    }
}
