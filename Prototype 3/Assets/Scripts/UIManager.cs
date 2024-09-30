using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Text scoreText;
    public static int score = 0;

    public PlayerController playerControllerScript;

    public bool won = false;

    // Start is called before the first frame update
    void Start()
    {
        if(scoreText == null)
        {
            scoreText == FindObjectOfType<Text>();
        }

        if(playerControllerScript == null)
        {
            playerControllerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        }

        scoreText.text = "Score: 0";
        
    }

    // Update is called once per frame
    void Update()
    {

        if(!playerControllerScript.gameOver && !won)
        {
            scoreText.text = "Score: " + score;
        }

        if (playerControllerScript.gameOver && !won)
        {
            scoreText.text = "You Lose!\nPress R to Try Again!";
        }

        if (score >= 10)
        {
            playerControllerScript.gameOver = true;
            won = true;

           // playerControllerScript.StopRunning();

            scoreText.text = "You Win!\nPress R to Try Again!";
        }

        if(playerControllerScript.gameOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
    }
}
