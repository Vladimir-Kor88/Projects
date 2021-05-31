using System.Collections;
using UnityEngine;

public class RedGreenArrow : GreenObstacle
{
    [SerializeField]
    private float _powerOutPush;

    private void OnTriggerEnter2D(Collider2D other)
    {
        _mayPushIn = false;
        _rbCat.AddForce(transform.up.normalized * _powerOutPush);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _mayPushIn = true;
    }
}
