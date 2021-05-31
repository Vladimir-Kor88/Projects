using UnityEngine;

public class Oculars : MonoBehaviour
{
    [SerializeField]
    private float _speedEye = 0.01f;

    [SerializeField]
    private float _minDistance = 0.3f;

    [SerializeField]
    private float _bound = 0.07f;

    private Vector3 _startPosition;
    private Food[] _arrFoods;

    private void Start()
    {
        _startPosition = transform.localPosition;
        _arrFoods = FindObjectsOfType<Food>();
    }

    private void FixedUpdate()
    {
        var nearDistance = float.MaxValue;
        var nearestFood = new Vector3(0, 0);
        var anyActiveFood = false;
        foreach (var e in _arrFoods)
        {
            if (e.gameObject.activeSelf)
            {
                if (!anyActiveFood)
                    anyActiveFood = true;

                if ((e.transform.position - transform.root.position).magnitude < nearDistance)
                {
                    nearDistance = (e.transform.position - transform.root.position).magnitude;
                    nearestFood = e.transform.position;
                }
            }
        }
        Debug.Log("Nearest Food " + nearestFood.x + " " + nearestFood.y);
        if (nearDistance <= _minDistance)
            transform.Translate((nearestFood - transform.root.position).normalized * _speedEye);
        else if (anyActiveFood)
            transform.localPosition = _startPosition;
        else
        {
            var exitPosition = GameObject.FindObjectOfType<Exit>().transform.position;
            transform.Translate((exitPosition - transform.root.position).normalized * _speedEye);
        }

        CheckBounds();
    }

    private void CheckBounds()
    {
        if (transform.localPosition.x > _bound)
            transform.localPosition = new Vector2(_bound, transform.localPosition.y);
        else if (transform.localPosition.x < -_bound)
            transform.localPosition = new Vector2(-_bound, transform.localPosition.y);

        if (transform.localPosition.y > _bound)
            transform.localPosition = new Vector2(transform.localPosition.x, _bound);
        else if (transform.localPosition.y < -_bound)
            transform.localPosition = new Vector2(transform.localPosition.x, -_bound);
    }
}
