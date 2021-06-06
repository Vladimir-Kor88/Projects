using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tail : MonoBehaviour
{
    [SerializeField]
    private Transform _leftBoard;

    [SerializeField]
    private Transform _rightBoard;

    [SerializeField]
    private float _k;

    [SerializeField]
    private Transform _target;

    [SerializeField]
    private float _velocity;

    [SerializeField]
    private float _divider ;

    //[SerializeField]
    //private float _deltaMax = 2;

    private Colour _color;
    private Colour _targetColor;
    
    private void Start()
    {
        _color = GetComponent<Colour>();
        _targetColor = _target.GetComponent<Colour>();
        _targetColor._ColorChanged += OnTargetColorChanged;
    }

    private void FixedUpdate()
    {
        var delta = _target.position.x - transform.position.x;

        //if (Mathf.Abs(delta) > _deltaMax)
        //{
        //    Debug.Log("Big Space " + delta + " TargetX " + _target.position.x + " ThisX " + transform.position.x);
        //    transform.position = new Vector3(_target.position.x > transform.position.x ?
        //                                                _target.position.x - _deltaMax :
        //                                                _target.position.x + _deltaMax,
        //                                    transform.position.y,
        //                                    transform.position.z);
        //    delta = _deltaMax;
        //}

        _velocity += _k * delta * Time.fixedDeltaTime;
        _velocity /= _divider;
        var directX = _velocity * Time.fixedDeltaTime;

        if (transform.position.x + directX < _leftBoard.position.x)
            directX = _leftBoard.position.x - transform.position.x;
        else if (transform.position.x + directX > _rightBoard.position.x)
            directX = _rightBoard.position.x - transform.position.x;

        transform.Translate(new Vector3(directX, 0, 0));       
    }

    private void OnTargetColorChanged(Colours color)
    {
        _color.objColor = color;
    }
}
