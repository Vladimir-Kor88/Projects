using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
    private float speed = 5;
    [HideInInspector]
    public float paramForBallRotate;
    private float bound = 2.5f;
    private float smoothSpeed = 0.1f;
    private GameObject ball;
    private Ball ballScript;
    
    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.Find("Ball");
        ballScript = ball.GetComponent<Ball>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(ballScript.isGame)
        { 
            if (gameObject.CompareTag("Player"))
            {
                paramForBallRotate = Input.GetAxis("Vertical") * Time.deltaTime * speed;
                transform.Translate(paramForBallRotate * Vector2.up);
            }
            else
            {
                paramForBallRotate = Random.Range(0.0f, 1.0f) * smoothSpeed;
                float newY = Mathf.Lerp(transform.position.y, ball.transform.position.y, paramForBallRotate);
                Vector2 position = new Vector2(transform.position.x, newY);
                if (ball.transform.position.y < transform.position.y)
                   paramForBallRotate = -paramForBallRotate;
                transform.position = position;
            }
            if (transform.position.y > bound)
                transform.position = new Vector2(transform.position.x, bound);
            else if(transform.position.y < -bound)
                transform.position = new Vector2(transform.position.x, -bound);
        }
    }
}
