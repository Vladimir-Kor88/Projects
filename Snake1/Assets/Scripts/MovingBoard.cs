using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBoard : ObstacleMove
{
    private Vector3 _startPos;
    private float _repeatWidth;
   
    void Start()
    {
        _startPos = transform.position;
        _repeatWidth = GetComponent<BoxCollider>().size.z / 10;
    }

    protected override void Update() 
    {
        base.Update();
        if (transform.position.z < _startPos.z - _repeatWidth)
            transform.position = _startPos;
    }
}
