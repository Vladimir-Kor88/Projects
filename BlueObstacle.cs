using UnityEngine;

public class BlueObstacle : Obstacle
{
    [SerializeField]
    protected float _maxDistanceToCat = 5.0f;

    protected override void Push()
    {
        var distance = _cat.transform.position - transform.position;

        if (distance.magnitude <= _maxDistanceToCat)
        {
            Debug.Log("Push out " + distance.magnitude);
            _rbCat.velocity = Vector2.zero;
            _rbCat.AddForce(distance.normalized * _powerPush);
        }
    }
    private void OnMouseDown()
    {
        if (_cat != null)
        {
            Debug.Log("MouseClick");
            Push();
        }
    }
}
