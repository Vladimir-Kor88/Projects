using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;
using TMPro;


public class Head : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2.0f;

    [SerializeField]
    private Transform _leftBoard;

    [SerializeField]
    private Transform _rightBoard;

    [SerializeField]
    private TextMeshProUGUI _scoreText;

    private static int _score;
    private Vector3 _target ;
    private Colour _color;
    private bool _isFever = false;
    private Vector3 _startPos;
    private float _timer;

    public event Action _OnFever;
    public event Action _OnFeverEnd;

    private void Start()
    {
        _color = GetComponent<Colour>();
        _startPos = _target = transform.position;
    }

    private void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0) && !_isFever)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit rHit;
            var cubeLayerIndex = LayerMask.NameToLayer("Road");
            var layerMask = (1 << cubeLayerIndex);

            if (Physics.Raycast(ray, out rHit, float.MaxValue, layerMask))
                GetTarget(rHit.point.x) ;        
        }

        if ((transform.position - _target).magnitude >= Mathf.Epsilon)
            transform.position = Vector3.MoveTowards(transform.position, _target, _speed * Time.fixedDeltaTime);

        _timer += Time.fixedDeltaTime;
        Debug.Log("Timer " + _timer);

        if(_timer >= 40)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision");
        if (other.CompareTag("CP"))
            _color.objColor = other.GetComponent<Colour>().objColor;
        else if (!_isFever 
                    && (other.CompareTag("Bomb") 
                        || (other.CompareTag("Man") && other.GetComponent<Colour>().objColor != _color.objColor)))
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        else
        {
            if (other.CompareTag("Crystal") && !_isFever)
            {
                _score++;
                _scoreText.text = _score.ToString();
                if (_score > 3)
                    StartCoroutine(Fever());
            }
            Destroy(other.gameObject);
        }         
    }

    private void GetTarget (float xPoint)
    {
        if (xPoint < _leftBoard.position.x)
            xPoint = _leftBoard.position.x ;
        else if (xPoint > _rightBoard.position.x)
            xPoint = _rightBoard.position.x ;

        _target = new Vector3(xPoint, transform.position.y, transform.position.z);
    }

    private IEnumerator Fever()
    {
        _isFever = true;
        _target = _startPos;
        _OnFever?.Invoke();
        yield return new WaitForSeconds(5);
        _isFever = false;
        _OnFeverEnd?.Invoke();
        _score = 0;
        _scoreText.text = _score.ToString();
    }
}
