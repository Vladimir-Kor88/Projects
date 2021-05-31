using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [SerializeField]
    private Vector2[] _arrPoints = new Vector2[] { new Vector2(-2.5f, 1.8f),
                                                   new Vector2(1.1f, 1.8f),
                                                   new Vector2(1.1f, -0.1f),
                                                   new Vector2(-2.5f, -0.1f) };
    [SerializeField]
    private float _speed = 0.1f;
    
    private int _numberOfStep;
    
    private void FixedUpdate()
    {
        var step = _speed * Time.fixedDeltaTime;

        transform.position = Vector2.MoveTowards(transform.position, _arrPoints[_numberOfStep], step);

        if (((Vector2)transform.position - _arrPoints[_numberOfStep]).magnitude < Mathf.Epsilon)
            _numberOfStep = _numberOfStep + 1 == _arrPoints.Length ? 0 : _numberOfStep + 1;
    }
}
