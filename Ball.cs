using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ball : MonoBehaviour
{
    [SerializeField]
    private Vector2 direction;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float divideCof = 2;
    private float ballRotation;
    public float rotationSmooth = 0.01f;
    private Vector2 delta;
    private float numSecForIncrSpeed = 10;
    private float incrSpeed = 1;
    private bool allowToIncrSpeed;
    private Animator anim;
    private float rocketBound = 14.5f;
    private int playerScore;
    private int compScore;
    public TextMeshProUGUI scoreText;
    public int secForRestart = 5;
    public bool isGame;
    private Transform particle;
    public int winScore = 2;
    public GameObject winScreen;
    public TextMeshProUGUI winText;
    private float timer;
    public TextMeshProUGUI pauseText;
    private bool isPause;
    private bool isCountdown;
    public TextMeshProUGUI countdownText;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Debug.Log("Start " + incrSpeed + " " + numSecForIncrSpeed);
        particle = transform.GetChild(0);
        BeginGame();
    }
    public void BeginGame()
    {
        winScreen.SetActive(false);
        Reset(0);
        StartCoroutine(RestartGame());
    }
    IEnumerator RestartGame()
    {
        isCountdown = true;
        countdownText.gameObject.SetActive(true);
        for (int i = secForRestart; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1);
        }
        countdownText.gameObject.SetActive(false);
        isGame = true;
        particle.gameObject.SetActive(true);
        isCountdown = false;
    }
    void PauseGame()
    {
        isGame = false;
        particle.gameObject.SetActive(false);
    }
    void Reset(float posBall)
    {
        timer = 0;
        PauseGame();
        if (posBall < 0)
            compScore++;
        else if (posBall > 0)
            playerScore++;
        else
        {
            compScore = 0;
            playerScore = 0;
        }
        scoreText.text = "Player: " + playerScore + "  Comp: " + compScore;
        ballRotation = 0;
        transform.position = new Vector2(0, 0);
        speed = 10;
        direction = Random.insideUnitCircle.normalized;
        allowToIncrSpeed = false;
        //StopCoroutine("IncrementSpeed");
        Debug.Log("Reset");
    }
    void Update()
    {
        if (isGame)
        {
            timer += Time.deltaTime;
            Debug.Log("Timer: " + timer);
            if (timer > numSecForIncrSpeed)
            {
                allowToIncrSpeed = true;
                timer -= numSecForIncrSpeed;
            }
        }
        if (Input.GetKeyDown(KeyCode.P) && !isCountdown)
        {
            SetPause(isPause);
        }
    }
    void SetPause(bool pauseSet)
    {
        isPause = !pauseSet;
        pauseText.gameObject.SetActive(!pauseSet);
        if (pauseSet)
            StartCoroutine(RestartGame());
        else
            PauseGame();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (isGame)
        {
            particle.localPosition = -direction.normalized * 2;
            //Debug.Log(transform.position.x);
            delta = -Vector2.Perpendicular(direction).normalized * ballRotation * rotationSmooth;
            direction += delta;
            transform.Translate(direction * Time.fixedDeltaTime * speed);
            //transform.Rotate(0,0,ballRotation * 0.01f);
            if (transform.position.x < -rocketBound || transform.position.x > rocketBound)
            {
                Reset(transform.position.x);
                if (compScore == winScore || playerScore == winScore)
                {
                    winScreen.SetActive(true);
                    winText.text = (compScore > playerScore ? "Comp" : "Player") + " is win";
                }
                else
                    StartCoroutine(RestartGame());
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        //Debug.LogError("!");
        ballRotation /= divideCof;
        if (other.gameObject.CompareTag("Board"))
            direction = Vector2.Reflect(direction, Vector2.up);
        else if(!other.gameObject.CompareTag("Center"))
        {
            anim.SetTrigger("Punch");
            direction = Vector2.Reflect(direction, Vector2.right);
            float paramForRotate = other.GetComponent<Rocket>().paramForBallRotate;
            Debug.Log(other.name + " " + paramForRotate);
            //if (other.gameObject.CompareTag("Computer"))
            //    paramForRotate = -paramForRotate;
            //float isComp = other.gameObject.CompareTag("Computer") ? -1 : 1;
            ballRotation += paramForRotate ;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Center") && allowToIncrSpeed)
        {
            Debug.Log("Cross Center");
            speed += incrSpeed;
            allowToIncrSpeed = false;
        }
    }
}
