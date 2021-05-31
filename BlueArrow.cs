using UnityEngine;

public class BlueArrow : BlueObstacle
{   protected override void Push()
    {
        var distance = _cat.transform.position - transform.position;

        if (distance.magnitude <= _maxDistanceToCat)
        {
            _rbCat.velocity = Vector2.zero;
            _rbCat.AddForce(transform.up.normalized * _powerPush);
        }
    }
}
