using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public int score = 0;
    public Text scoreText;
    public PlayerControllerX playerControllerXScript;
    public bool won = false;

    void Start()
    {
        if (scoreText == null)
        {
            scoreText = FindObjectOfType<Text>();
        }

        if (playerControllerXScript == null)
        {
            playerControllerXScript = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControllerX>();
        }

        UpdateScoreText();
    }

    void Update()
    {
        if (!playerControllerXScript.gameOver)
        {
            scoreText.text = "Score: " + score;

            if (score >= 10 && !won)
            {
                playerControllerXScript.gameOver = true;
                won = true;
                scoreText.text = "You Win!\nPress R to Try Again!";
            }
        }
        else
        {
            if (playerControllerXScript.gameOver = true && !won)
            {
                scoreText.text = "You Lose!\nPress R to Try Again!";
            }
        }

        if (playerControllerXScript.gameOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }
}
