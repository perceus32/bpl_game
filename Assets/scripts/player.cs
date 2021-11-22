using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class player : MonoBehaviour
{
    public Animator anim;            //animation
    public Text score, highScore;    //ui
    public static int gameScore = 0;        //ui reference
    public float upForce;            //jumpForce
    public Rigidbody2D rb;           //player rb
    public bool isGameOver = false;  //game end logic
    public GameObject oMove;
    public float timer = 0f;
    public float timerVar;
    public bool canJump = true;
    public string game;
    Scene scene;
    


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(gameScore);
        highScore.text = "HighScore : " + PlayerPrefs.GetInt("HighScore", 0).ToString();    //initialize highscore to 0
        rb.AddForce(Vector2.up * upForce * Time.deltaTime);                //initial jump
        anim = GetComponent<Animator>();
        Scene scene = SceneManager.GetActiveScene();
    }

    void Update()
    {
        if (timer > 0)               //max jump height logic
        {
            timer -= Time.deltaTime;
        }
        else
        {
            canJump = true;
        }
        if (Input.GetKey(KeyCode.Space)&&canJump)                                                  //for keyboard control
        //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)            //for touch control
        {
            //Debug.Log("jumped yay");
            //rb.velocity += new Vector2(0, upForce) * Time.deltaTime;                      //jump using velocity
            //rb.AddForce(new Vector2(0, upForce) * Time.deltaTime * rb.gravityScale, ForceMode2D.Impulse);
            rb.velocity = Vector2.up * upForce;//jump using Force
            //anim.SetBool("Jump", true);
            if (isGameOver) return;
            
        }
        if (rb.transform.position.y >= 3.2f)
        {
            Debug.Log("yoo");
            canJump = false;
            //rb.velocity = new Vector2(0, 0);
            timer = timerVar;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isGameOver) return;
        if (!isGameOver&&scene.name==game)                                                //score update and display
        {
            gameScore++;
            score.text = "Score - " + gameScore.ToString();

        }
        if (gameScore > PlayerPrefs.GetInt("HighScore", 0))              //highscore update
        {
            PlayerPrefs.SetInt("HighScore", gameScore);
            highScore.text = "HighScore - " + PlayerPrefs.GetInt("HighScore").ToString();
        }

        //Debug.Log(isGameOver);

        //Debug.Log(isInAir);

    }
    private void OnCollisionEnter2D(Collision2D collision)               //if player touches bottom rectangle game ends
    {
        if (collision.gameObject.CompareTag("Dead"))
        {
            Debug.Log("Ded");
            GameOver();
            SceneManager.LoadScene("restart");
        }

        if (collision.gameObject.CompareTag("Coin"))
        {
            Debug.Log("Coin!");
            Destroy(collision.gameObject);
            gameScore += 64;
        }
    }
    public void GameOver()
    {
        isGameOver = true;
        //anim.SetBool("GameOver", true);
        //oMove.GetComponent<obstacleMove>().isGameOver = true;
        FindObjectOfType<parallax>().xVel = 0;
        //FindObjectOfType<gameEnd>().GameOver();
        isGameOver = true;
        //anim.SetBool("GameOver", true);
        //oMove.GetComponent<obstacleMove>().isGameOver = true;
        FindObjectOfType<parallax>().xVel = 0;
        FindObjectOfType<spawner>().GameOver();
    }
}