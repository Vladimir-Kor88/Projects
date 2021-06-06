using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleMove : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;

    [SerializeField]
    protected float _speed = 1;

    [SerializeField]
    private float _zBound = -5.5f;

    private Head _playerScript;

    private void Start()
    {
        _playerScript = _player.GetComponent<Head>();
        _playerScript._OnFever += IncreaseSpeed;
        _playerScript._OnFeverEnd += DecreaseSpeed;
    }
    protected virtual void Update()
    {
        transform.Translate(new Vector3(0, 0, -1) * Time.deltaTime * _speed);

        if (transform.position.z < _zBound)
            Destroy(gameObject);
    }

    private void IncreaseSpeed()
    {
        _speed *= 3;
    }

    private void DecreaseSpeed()
    {
        _speed /= 3;
    }
}
