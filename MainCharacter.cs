using UnityEngine;
using TMPro;

public class MainCharacter : MonoBehaviour
{
    [SerializeField]
    private float _startSpeed = 10.0f;

    [SerializeField]
    private float _decrementSpeed = -0.5f;

    [SerializeField]
    private TextMeshProUGUI _scoreText;

    [SerializeField]
    private Rigidbody2D _rigidBody;

    [SerializeField]
    private float _yDirection = 1.3f;

    [SerializeField]
    private float _xBound = 2.12f;

    private int _score;

    public void ScoreUpdate(int addedScore)
    {
        _score += addedScore;
        _scoreText.text = "Score: " + _score;
    }

    private void Start()
    {
        var direction = new Vector2(Random.Range(-_xBound, _xBound), _yDirection).normalized;
        _rigidBody.AddForce(direction * _startSpeed, ForceMode2D.Impulse);
    }

    private void FixedUpdate()
    {
        _rigidBody.velocity += _decrementSpeed * _rigidBody.velocity * Time.fixedDeltaTime;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bottom"))
        {
            other.isTrigger = false;
            Debug.Log("ExitTrig");
        }
    }
}
