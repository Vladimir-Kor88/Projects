using UnityEngine;

public abstract class Obstacle : MonoBehaviour
{
    [SerializeField]
    protected float _powerPush = 10.0f;

    protected MainCharacter _cat;
    protected Rigidbody2D _rbCat;
    
    protected void Start()
    {
        _cat = GameObject.FindObjectOfType<MainCharacter>();
        _rbCat = _cat.GetComponent<Rigidbody2D>();
    }

    protected abstract void Push();
}
