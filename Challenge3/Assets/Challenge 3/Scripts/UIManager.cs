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

        scoreText.text = "Score: " + score;
    }

    void Update()
    {
        if (!playerControllerXScript.gameOver)
        {
            scoreText.text = "Score: " + score;

            // Check for win condition
            if (score >= 10 && !won)
            {
                playerControllerXScript.gameOver = true;
                won = true;
                scoreText.text = "You Win!\nPress R to Try Again!";
            }
        }

        if (playerControllerXScript.gameOver && !won)
        {
            scoreText.text = "You Lose!\nPress R to Try Again!";
        }

        if (playerControllerXScript.gameOver && Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    // Method to add score
    public void AddScore()
    {
        score++;
        Debug.Log("Score: " + score);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Money"))
        {
            AddScore(); // Increment score when collecting money
            Destroy(other.gameObject); // Destroy the money object after collecting it
        }
    }
}
