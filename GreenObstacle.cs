using UnityEngine;

public class GreenObstacle : BlueObstacle
{
    protected bool _mayPushIn = true;
    protected override void Push()
    {
        var distance = transform.position - _cat.transform.position ;

        if (distance.magnitude <= _maxDistanceToCat)
        {
            Debug.Log("Push out " + distance.magnitude);
            _rbCat.AddForce(distance.normalized * _powerPush);
        }
    }

    private void Update()
    {
        if (_mayPushIn && _cat != null)
            Push();
    }
}
