using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField]
    private int _cost = 10;

    private MainCharacter _catCharacter;
  
    private void Start()
    {
        _catCharacter = GameObject.FindObjectOfType<MainCharacter>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gameObject.SetActive(false);
        _catCharacter.ScoreUpdate(_cost);
    }
}
