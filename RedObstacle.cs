using UnityEngine;

public class RedObstacle : Obstacle
{
    protected override void Push()
    {
        var distance = _cat.transform.position - transform.position;
        Debug.Log("Push out " + distance.magnitude);
        _rbCat.velocity = Vector2.zero;
        _rbCat.AddForce(distance.normalized * _powerPush);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Push();
    }
}
