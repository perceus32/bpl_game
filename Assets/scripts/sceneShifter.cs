using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class sceneShifter : MonoBehaviour
   

{
    public Text score;
    public int gameScore;
    // Start is called before the first frame update
    void Start()
    {
        
        gameScore = player.gameScore;
        score.text = "Score : " + gameScore.ToString();
        player.gameScore = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void PlayGame()
    {
        SceneManager.LoadScene("game");
    }
    public void Home()
    {
        SceneManager.LoadScene("home");
    }
}
