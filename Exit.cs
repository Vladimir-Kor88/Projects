using UnityEngine;

public class Exit : MonoBehaviour
{
    [SerializeField]
    private GameObject _catObject;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(_catObject);
    }
}
